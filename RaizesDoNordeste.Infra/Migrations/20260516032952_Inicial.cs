using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RaizesDoNordeste.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanalPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanalPedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusPagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEstoques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEstoques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Logradouro = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Complemento = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    CEP = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    HorarioAbertura = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    HorarioFechamento = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Senha = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    IdPerfil = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    DataAceiteTermos = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Perfils_IdPerfil",
                        column: x => x.IdPerfil,
                        principalTable: "Perfils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    IdProduto = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdUnidade = table.Column<Guid>(type: "TEXT", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoques_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Estoques_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCanalPedido = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    IdStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    IdStatusPagamento = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrecoDesconto = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdUnidade = table.Column<Guid>(type: "TEXT", nullable: false),
                    EhEntrega = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PontosUsuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Validade = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontosUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontosUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimentacaoEstoques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdEstoque = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdTipoEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "TEXT", nullable: false),
                    Motivo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    EstoqueId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoEstoques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacaoEstoques_Estoques_EstoqueId",
                        column: x => x.EstoqueId,
                        principalTable: "Estoques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentacaoEstoques_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensPedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    IdProduto = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdPedido = table.Column<Guid>(type: "TEXT", nullable: false),
                    PedidoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensPedidos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CanalPedidos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "App" },
                    { 2, "Totem" },
                    { 3, "Balcao" },
                    { 4, "Web" }
                });

            migrationBuilder.InsertData(
                table: "Perfils",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Gerente" },
                    { 3, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Pedido Realizado" },
                    { 2, "Confirmado" },
                    { 3, "Em Preparo" },
                    { 4, "Pronto" },
                    { 5, "Em Entrega" },
                    { 6, "Concluido" },
                    { 7, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "StatusPagamentos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Aguardando Pagamento" },
                    { 2, "Pagamento Aprovado" },
                    { 3, "Pagamento Recusado" },
                    { 4, "Reembolso Solicitado" },
                    { 5, "Reembolsado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_IdUnidade_IdProduto",
                table: "Estoques",
                columns: new[] { "IdUnidade", "IdProduto" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_UnidadeId",
                table: "Estoques",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_IdProduto_IdPedido",
                table: "ItensPedidos",
                columns: new[] { "IdProduto", "IdPedido" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_PedidoId",
                table: "ItensPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_ProdutoId",
                table: "ItensPedidos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoEstoques_EstoqueId",
                table: "MovimentacaoEstoques",
                column: "EstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoEstoques_UsuarioId",
                table: "MovimentacaoEstoques",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UnidadeId",
                table: "Pedidos",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PontosUsuarios_IdUsuario",
                table: "PontosUsuarios",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PontosUsuarios_UsuarioId",
                table: "PontosUsuarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Nome_Preco",
                table: "Produtos",
                columns: new[] { "Nome", "Preco" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_CNPJ",
                table: "Unidades",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdPerfil",
                table: "Usuarios",
                column: "IdPerfil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanalPedidos");

            migrationBuilder.DropTable(
                name: "ItensPedidos");

            migrationBuilder.DropTable(
                name: "MovimentacaoEstoques");

            migrationBuilder.DropTable(
                name: "PontosUsuarios");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "StatusPagamentos");

            migrationBuilder.DropTable(
                name: "TipoEstoques");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Perfils");
        }
    }
}
