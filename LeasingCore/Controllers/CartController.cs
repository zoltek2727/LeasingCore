using LeasingCore.Helpers;
using LeasingCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LeasingCore.Controllers
{
    public class CartController : Controller
    {
        LeasingContext _context = new LeasingContext();

        // GET: Cart
        [Authorize]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;

            if (cart != null)
            {
                ViewBag.total = cart.Sum(item => item.Product.ProductPrice * item.Quantity);
                return View();
            }

            return View();
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> GetCartAmount()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");

            var names = "0";

            try
            {
                if (cart != null)
                {
                    names = cart.Count.ToString();
                }

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("Buy/{id}")]
        public IActionResult Buy(int id, string amount)
        {
            if(string.IsNullOrWhiteSpace(amount))
            {
                amount = "1";
            }

            Product productModel = new Product();
            if (SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<ShoppingCart>();
                cart.Add(new ShoppingCart() { Product = _context.Products.Find(id), Quantity = Convert.ToInt32(amount) });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new ShoppingCart() { Product = _context.Products.Find(id), Quantity = Convert.ToInt32(amount) });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Order()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;

            if (cart != null)
            {
                ViewBag.total = cart.Sum(item => item.Product.ProductPrice * item.Quantity);
                return View();
            }

            return View();
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Leasing()
        {
            List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");

            try
            {
                Leasing l = new Leasing
                {
                    LeasingStart = DateTime.Now,
                    UserId = 1
                };
                _context.Add(l);

                LeasingDetail ld;

                foreach (var item in cart)
                {
                    ld = new LeasingDetail
                    {
                        LeasingId = l.LeasingId,
                        LeasingDetailAmount = item.Quantity,
                        LeasingDetailExtend = true,
                        LeasingDetailEnd = DateTime.Now.AddYears(1),
                        ProductId = item.Product.ProductId
                    };
                    _context.Add(ld);
                }

                _context.SaveChanges();
            }
            catch
            {
                ViewBag.ErrorMessage = "Your cart can't be empty";
                return RedirectToAction("Index", "Leasings", new { id = _context.Leasings.Last().LeasingId });
            }
                
            cart = null;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("mail@gmail.com", "haslo");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("mail@gmail.com");
            mailMessage.To.Add("mail@wp.pl");
            mailMessage.Body = "body";
            mailMessage.Attachments.Add(new Attachment(@"C:\PDF\Leasing.pdf"));
            mailMessage.Subject = "subject";
            client.Send(mailMessage);

            return RedirectToAction("Index", "Leasings");
        }

        private int isExist(int id)
        {
            List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
