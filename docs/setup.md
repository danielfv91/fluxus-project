# Setup do Projeto Fluxus

Este guia descreve os passos necessários para configurar e executar o projeto **Fluxus** em ambiente local.

---

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- PostgreSQL (local ou Docker)
- Visual Studio 2022 ou VS Code (opcional)

---

## Configurando os segredos do usuário (User-Secrets)

O projeto utiliza **user-secrets** para manter dados sensíveis fora do código-fonte.

Abra o terminal na pasta do projeto WebApi e execute:

```bash
dotnet user-secrets set "Seed:AdminEmail" "email@email.com"
dotnet user-secrets set "Seed:AdminPassword" "suasenha"
dotnet user-secrets set "Seed:AdminName" "Administrador"
dotnet user-secrets set "Seed:AdminRole" "Admin"
dotnet user-secrets set "Jwt:SecretKey" "superseguro-chave-jwt-fluxus"
```

---

## Configurando o banco de dados

Certifique-se de que o PostgreSQL está rodando e acessível.

### Exemplo de connection string:

```
Host=localhost;Port=5432;Database=fluxus_db;Username=fluxus_user;Password=fluxus_pass
```

Essa string deve estar em `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "sua-string-aqui"
}
```

---

## Aplicando a migration

Rode os comandos abaixo no terminal ou Package Manager Console do Visual Studio:

```bash
dotnet ef database update --project src/Fluxus.ORM --startup-project src/Fluxus.WebApi
```

---

## Executando a aplicação

```bash
dotnet run --project src/Fluxus.WebApi
```

A aplicação estará disponível em:
```
https://localhost:<porta>/swagger
```

---

## Teste inicial

1. Acesse o Swagger
2. Vá até o endpoint `POST /api/auth`
3. Use as credenciais:

```json
{
  "email": "email@email.com",
  "password": "suasenha"
}
```

Você receberá um **JWT token válido**.

Utilize esse token para testar os endpoints protegidos adicionando-o como `Bearer Token` no Swagger.

---

Caso precise resetar o banco:
```sql
DROP DATABASE fluxus_db;
CREATE DATABASE fluxus_db;
```

---

Para mais detalhes sobre autenticação, veja `docs/auth.md`
Para visão técnica da arquitetura, veja `docs/architecture.md`
