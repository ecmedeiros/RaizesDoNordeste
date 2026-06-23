# Raízes do Nordeste - API

API Back-end para gerenciamento da rede de lanchonetes **Raízes do Nordeste**, desenvolvida como Projeto Multidisciplinar da trilha Back-end da UNINTER (2026).

---

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Git

> Não é necessário instalar nenhum banco de dados. O projeto utiliza **SQLite**, que é criado automaticamente ao rodar as migrations.

---

## Tecnologias utilizadas

- ASP.NET Core 10 (Web API)
- Entity Framework Core + SQLite
- JWT Bearer Authentication
- BCrypt.Net para hash de senhas
- NSwag para documentação OpenAPI/Swagger

---

## Estrutura do projeto

```
RaizesDoNordeste/
├── RaizesDoNordeste.API            → Controllers, Middlewares, Program.cs
├── RaizesDoNordeste.Application    → Services, DTOs
├── RaizesDoNordeste.Domain         → Entidades, Interfaces, Enums
└── RaizesDoNordeste.Infrastructure → Repositories, DbContext, Migrations, Mocks
```

---

## Como configurar

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/RaizesDoNordeste.git
cd RaizesDoNordeste
```

### 2. Configure as variáveis de ambiente

Crie um arquivo `appsettings.Development.json` dentro de `RaizesDoNordeste.API` com o seguinte conteúdo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=raizesdonordeste.db"
  },
  "Jwt": {
    "Key": "RaizesDoNordeste@ChaveSecreta2026!MinimoDe32Caracteres",
    "Issuer": "RaizesDoNordeste",
    "Audience": "RaizesDoNordeste"
  }
}
```

> O arquivo `appsettings.Development.json` já está no `.gitignore` para não expor dados sensíveis.

---

## Como instalar as dependências

```bash
dotnet restore
```

---

## Como criar o banco e executar as migrations

O banco é criado automaticamente ao iniciar a aplicação via `MigrateAsync()` no `Program.cs`. Mas se preferir rodar manualmente pelo terminal:

```bash
dotnet ef database update --project RaizesDoNordeste.Infra --startup-project RaizesDoNordeste.API
```

Ou pelo Package Manager Console no Visual Studio:

```
Update-Database -Project RaizesDoNordeste.Infra -StartupProject RaizesDoNordeste.API
```

> O seed é executado automaticamente na primeira inicialização, criando dados de teste prontos para uso.

---

## Dados do seed

### Usuários disponíveis para teste

| Perfil  | E-mail                              | Senha        |
|---------|-------------------------------------|--------------|
| Admin   | admin@raizesnordeste.com.br         | Admin@123    |
| Gerente | gerente@raizesnordeste.com.br       | Gerente@123  |
| Cliente | cliente@teste.com.br                | Cliente@123  |

### Unidades disponíveis

| Nome                                    | ID                                   |
|-----------------------------------------|--------------------------------------|
| Raízes do Nordeste - Fortaleza Centro   | a1b2c3d4-e5f6-7890-abcd-ef1234567890 |
| Raízes do Nordeste - Recife Boa Viagem  | b2c3d4e5-f6a7-8901-bcde-f12345678901 |

### Produtos disponíveis

| Nome          | ID                                   | Preço  |
|---------------|--------------------------------------|--------|
| Baião de Dois | f6a7b8c9-d0e1-2345-fabc-456789012345 | R$ 29,90 |
| Carne de Sol  | a7b8c9d0-e1f2-3456-abcd-567890123456 | R$ 45,90 |
| Canjica Junina (sazonal) | b8c9d0e1-f2a3-4567-bcde-678901234567 | R$ 15,90 |
| Pamonha (sazonal) | c9d0e1f2-a3b4-5678-cdef-789012345678 | R$ 12,90 |
| Suco de Caju  | d0e1f2a3-b4c5-6789-defa-890123456789 | R$ 8,90  |

---

## Como iniciar a API

```bash
cd RaizesDoNordeste.API
dotnet run
```

A API estará disponível em:
```
https://localhost:7000
http://localhost:5000
```

> A porta pode variar. Verifique o terminal após iniciar.

---

## Como acessar a documentação

Com a API rodando, acesse no navegador:

```
https://localhost:{porta}/swagger
```

A documentação lista todos os endpoints disponíveis com exemplos de request/response e permite testar autenticação JWT diretamente pelo Swagger UI.

---

## Fluxo principal (Fluxo A)

O fluxo crítico implementado é:

**Pedido → Pagamento Mock → Atualização de Status**

### Passo a passo

**1. Autenticar**
```
POST /api/auth/login
{
    "email": "cliente@teste.com.br",
    "senha": "Cliente@123"
}
→ copie o accessToken retornado
```

**2. Criar pedido**
```
POST /api/pedidos
Authorization: Bearer {token}
{
    "idCanalPedido": 1,
    "ehEntrega": false,
    "idUnidade": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
    "usarPontos": false,
    "observacao": "Sem cebola por favor",
    "itensPedido": [
        {
            "idProduto": "f6a7b8c9-d0e1-2345-fabc-456789012345",
            "quantidade": 2
        }
    ]
}
→ copie o id retornado
```

**3. Processar pagamento mock**
```
POST /api/pagamentos
Authorization: Bearer {token}
{
    "idPedido": "{id do pedido}",
    "formaPagamento": "MOCK"
}
→ 80% de chance de aprovação
```

**4. Atualizar status (como Gerente)**
```
PATCH /api/pedidos/{id}/status
Authorization: Bearer {token de gerente}
Body: 3  (Em Preparo)
```

---

## Endpoints disponíveis

| Recurso       | Método | Rota                          | Perfil               |
|---------------|--------|-------------------------------|----------------------|
| Auth          | POST   | /api/auth/login               | Público              |
| Auth          | POST   | /api/auth/registro            | Público              |
| Produtos      | GET    | /api/produtos                 | Público              |
| Produtos      | GET    | /api/produtos/{id}            | Público              |
| Produtos      | POST   | /api/produtos                 | Admin, Gerente       |
| Produtos      | PUT    | /api/produtos/{id}            | Admin, Gerente       |
| Produtos      | DELETE | /api/produtos/{id}            | Admin                |
| Unidades      | GET    | /api/unidades                 | Público              |
| Unidades      | GET    | /api/unidades/{id}            | Público              |
| Pedidos       | POST   | /api/pedidos                  | Cliente, Admin, Gerente |
| Pedidos       | GET    | /api/pedidos                  | Admin, Gerente       |
| Pedidos       | GET    | /api/pedidos/{id}             | Autenticado          |
| Pedidos       | PATCH  | /api/pedidos/{id}/status      | Admin, Gerente       |
| Pedidos       | PATCH  | /api/pedidos/{id}/cancelar    | Autenticado          |
| Pagamentos    | POST   | /api/pagamentos               | Autenticado          |
| Estoques      | GET    | /api/estoques/{idUnidade}     | Admin, Gerente       |
| Estoques      | POST   | /api/estoques/entrada         | Admin, Gerente       |
| Estoques      | POST   | /api/estoques/saida           | Admin, Gerente       |
| Fidelidade    | GET    | /api/fidelidade/{idUsuario}   | Autenticado          |
| Fidelidade    | POST   | /api/fidelidade/resgatar      | Cliente              |

---

## Canais de pedido

| ID | Canal  |
|----|--------|
| 1  | APP    |
| 2  | TOTEM  |
| 3  | BALCAO |
| 4  | WEB    |

---

## Status do pedido

| ID | Status           |
|----|------------------|
| 1  | Pedido Realizado |
| 2  | Confirmado       |
| 3  | Em Preparo       |
| 4  | Pronto           |
| 5  | Em Entrega       |
| 6  | Concluído        |
| 7  | Cancelado        |

---

## Status do pagamento

| ID | Status                |
|----|-----------------------|
| 1  | Aguardando Pagamento  |
| 2  | Pagamento Aprovado    |
| 3  | Pagamento Recusado    |
| 4  | Reembolso Solicitado  |
| 5  | Reembolsado           |

---

## Padrão de erro

Todos os erros seguem o mesmo formato JSON:

```json
{
    "error": "NOME_DO_ERRO",
    "message": "Mensagem legível",
    "timestamp": "2026-06-20T12:00:00Z",
    "path": "/api/rota"
}
```

| Código | Descrição                        |
|--------|----------------------------------|
| 400    | Erro de validação                |
| 401    | Não autenticado                  |
| 403    | Sem permissão                    |
| 404    | Recurso não encontrado           |
| 409    | Conflito / regra de negócio      |
| 422    | Dados inválidos                  |
| 500    | Erro interno do servidor         |

---

## Observações

- O pagamento é simulado (mock) com 80% de chance de aprovação
- Pontos de fidelidade são acumulados apenas para usuários com consentimento
- Produtos sazonais só ficam disponíveis no período configurado
- O estoque é por unidade, cada lanchonete tem seu próprio controle
- Senhas são armazenadas com hash BCrypt
- Todos os endpoints protegidos exigem token JWT no header `Authorization: Bearer {token}`
