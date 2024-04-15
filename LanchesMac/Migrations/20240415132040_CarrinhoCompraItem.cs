using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    public partial class CarrinhoCompraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoCompraItens",
                columns: table => new
                {
                    CarrinhoCompraItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LancheId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    CarrinhoCompraId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompraItens", x => x.CarrinhoCompraItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompraItens_Lanches_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanches",
                        principalColumn: "LancheId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompraItens_LancheId",
                table: "CarrinhoCompraItens",
                column: "LancheId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompraItens");
        }
    }
}
