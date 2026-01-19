using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phone_Book.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactId", "EMail", "Name", "PhoneNumber" },
                values: new object[] { 1, "seededmail@mail.com", "George", "1234567890" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 1);
        }
    }
}
