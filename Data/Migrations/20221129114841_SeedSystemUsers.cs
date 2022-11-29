using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCodeAcademy.Web.Data.Migrations
{
    public partial class SeedSystemUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4dd9a048-143f-4b5c-a05a-4c82bb7609b5", "48b12277-d927-4a74-9088-9dd0a53b39e6", "Admin", "ADMIN" },
                    { "e25eaf46-c650-4fa8-920d-947a16d384b1", "0a7a0b7e-1974-4462-a2a7-db8a7b0c36e2", "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "HomeAddress", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1eb7cb27-23f3-4ebf-930e-5029221c7c5e", 0, null, "963633e3-b348-488f-8e74-44a0efb60549", "aa@aa.aa", true, null, false, null, "AA@AA.AA", "AA@AA.AA", "AQAAAAEAACcQAAAAEPlVnh3ih+nJZI4GICRxOo7sVqt/aW4RLlKCoJQCxBEuUsbcrgwqd6e1IypBrUAPGg==", null, false, "a8dc75e7-a3b7-4acb-993d-98e4db3be4bd", false, "aa@aa.aa" },
                    { "8b2d3533-0f4c-4c5d-905a-77c60368fb69", 0, null, "3af61580-1a76-4ad6-9632-8430d8e548e9", "mm@mm.mm", true, null, false, null, "MM@MM.MM", "MM@MM.MM", "AQAAAAEAACcQAAAAEG8q/hFAiu0IezLSkhJTnNbBhyA7lN/k0SmH2jNjifBesw+LhVVYyPMUMjZ15hWbFg==", null, false, "b7beede0-3c76-4609-9f68-5ab7f0a3303c", false, "mm@mm.mm" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4dd9a048-143f-4b5c-a05a-4c82bb7609b5", "1eb7cb27-23f3-4ebf-930e-5029221c7c5e" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e25eaf46-c650-4fa8-920d-947a16d384b1", "8b2d3533-0f4c-4c5d-905a-77c60368fb69" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4dd9a048-143f-4b5c-a05a-4c82bb7609b5", "1eb7cb27-23f3-4ebf-930e-5029221c7c5e" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e25eaf46-c650-4fa8-920d-947a16d384b1", "8b2d3533-0f4c-4c5d-905a-77c60368fb69" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4dd9a048-143f-4b5c-a05a-4c82bb7609b5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e25eaf46-c650-4fa8-920d-947a16d384b1");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1eb7cb27-23f3-4ebf-930e-5029221c7c5e");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8b2d3533-0f4c-4c5d-905a-77c60368fb69");
        }
    }
}
