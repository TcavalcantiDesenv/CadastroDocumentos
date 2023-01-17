using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransactions.Migrations
{
    public partial class Atualiza3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Documentss",
                table: "Documentss");

            migrationBuilder.RenameTable(
                name: "Documentss",
                newName: "Documentos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documentos",
                table: "Documentos",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Documentos",
                table: "Documentos");

            migrationBuilder.RenameTable(
                name: "Documentos",
                newName: "Documentss");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documentss",
                table: "Documentss",
                column: "DocumentId");
        }
    }
}
