using BCrypt.Net;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Infra.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await SeedUnidadesAsync(context);
        await SeedUsuariosAsync(context);
        await SeedProdutosAsync(context);
        await SeedEstoquesAsync(context);
    }

    private static async Task SeedUnidadesAsync(AppDbContext context)
    {
        if (context.Unidades.Any()) return;

        var unidades = new List<Unidade>
        {
            new Unidade
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                Nome = "Raízes do Nordeste - Fortaleza Centro",
                CNPJ = "12345678000190",
                Telefone = "85912345678",
                Email = "fortaleza@raizesnordeste.com.br",
                Logradouro = "Rua dos Pinheiros",
                Numero = "123",
                Bairro = "Centro",
                Cidade = "Fortaleza",
                UF = "CE",
                CEP = "60010000",
                HorarioAbertura = new TimeOnly(8, 0),
                HorarioFechamento = new TimeOnly(22, 0),
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            },
            new Unidade
            {
                Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"),
                Nome = "Raízes do Nordeste - Recife Boa Viagem",
                CNPJ = "98765432000110",
                Telefone = "81987654321",
                Email = "recife@raizesnordeste.com.br",
                Logradouro = "Av. Boa Viagem",
                Numero = "456",
                Bairro = "Boa Viagem",
                Cidade = "Recife",
                UF = "PE",
                CEP = "51011000",
                HorarioAbertura = new TimeOnly(9, 0),
                HorarioFechamento = new TimeOnly(23, 0),
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            }
        };

        await context.Unidades.AddRangeAsync(unidades);
        await context.SaveChangesAsync();
    }

    private static async Task SeedUsuariosAsync(AppDbContext context)
    {
        if (context.Usuarios.Any()) return;

        var usuarios = new List<Usuario>
        {
            new Usuario
            {
                Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-123456789012"),
                Nome = "Administrador",
                Email = "admin@raizesnordeste.com.br",
                Senha = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                IdPerfil = 1, // Admin
                DataAceiteTermos = DateTime.UtcNow,
                DataCriacao = DateTime.UtcNow
            },
            new Usuario
            {
                Id = Guid.Parse("d4e5f6a7-b8c9-0123-defa-234567890123"),
                Nome = "Gerente Fortaleza",
                Email = "gerente@raizesnordeste.com.br",
                Senha = BCrypt.Net.BCrypt.HashPassword("Gerente@123"),
                IdPerfil = 2, // Gerente
                DataAceiteTermos = DateTime.UtcNow,
                DataCriacao = DateTime.UtcNow
            },
            new Usuario
            {
                Id = Guid.Parse("e5f6a7b8-c9d0-1234-efab-345678901234"),
                Nome = "Cliente Teste",
                Email = "cliente@teste.com.br",
                Senha = BCrypt.Net.BCrypt.HashPassword("Cliente@123"),
                IdPerfil = 3, // Cliente
                DataAceiteTermos = DateTime.UtcNow,
                DataCriacao = DateTime.UtcNow
            }
        };

        await context.Usuarios.AddRangeAsync(usuarios);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProdutosAsync(AppDbContext context)
    {
        if (context.Produtos.Any()) return;

        var produtos = new List<Produto>
        {
            new Produto
            {
                Id = Guid.Parse("f6a7b8c9-d0e1-2345-fabc-456789012345"),
                Nome = "Baião de Dois",
                Descricao = "Prato típico nordestino com arroz e feijão verde",
                Preco = 29.90m,
                DataCriacao = DateTime.UtcNow
            },
            new Produto
            {
                Id = Guid.Parse("a7b8c9d0-e1f2-3456-abcd-567890123456"),
                Nome = "Carne de Sol",
                Descricao = "Carne de sol grelhada com manteiga de garrafa",
                Preco = 45.90m,
                DataCriacao = DateTime.UtcNow
            },
            new Produto
            {
                Id = Guid.Parse("b8c9d0e1-f2a3-4567-bcde-678901234567"),
                Nome = "Canjica Junina",
                Descricao = "Canjica especial de festa junina",
                Preco = 15.90m,
                DataCriacao = DateTime.UtcNow
            },
            new Produto
            {
                Id = Guid.Parse("c9d0e1f2-a3b4-5678-cdef-789012345678"),
                Nome = "Pamonha",
                Descricao = "Pamonha tradicional nordestina",
                Preco = 12.90m,
                DataCriacao = DateTime.UtcNow
            },
            new Produto
            {
                Id = Guid.Parse("d0e1f2a3-b4c5-6789-defa-890123456789"),
                Nome = "Suco de Caju",
                Descricao = "Suco natural de caju",
                Preco = 8.90m,
                DataCriacao = DateTime.UtcNow
            }
        };

        await context.Produtos.AddRangeAsync(produtos);
        await context.SaveChangesAsync();
    }

    private static async Task SeedEstoquesAsync(AppDbContext context)
    {
        if (context.Estoques.Any()) return;

        var estoques = new List<Estoque>
        {
            // Fortaleza
            new Estoque
            {
                Id = Guid.NewGuid(),
                IdUnidade = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                IdProduto = Guid.Parse("f6a7b8c9-d0e1-2345-fabc-456789012345"),
                Quantidade = 100,
                DataCriacao = DateTime.UtcNow
            },
            new Estoque
            {
                Id = Guid.NewGuid(),
                IdUnidade = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                IdProduto = Guid.Parse("a7b8c9d0-e1f2-3456-abcd-567890123456"),
                Quantidade = 50,
                DataCriacao = DateTime.UtcNow
            },
            // Recife
            new Estoque
            {
                Id = Guid.NewGuid(),
                IdUnidade = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"),
                IdProduto = Guid.Parse("f6a7b8c9-d0e1-2345-fabc-456789012345"),
                Quantidade = 80,
                DataCriacao = DateTime.UtcNow
            },
            new Estoque
            {
                Id = Guid.NewGuid(),
                IdUnidade = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"),
                IdProduto = Guid.Parse("a7b8c9d0-e1f2-3456-abcd-567890123456"),
                Quantidade = 40,
                DataCriacao = DateTime.UtcNow
            }
        };

        await context.Estoques.AddRangeAsync(estoques);
        await context.SaveChangesAsync();
    }
}