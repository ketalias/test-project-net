using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASPNetExapp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "ComputerInfo", "Department", "LastName", "RoomNumber" },
                values: new object[,]
                {
                    { 1, "Asus Vivobook", "Backend", "Ivanich", 15 },
                    { 2, "Asus Pro Vivobook", "Frontend", "Fofich", 15 },
                    { 3, "Macbook", "UI", "Bazzich", 15 },
                    { 4, "Asus Vivobook", "UX", "Gedich", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
