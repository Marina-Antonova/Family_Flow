using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedTasksAndEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38bbdf9b-b4a7-489d-b6a4-c66728a23e00", "AQAAAAIAAYagAAAAEBfZF7D0lyB1bFKMJ/Aal4aX13OCVHeLjSx/Vbn1aBhzFIxW+CxjFt/sm/yimMHPnA==", "94f76c6f-cb60-44a7-b1e7-20481abad09a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13ff91cd-a1da-4185-ae71-f6f4e43f38ac", "AQAAAAIAAYagAAAAEEM/kku3fZOC1Se6BirPvUndJjCjFkkh03OMzw2WyDUiqfopyh8cLyPJcbFkNVwwGA==", "75d413ed-6e90-4385-ad53-abc08b5f31d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37aa2bda-ebd0-46c0-8c3b-4f1b704d70da", "AQAAAAIAAYagAAAAEIxSGLIuJpbG7dCNoC4aRpUEk82ZHqdntlTKvPIkcVe9GlN7DrppIF3WW2xFJL7R8g==", "cee10e2f-2b41-4fbd-a7fd-142d784a58c8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0532194-5935-4d8f-a8a2-8c34ded56416", "AQAAAAIAAYagAAAAEI3/yCNW1QkLrOVdiQ+jfQUzeKBC4h5+9/rKhP9dn1wycBsBh4Tu7vHMAhqvPlzASg==", "db73695c-e32f-42e0-bc82-80d8b2ca45e4" });

            migrationBuilder.InsertData(
                table: "HouseTasks",
                columns: new[] { "Id", "Description", "DueDate", "FamilyMemberId", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { 3, "Wipe down counters and mop the floor", new DateTime(2026, 4, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), 11, false, "Clean the kitchen" },
                    { 4, "Mow the front and back yard", new DateTime(2026, 4, 13, 17, 0, 0, 0, DateTimeKind.Unspecified), 12, false, "Mow the lawn" },
                    { 5, "Collect all trash and recycling and take to curb", new DateTime(2026, 4, 11, 19, 0, 0, 0, DateTimeKind.Unspecified), 13, false, "Take out the trash" },
                    { 6, "Clean the exterior and interior of the car", new DateTime(2026, 4, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), 12, false, "Wash the car" },
                    { 7, "Sort and declutter items in the garage", new DateTime(2026, 4, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), 12, false, "Organize the garage" },
                    { 8, "Buy groceries for the week", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 11, false, "Grocery shopping" },
                    { 9, "Wash and fold clothes", new DateTime(2026, 4, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), 14, false, "Laundry" }
                });

            migrationBuilder.InsertData(
                table: "ScheduleEvents",
                columns: new[] { "Id", "AccompanyingAdultId", "CreatorId", "EndTime", "StartTime", "Title" },
                values: new object[,]
                {
                    { 7, 11, 11, new DateTime(2026, 4, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 12, 7, 0, 0, 0, DateTimeKind.Unspecified), "Morning Walk" },
                    { 8, null, 12, new DateTime(2026, 4, 13, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 13, 18, 30, 0, 0, DateTimeKind.Unspecified), "Family Meeting" },
                    { 9, 12, 13, new DateTime(2026, 4, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), "Birthday Party" },
                    { 10, 11, 14, new DateTime(2026, 4, 15, 12, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 15, 11, 30, 0, 0, DateTimeKind.Unspecified), "Doctor Apointment" }
                });

            migrationBuilder.InsertData(
                table: "ScheduleEventParticipants",
                columns: new[] { "FamilyMemberId", "ScheduleEventId" },
                values: new object[,]
                {
                    { 12, 8 },
                    { 13, 8 },
                    { 14, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ScheduleEventParticipants",
                keyColumns: new[] { "FamilyMemberId", "ScheduleEventId" },
                keyValues: new object[] { 12, 8 });

            migrationBuilder.DeleteData(
                table: "ScheduleEventParticipants",
                keyColumns: new[] { "FamilyMemberId", "ScheduleEventId" },
                keyValues: new object[] { 13, 8 });

            migrationBuilder.DeleteData(
                table: "ScheduleEventParticipants",
                keyColumns: new[] { "FamilyMemberId", "ScheduleEventId" },
                keyValues: new object[] { 14, 8 });

            migrationBuilder.DeleteData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 8);

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
    }
}
