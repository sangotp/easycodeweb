using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Bogus;
using EasyCodeAcademy.Web.Models;

#nullable disable

namespace EasyCodeAcademy.Web.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.CategoryId);
                });

            //// Fake Data: Bogus
            //Randomizer.Seed = new Random(8675309);

            //var fakerCategory = new Faker<Category>();

            //fakerCategory.RuleFor(c => c.CategoryName, f => f.Lorem.Sentence(1, 1));
            //fakerCategory.RuleFor(c => c.created_date, f => f.Date.Between(new DateTime(2022,1,1), DateTime.Now));
            //fakerCategory.RuleFor(c => c.updated_date, f => f.Date.Between(new DateTime(2022, 1, 1), DateTime.Now));

            //for (int i = 0; i < 20; i++)
            //{
            //    Category category = fakerCategory.Generate();
            //    migrationBuilder.InsertData(
            //        table: "categories",
            //        columns: new[] { "CategoryName", "created_date", "updated_date" },
            //        values: new object[]
            //        {
            //            category.CategoryName,
            //            category.created_date,
            //            category.updated_date
            //        }
            //    );
            //}

            //migrationBuilder.InsertData(
            //    table: "categories",
            //    columns: new[] { "CategoryName", "created_date", "updated_date" },
            //    values: new object[]
            //    {
            //        "Web",
            //        DateTime.Now,
            //        DateTime.Now
            //    }
            //);

            //migrationBuilder.InsertData(
            //    table: "categories",
            //    columns: new[] { "CategoryName", "created_date", "updated_date" },
            //    values: new object[]
            //    {
            //        "Programming Language",
            //        DateTime.Now,
            //        DateTime.Now
            //    }
            //);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
