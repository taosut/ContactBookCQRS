using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactBookCQRS.Infra.Persistence.Migrations
{
    public partial class ContactChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "dbo",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "dbo",
                table: "Contacts");
        }
    }
}
