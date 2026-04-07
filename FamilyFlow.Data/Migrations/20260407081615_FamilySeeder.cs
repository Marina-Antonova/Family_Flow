using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class FamilySeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Families",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 10, "Anderson", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3e9ad1c-fb02-4243-91d0-03c5368ab041", "AQAAAAIAAYagAAAAEHnhByjI59ZTArg8AGcE05N/x8nf6vU5wnMmT5Em/RvzkSqj/geZaxotLR4WoT8X4A==", "7483fe14-54e5-4727-bcf9-9a04a570e87b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "197d9a55-d8ed-423a-94ac-5c76067b7d26", "AQAAAAIAAYagAAAAENn1wO37w52A5GHser1XkaOpTU2Er15xbd7ntzawK3c+akUTWW7kpuXsxeZvCr8a0Q==", "a7983a11-c341-484e-bcc6-998c0e2762b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e8e2bc0-9858-4a04-b927-2d7816f43bab", "AQAAAAIAAYagAAAAECIeF2eyD8Q/Vr42OzkZLQriJmeTesLNiMJsPGhP47gzwTgIaz6WHLHp7YhrT09Q0Q==", "0adbf915-8298-44bb-9ed4-001dcca1049b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4beab116-157c-44ba-8edb-25278c51c94b", "AQAAAAIAAYagAAAAEJCoXu7+yC4ll0hnFwDmGmD/+V+7oX/Wqqsi3gUqqusFCFTGo9z7O8VGUVMNQMBi6A==", "6b7debd6-d685-4410-a584-07ecfe19a5b4" });
        }
    }
}
