using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropetyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderAddress_Street",
                table: "Orders",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "OrderAddress_LastName",
                table: "Orders",
                newName: "Address_LastName");

            migrationBuilder.RenameColumn(
                name: "OrderAddress_FirstName",
                table: "Orders",
                newName: "Address_FirstName");

            migrationBuilder.RenameColumn(
                name: "OrderAddress_Country",
                table: "Orders",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "OrderAddress_City",
                table: "Orders",
                newName: "Address_City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Orders",
                newName: "OrderAddress_Street");

            migrationBuilder.RenameColumn(
                name: "Address_LastName",
                table: "Orders",
                newName: "OrderAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "Address_FirstName",
                table: "Orders",
                newName: "OrderAddress_FirstName");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Orders",
                newName: "OrderAddress_Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Orders",
                newName: "OrderAddress_City");
        }
    }
}
