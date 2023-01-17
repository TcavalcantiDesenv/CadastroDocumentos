using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransactions.Migrations
{
    public partial class Atualiza4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Documentos",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Documentos");
        }
    }
}
