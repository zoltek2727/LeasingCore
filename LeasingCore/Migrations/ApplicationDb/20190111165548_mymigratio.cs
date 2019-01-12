using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeasingCore.Migrations.ApplicationDb
{
    public partial class mymigratio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leasings_Products_ProductId",
                table: "Leasings");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamAssortments_Params_ParamId",
                table: "ParamAssortments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Leasings_LeasingId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "LeasingDetailParams");

            migrationBuilder.DropTable(
                name: "ProductParams");

            migrationBuilder.DropIndex(
                name: "IX_Leasings_ProductId",
                table: "Leasings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamAssortments",
                table: "ParamAssortments");

            migrationBuilder.DropColumn(
                name: "LeasingEnd",
                table: "Leasings");

            migrationBuilder.DropColumn(
                name: "LeasingExtend",
                table: "Leasings");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Leasings");

            migrationBuilder.RenameTable(
                name: "ParamAssortments",
                newName: "Assortments");

            migrationBuilder.RenameColumn(
                name: "LeasingId",
                table: "Reports",
                newName: "LeasingDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_LeasingId",
                table: "Reports",
                newName: "IX_Reports_LeasingDetailId");

            migrationBuilder.RenameColumn(
                name: "ParamAssortmentName",
                table: "Assortments",
                newName: "AssortmentName");

            migrationBuilder.RenameColumn(
                name: "ParamAssortmentBrand",
                table: "Assortments",
                newName: "AssortmentBrand");

            migrationBuilder.RenameColumn(
                name: "ParamAssortmentId",
                table: "Assortments",
                newName: "AssortmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamAssortments_ParamId",
                table: "Assortments",
                newName: "IX_Assortments_ParamId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportAdded",
                table: "Reports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductAdded",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeasingDetailEnd",
                table: "LeasingDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "LeasingDetailExtend",
                table: "LeasingDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "AssortmentPrice",
                table: "Assortments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assortments",
                table: "Assortments",
                column: "AssortmentId");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhotoUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                });

            migrationBuilder.CreateTable(
                name: "ProductAssortments",
                columns: table => new
                {
                    ProductAssortmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    AssortmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAssortments", x => x.ProductAssortmentId);
                    table.ForeignKey(
                        name: "FK_ProductAssortments_Assortments_AssortmentId",
                        column: x => x.AssortmentId,
                        principalTable: "Assortments",
                        principalColumn: "AssortmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAssortments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoProducts",
                columns: table => new
                {
                    PhotoProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhotoProductIsDefault = table.Column<bool>(nullable: false),
                    PhotoId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoProducts", x => x.PhotoProductId);
                    table.ForeignKey(
                        name: "FK_PhotoProducts_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "PhotoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoProducts_PhotoId",
                table: "PhotoProducts",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoProducts_ProductId",
                table: "PhotoProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssortments_AssortmentId",
                table: "ProductAssortments",
                column: "AssortmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssortments_ProductId",
                table: "ProductAssortments",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assortments_Params_ParamId",
                table: "Assortments",
                column: "ParamId",
                principalTable: "Params",
                principalColumn: "ParamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LeasingDetails_LeasingDetailId",
                table: "Reports",
                column: "LeasingDetailId",
                principalTable: "LeasingDetails",
                principalColumn: "LeasingDetailId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assortments_Params_ParamId",
                table: "Assortments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LeasingDetails_LeasingDetailId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "PhotoProducts");

            migrationBuilder.DropTable(
                name: "ProductAssortments");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assortments",
                table: "Assortments");

            migrationBuilder.DropColumn(
                name: "ReportAdded",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ProductAdded",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LeasingDetailEnd",
                table: "LeasingDetails");

            migrationBuilder.DropColumn(
                name: "LeasingDetailExtend",
                table: "LeasingDetails");

            migrationBuilder.DropColumn(
                name: "AssortmentPrice",
                table: "Assortments");

            migrationBuilder.RenameTable(
                name: "Assortments",
                newName: "ParamAssortments");

            migrationBuilder.RenameColumn(
                name: "LeasingDetailId",
                table: "Reports",
                newName: "LeasingId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_LeasingDetailId",
                table: "Reports",
                newName: "IX_Reports_LeasingId");

            migrationBuilder.RenameColumn(
                name: "AssortmentName",
                table: "ParamAssortments",
                newName: "ParamAssortmentName");

            migrationBuilder.RenameColumn(
                name: "AssortmentBrand",
                table: "ParamAssortments",
                newName: "ParamAssortmentBrand");

            migrationBuilder.RenameColumn(
                name: "AssortmentId",
                table: "ParamAssortments",
                newName: "ParamAssortmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Assortments_ParamId",
                table: "ParamAssortments",
                newName: "IX_ParamAssortments_ParamId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LeasingEnd",
                table: "Leasings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "LeasingExtend",
                table: "Leasings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Leasings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamAssortments",
                table: "ParamAssortments",
                column: "ParamAssortmentId");

            migrationBuilder.CreateTable(
                name: "LeasingDetailParams",
                columns: table => new
                {
                    LeasingDetailParamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeasingDetailId = table.Column<int>(nullable: false),
                    ParamAssortmentId = table.Column<int>(nullable: false),
                    ParamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeasingDetailParams", x => x.LeasingDetailParamId);
                    table.ForeignKey(
                        name: "FK_LeasingDetailParams_LeasingDetails_LeasingDetailId",
                        column: x => x.LeasingDetailId,
                        principalTable: "LeasingDetails",
                        principalColumn: "LeasingDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeasingDetailParams_ParamAssortments_ParamAssortmentId",
                        column: x => x.ParamAssortmentId,
                        principalTable: "ParamAssortments",
                        principalColumn: "ParamAssortmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeasingDetailParams_Params_ParamId",
                        column: x => x.ParamId,
                        principalTable: "Params",
                        principalColumn: "ParamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductParams",
                columns: table => new
                {
                    ProductParamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParamId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParams", x => x.ProductParamId);
                    table.ForeignKey(
                        name: "FK_ProductParams_Params_ParamId",
                        column: x => x.ParamId,
                        principalTable: "Params",
                        principalColumn: "ParamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductParams_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leasings_ProductId",
                table: "Leasings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LeasingDetailParams_LeasingDetailId",
                table: "LeasingDetailParams",
                column: "LeasingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_LeasingDetailParams_ParamAssortmentId",
                table: "LeasingDetailParams",
                column: "ParamAssortmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeasingDetailParams_ParamId",
                table: "LeasingDetailParams",
                column: "ParamId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParams_ParamId",
                table: "ProductParams",
                column: "ParamId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParams_ProductId",
                table: "ProductParams",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leasings_Products_ProductId",
                table: "Leasings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamAssortments_Params_ParamId",
                table: "ParamAssortments",
                column: "ParamId",
                principalTable: "Params",
                principalColumn: "ParamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Leasings_LeasingId",
                table: "Reports",
                column: "LeasingId",
                principalTable: "Leasings",
                principalColumn: "LeasingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
