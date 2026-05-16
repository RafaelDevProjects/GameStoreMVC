# GameStoreMVC - Checkpoint 6 - C# Development - FIAP 3ESPR

## Integrantes

| Nome | RM |
|---|---|
| Rafael Almeida | RM554019 |
| Giovanna Franco | RM553701  |
| Rafael Jorge | RM552765 |

## Como Executar

### Pre-requisitos
- .NET 8 SDK
- MySQL Server

### 1. Configurar Banco de Dados
Edite `appsettings.json` e atualize a connection string:
```json
"DefaultConnection": "Server=localhost;Database=GameStoreDB;User=root;Password=SUA_SENHA;"
```

### 2. Restaurar Pacotes
```bash
dotnet restore
```

### 3. Executar (o banco e criado automaticamente)
```bash
dotnet run
```

Acesse: http://localhost:5128

### 4. Credenciais Admin Padrao
- Email: admin@gamestore.com
- Senha: Admin@123

---

## Estrutura do Projeto (MVC + Repositorios + Interfaces)
```
GameStoreMVC/
├── Controllers/
│   ├── HomeController.cs       # Listagem de jogos com filtro por categoria
│   ├── LoginController.cs      # Autenticacao (BCrypt + Claims)
│   └── GameController.cs       # CRUD de jogos (somente Admin)
├── Models/
│   ├── Usuario.cs              # Entidade de usuario
│   └── Game.cs                 # Entidade de jogo
├── ViewModels/
│   └── AuthViewModels.cs       # LoginViewModel / CadastroViewModel
├── Data/
│   └── AppDbContext.cs         # EF Core DbContext
├── Interfaces/
│   ├── IUsuarioRepository.cs
│   └── IGameRepository.cs
├── Repositories/
│   ├── UsuarioRepository.cs
│   └── GameRepository.cs
└── Views/
    ├── Home/Index.cshtml       # Pagina inicial com hero, categorias, cards
    ├── Login/
    │   ├── Login.cshtml        # Tela de login
    │   ├── Cadastro.cshtml     # Tela de cadastro
    │   └── AcessoNegado.cshtml
    ├── Game/
    │   ├── Criar.cshtml        # Cadastro de novo jogo
    │   └── Editar.cshtml       # Edicao de jogo
    └── Shared/
        ├── _Layout.cshtml      # Layout principal (Bootstrap + tema dark gaming)
        └── _GameCard.cshtml    # Partial de card de jogo
```

## Funcionalidades
- **Gestao de Usuarios**: Cadastro com BCrypt, Login com Cookie + Claims
- **Autorizacao por Role**: Admin ve botoes Editar/Excluir/Novo Jogo
- **CRUD de Games**: Criar, listar, editar, excluir jogos
- **Filtro por Categoria**: RPG, Acao, Corrida, Aventura
- **Destaque**: Jogos marcados aparecem na secao Em Destaque
- **Responsivo**: Bootstrap 5 + layout adaptado para mobile
- **MySQL + EF Core**: Persistencia de dados com migrations automaticas

## Seguranca
- Senhas criptografadas com BCrypt.Net
- Autenticacao via Cookie com Claims
- Autorizacao por Role (Admin)
- Anti-forgery tokens em todos os formularios POST

