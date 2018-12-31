﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeasingCore.Models;
using Microsoft.AspNetCore.Authorization;
using LeasingCore.Helpers;
using System.Net.Mail;
using System.Net;

namespace LeasingCore.Controllers
{
    public class CartController : Controller
    {
        //private readonly LeasingContext _context;

        //public CartController(LeasingContext context)
        //{
        //    _context = context;
        //}

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

            //var leasingContext = _context.Products.Include(p => p.Category);
            //return View(await leasingContext.ToListAsync());
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            Product productModel = new Product();
            if (SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<ShoppingCart>();
                cart.Add(new ShoppingCart() { Product = _context.Products.Find(id), Quantity = 1 });
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
                    cart.Add(new ShoppingCart() { Product = _context.Products.Find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
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


            
                Leasing l = new Leasing
                {
                    LeasingStart = DateTime.Now,
                    LeasingEnd = DateTime.MaxValue,
                    LeasingExtend = true,
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
                        ProductId = item.Product.ProductId
                    };
                    _context.Add(ld);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    ViewBag.ErrorMessage = "Your cart can't be empty";
                    return View("Index");
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
