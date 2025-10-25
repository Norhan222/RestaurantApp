using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedDataForCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9249), "Start your meal with delicious small bites and starters.", null, false, "Appetizers", null },
                    { 2, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9253), "Hearty and satisfying main dishes for every taste.", null, false, "Main Courses", null },
                    { 3, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9254), "Freshly made pasta and wood-fired pizzas.", null, false, "Pasta & Pizza", null },
                    { 4, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9255), "Classic sandwiches and juicy burgers served with fries.", null, false, "Sandwiches & Burgers", null },
                    { 5, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9256), "Healthy and fresh salad options.", null, false, "Salads", null },
                    { 6, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9257), "Delicious seafood dishes cooked with care.", null, false, "Seafood", null },
                    { 7, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9258), "Sweet treats to end your meal perfectly.", null, false, "Desserts", null },
                    { 8, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9259), "Refreshing cold and hot drinks.", null, false, "Beverages", null },
                    { 9, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9260), "Tasty meals specially made for kids.", null, false, "Kids Menu", null },
                    { 10, new DateTime(2025, 10, 14, 11, 53, 55, 448, DateTimeKind.Utc).AddTicks(9277), "Seasonal and promotional dishes at great prices.", null, false, "Special Offers", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
