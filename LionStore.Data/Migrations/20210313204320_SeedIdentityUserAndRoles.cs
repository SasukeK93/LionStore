using Microsoft.EntityFrameworkCore.Migrations;

namespace LionStore.Data.Migrations
{
    public partial class SeedIdentityUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "80755ea2-054c-448b-9252-7485ce463606", "Admin", "ADMIN" },
                    { "2", "7e6a81e4-f414-49d7-a173-2eb04ac6da64", "Seller", "SELLER" },
                    { "3", "3ab6cab1-0a0e-49fc-9941-65f6681d7ea1", "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "c3267143-a087-4f79-b46c-08f6eb2bca86", "admin@lionstore.com", true, null, null, false, null, "ADMIN@LIONSTORE.COM", "ADMIN", "AQAAAAEAACcQAAAAEBNwixV+0jwaqKVdITTapvoGDUdfi4HQuXrAhSI4CQ13MnmZPBjAnu/BT3OCaQZ/Ag==", null, false, "f624d37a-26dc-4928-af00-a1a744b67ac4", false, "admin" },
                    { "2", 0, "210a3d58-8082-43d4-9604-3d1d46fc3bdf", "seller@lionstore.com", true, null, null, false, null, "SELLER@LIONSTORE.COM", "SELLER", "AQAAAAEAACcQAAAAEP2yxoL60bLPW+Btrtg7+ewdw9C1EM4cHppibpl5fjabwasncCy02+GjPhw75RZfoQ==", null, false, "9df3ab34-6fae-42d8-9e0e-e853c3300df7", false, "seller" },
                    { "3", 0, "9201283c-d200-4d85-b09c-916e5507943c", "client@lionstore.com", true, null, null, false, null, "CLIENT@LIONSTORE.COM", "CLIENT", "AQAAAAEAACcQAAAAECLx+pM1haQ0IRsu7sGs9pHZ0WbQ++uHaoWEBeFILRvue0pqOCB3U9v6/8bLfda4cg==", null, false, "07684ac5-5fa8-4e5e-8781-280313dbcdd9", false, "client" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2", "2" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "3", "3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "3", "3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");
        }
    }
}
