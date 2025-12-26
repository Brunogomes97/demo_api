# API Demo .NET C#

API desenvolvida em **ASP.NET Core (.NET 8)** seguindo uma arquitetura em camadas (`Domain`, `Applications`, `Infrastructure`), com autenticação via **JWT**, persistência em **PostgreSQL** e testes unitários com **xUnit + Moq**.
Se trata de uma api CRUD (Create, Read, Update, Delete) para um caso simples de 2 Entidades: `Users` e `Products`


1. O Frontend da aplicação, pode ser acessada por aqui: [demo_app](https://github.com/Brunogomes97/demo_app)

2. O Script para o app fullstack dockerizado em prod: [demo_scripts](https://github.com/Brunogomes97/demo_scripts)

## 📁 Estrutura do Projeto

```text
demo_api/
├── src/
│   ├── project.API/
│   │   ├── Applications/
│   │   ├── Domain/
│   │   ├── Infrastructure/
│   │   ├── Migrations/
│   │   ├── Properties/
│   │   ├── Program.cs
│   │   └── project.API.csproj
│   │
│   └── project.API.Tests/
│       ├── Applications/Products/
│       ├── GlobalUsings.cs
│       └── project.API.Tests.csproj
│
├── Dockerfile
├── demo_api.sln
└── README.md
```

## 🛠️ Tecnologias e Ferramentas Utilizadas

### Backend
- **Entity Framework Core** — ORM para acesso a dados
- **JWT (JSON Web Token)** — Autenticação stateless
- **Microsoft IdentityModel Tokens** — Criação e validação de tokens JWT
- **Data Annotations** — Validação de modelos e DTOs

### Banco de Dados
- **PostgreSQL** — Banco de dados relacional
- **Npgsql** — Provider PostgreSQL para EF Core

### Arquitetura & Padrões
- **Arquitetura em Camadas (Clean-ish Architecture)**
  - Domain
  - Applications
  - Infrastructure
- **DTOs (Data Transfer Objects)**
- **Repository Pattern**
- **Dependency Injection (DI)**
- **Middleware para tratamento global de exceções**
- **Paginação com offset / limit**
- **Filtros dinâmicos por nome e categoria**

### Segurança
- **JWT Bearer Authentication**
- **Claims-based Identity**
- **User Secrets (Development)**
- **Variáveis de Ambiente (Production)**

### Testes
- **xUnit** — Framework de testes
- **Moq** — Mocking de dependências
- **Testes unitários focados na camada de Service**

### Documentação
- **Swagger / OpenAPI** — Documentação interativa da API

### DevOps / Infra
- **Docker** — Containerização
- **Docker Compose** — Orquestração do PostgreSQL local
- **dotnet CLI** — Build, run, migrations e testes
---

## ⚙️ Pré-requisitos

Antes de começar, você vai precisar ter instalado:

* ✅ .NET SDK 8.0+
* ✅ Docker
* ✅ Docker Compose

Verifique as instalações:

```bash
dotnet --version
docker --version
docker compose version
```

---

## 🐘 Subindo o PostgreSQL com Docker

O banco de dados PostgreSQL é executado via Docker. Pode executar via

```bash
docker run -d \
  --name postgres \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=meubanco \
  -p 5432:5432 \
  postgres:latest
```
---

Caso queria ter acesso ao app completo fullstack em prod dockerizado, utilize os dockerfiles e o script compose para deploy em [demo_scripts](https://github.com/Brunogomes97/demo_scripts)

## 🔐 Configuração de Secrets (JWT)

O projeto utiliza **User Secrets** para ambiente de desenvolvimento.

### 1️⃣ Inicializar User Secrets

```bash
cd src/project.API
dotnet user-secrets init
```

### 2️⃣ Configurar o JWT Secret

```bash
dotnet user-secrets set "JwtSettings:SecretKey" "SUA_SECRET_SUPER_SEGURA_AQUI"
```

📌 **Observações:**

* User Secrets funcionam apenas em **Development**
* Não são versionados
* Sobrescrevem `appsettings.Development.json`

---

## ▶️ Executando a API

```bash
cd src/project.API
dotnet run
```

Ou rodar em modo watch
```bash
dotnet watch run --project src/project.API 
```

A aplicação estará disponível em:

**Swagger:**

* [https://localhost:5014/swagger](https://localhost:5014/swagger)


---

## 🗄️ Migrations (Entity Framework Core)

Criar uma migration:

```bash
dotnet ef migrations add InitialCreate
```

Aplicar no banco de dados:

```bash
dotnet ef database update
```

⚠️ Certifique-se de que o PostgreSQL esteja rodando via Docker.

---

## 🧪 Executando os Testes

Os testes unitários estão no projeto `project.API.Tests`.

Rodar o projeto de testes:

```bash
dotnet test src/project.API.Tests
```

Tecnologias utilizadas nos testes:

* xUnit
* Moq

---

## 🏗️ Build do Projeto

Build de toda a solution:

```bash
dotnet build
```

Build apenas da API:

```bash
dotnet build src/project.API
```



## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
