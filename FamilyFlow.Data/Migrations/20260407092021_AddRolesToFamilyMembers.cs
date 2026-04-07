using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToFamilyMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "829596ea-31fc-4084-957c-e7cc9b951bbb", "AQAAAAIAAYagAAAAEMF9+gXIFa0EgIuPTJZ1k8q0wvu7y4/V5lWAFTz65yxhIjmGzv0EnQcScQXj3BY9Gg==", "c4ae3b96-10ce-47fd-b51b-a13d94eb7779" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86a1c0ee-fbf2-4ff3-96a9-9a8f297064ff", "AQAAAAIAAYagAAAAEH1MW8ZyqXUO5QXqgDazvpOqiZjAWtTr2lheSKKLTWoz+hlnu5ehlhLJAh843ZEeqA==", "21733f71-dcd4-4609-9b37-797f22ab8791" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d44bbe34-43af-4e77-b4e5-205a989d7f81", "AQAAAAIAAYagAAAAEACQQbW+vHJ5Lnz8XLoFHc+ViPp1C+ISHN1JKM8ZSHszc1TJ8dHYCudm7DWqJgmzXg==", "69225e98-c912-4b22-9e53-e57e63738481" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b973c3ae-a5d6-4ea8-b124-ec2258c204cf", "AQAAAAIAAYagAAAAECXOB5yRSc6sIkMRC/thPUENuJk2NmLPv6mbb2tLL+BAQet2su82t82VywxVMbu+sA==", "ee905944-8503-4403-808f-42b77c65e6bd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
