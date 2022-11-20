﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCodeAcademy.Web.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for(int i = 1; i < 150; i++)
            {
                migrationBuilder.InsertData(
                    "Users",
                    columns: new[]
                    {
                        "Id",
                        "UserName",
                        "Email",
                        "SecurityStamp",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",
                        "HomeAddress"
                    }, values: new object[]
                    {
                        Guid.NewGuid().ToString(),
                        $"email{i.ToString("D3")}@example.com",
                        $"email{i.ToString("D3")}@example.com",
                        Guid.NewGuid().ToString(),
                        true,
                        false,
                        false,
                        false,
                        0,
                        "...@#%..."
                    }
                );
            }    
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}