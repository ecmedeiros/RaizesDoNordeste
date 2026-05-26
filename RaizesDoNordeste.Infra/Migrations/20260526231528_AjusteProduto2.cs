using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaizesDoNordeste.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjusteProduto2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtos_Nome_Preco",
                table: "Produtos");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Nome_Descricao",
                table: "Produtos",
                columns: new[] { "Nome", "Descricao" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtos_Nome_Descricao",
                table: "Produtos");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Nome_Preco",
                table: "Produtos",
                columns: new[] { "Nome", "Preco" },
                unique: true);
        }
    }
}
