using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class FamilyMembersSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e133ec9f-9ef3-4852-962a-bd9565297e50", "AQAAAAIAAYagAAAAEP5UbZZzUYdXnt0MYOIF+k06FsAXzGO8JfPU2P6Rb6yx+2+9bwJDC5Yxs+ttGyPc4w==", "0cc3f9dd-097f-429a-acda-fc03a32f3f9a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f453cf1-16ef-4575-b7f3-f85529b6697e", "AQAAAAIAAYagAAAAEI0+fCb490M+IB6pCizS+3LqJq7CRAdXv16+SoPGWazzC3ebyRV459W8oR/ge3ASnA==", "59da5993-c87c-4958-8f4b-51c94dc537a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28d73245-3811-4dd8-b8be-039905ed7a22", "AQAAAAIAAYagAAAAEAV656b+FmsvVicvej8u+maugq1XSM9MCC/z9tFluZesv3DeEFHiCgyWUYO0IHmg1Q==", "dc58c858-6596-4d9b-a53d-463fd848dc5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d610f77d-d645-4f9c-8f4c-d42432ef7db1", "AQAAAAIAAYagAAAAEJ6YbwEkkbxbxBT1zDQri4twseQ7aivqeu+rubAVVatsGIi4aAnnPAYcaa9aMUjsyg==", "4cde1112-cdb7-43d1-bcfb-b2e62934db61" });

            migrationBuilder.InsertData(
                table: "FamilyMembers",
                columns: new[] { "Id", "Age", "Email", "FamilyId", "LinkedUserId", "Name", "Role", "UserId" },
                values: new object[,]
                {
                    { 11, 40, null, 10, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Alice", 0, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { 12, 42, null, 10, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Bob", 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { 13, 6, null, 10, null, "Charlie", 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { 14, 10, null, 10, new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Daisy", 3, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { 15, 56, null, 10, null, "Elise", 4, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f068e0b6-c2fe-4ad6-9f3f-cc232c385cd9", "AQAAAAIAAYagAAAAED8ZmhbLLqdDpekOFoRYp7BhsoYPfHj1y2gngk9G6RmdKQTr9U21bw0wcMliOIlLKw==", "6641a1f5-8620-4330-a931-69c5743c2aef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fc3315b-f7d0-48ea-a37f-49599fb0cb7f", "AQAAAAIAAYagAAAAEA74G16tjqtw2njv36q8+njfnAFnHi7srtYoIBCLLw/k3HF33Poo43ZDeB67LvGkcQ==", "1c2c4c75-74d0-4be0-ba00-6ff7f20a34e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da0ed3fc-f8f6-4efc-bfec-a09697cd8a36", "AQAAAAIAAYagAAAAEFd6ZE6466MjWD42EIG5KilrjoM4Ze9sNzyEJ2WoGhUMehj954c/ExZMdiH8gtJ0iw==", "128cf12a-8477-4c3c-ae26-6ab80ed8894b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8db5eb64-79ae-4e77-ae3e-c2a281eb66a4", "AQAAAAIAAYagAAAAEOZDZTRebKWM+0sfO46kDIg5ScqTD6HVpe9t9V0V5S2y8LlOgEqECxiPNDpiaxjDag==", "fb21f108-e44b-4794-9ac8-8c5755f8f171" });
        }
    }
}
