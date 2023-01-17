using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransactions.Migrations
{
    public partial class Atualiza2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Documentss",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Documentss");
        }
    }
}
