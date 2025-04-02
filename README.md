# Fluxus - Controle de Fluxo de Caixa

## Visão Geral

O **Fluxus** é uma aplicação ASP.NET Core para controle de **lançamentos financeiros** (débitos e créditos), com **consolidação diária** e **relatório em PDF**.

Criado como solução para um desafio de arquitetura, o sistema prioriza **boas práticas**, **escalabilidade**, **resiliência** e **testabilidade**.

---

## Requisitos Atendidos

- Controle de lançamentos (crédito/débito)
- Consolidação de saldos diários
- Relatório em PDF com saldo consolidado
- Respostas padronizadas em toda API (envelopamento)
- Rate limiting com tolerância a falhas (PDF)
- Cache em memória para relatórios já gerados
- Testes unitários cobrindo Services e Controllers
- Autenticação com JWT e controle de usuário

---

## Configuração do Projeto

Este guia irá ajudá-lo a configurar rapidamente o projeto no seu ambiente local.

---

### Pré-requisitos

Antes de começar, certifique-se de ter as seguintes ferramentas instaladas:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) ou [Visual Studio Code](https://code.visualstudio.com/)
- [PostgreSQL](https://www.postgresql.org/download/) (recomenda-se PgAdmin incluso na instalação)
- [Git](https://git-scm.com/downloads)

---

### Configuração Inicial

#### 1. Clone o Repositório

```bash
git clone https://github.com/danielfv91/fluxus-project.git 
cd fluxus-project
```

#### 2. Configure o Banco de Dados PostgreSQL

Certifique-se de que o PostgreSQL está rodando e acessível.
- Abra o PgAdmin e crie um novo banco de dados com o nome `fluxus_db`. 

#### 3. Configure o appsettings.json

No arquivo `appsettings.json` localizado em `src/Fluxus.WebApi/appsettings.json`, atualize a string de conexão com suas credenciais do PostgreSQL: 

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=fluxus_db;User Id=postgres;Password=sua_senha;"
}
```

##### Configurando os segredos do usuário (User-Secrets)

O projeto utiliza **user-secrets** para manter dados sensíveis fora do código-fonte.

Abra o terminal na pasta do projeto WebApi e execute:

```bash
dotnet user-secrets set "Seed:AdminEmail" "email@email.com"
dotnet user-secrets set "Seed:AdminPassword" "suasenha"
dotnet user-secrets set "Seed:AdminName" "Administrador"
dotnet user-secrets set "Jwt:SecretKey" "superseguro-chave-jwt-fluxus"
```

Deixe cada campo que são alimentados pela user-secrets com "", como no exemplo:

```json
"Jwt": {
  "SecretKey": ""
},
"Seed": {
  "AdminEmail": "",
  "AdminPassword": "",
  "AdminName": ""
}
```

#### 4. Aplicar Migrations do EF Core

No **Package Manager Console** do Visual Studio, execute:

```powershell
Update-Database -Project Fluxus.ORM -StartupProject Fluxus.WebApi 
```

Isso criará automaticamente as tabelas necessárias no seu banco de dados PostgreSQL.

---

### Executando a Aplicação

- No Visual Studio, defina o projeto `Fluxus.WebApi` como projeto de inicialização e pressione **F5**. 

- A aplicação será iniciada via HTTPS (`https://localhost:<porta>`), abrindo automaticamente a interface Swagger.

### Problema com Certificado HTTPS

Se ocorrer o erro **"Sua conexão não é particular"** (`ERR_CERT_INVALID`), execute os seguintes comandos no prompt (como administrador):

```bash
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

Reinicie o Visual Studio e execute novamente.

---

## Executando os Testes

Você pode rodar os testes diretamente pelo Visual Studio ou pelo terminal:

```bash
dotnet test
```

Todos os testes devem passar com sucesso. 
Incluem controllers, comandos, queries, validações e serviços de PDF.

---

## Tecnologias Utilizadas

- ASP.NET Core 8
- C# moderno (DateOnly, global usings, etc)
- MediatR (CQRS)
- AutoMapper
- QuestPDF (relatório)
- FluentValidation
- Bogus (dados fake em testes)
- xUnit + Moq + FluentAssertions
- IMemoryCache + Rate Limiting (nativos do .NET 8)
- PostgreSQL + EF Core

---

## Estrutura do Projeto

```
Fluxus
├── src
│   ├── Fluxus.WebApi            # Camada de entrada (controllers, requests, responses)
│   ├── Fluxus.Application       # Casos de uso (handlers, commands, queries)
│   ├── Fluxus.Domain            # Entidades e regras de negócio (DDD)
│   ├── Fluxus.ORM               # Infraestrutura de dados (EF Core, Migrations)
│   └── Fluxus.Common            # PDF, segurança e utilitários comuns
├── tests
│   └── Fluxus.Unit              # Testes unitários
├── docs                         # Documentos do projeto Fluxus
```

---

## Convenções e boas práticas

- Padrão de commit semântico (`feat`, `fix`, `test`, `docs`, etc.)
- Arquitetura DDD com separação clara entre camadas
- CQRS com MediatR para desacoplamento de lógica
- Handlers simples e testáveis
- AutoMapper segregado por camada
- Validações com FluentValidation
- Controllers enxutos e focados em orquestração

---

## Documentações adicionais

Veja a pasta `docs/` para:

-(`arquitetura.md`): detalhamento das camadas, aplicação de DDD e CQRS

-(`auth.md`): explicação do fluxo de autenticação com JWT

-(`diagrama-da-solucao.md`): diagrama textual representando a arquitetura

-(`escalabilidade-e-resiliencia.md`): mecanismos de tolerância a falhas, cache e rate limiting

-(`estrategia-de-testes.md`): organização, ferramentas e cobertura dos testes automatizados

-(`melhorias-futuras.md`): sugestões realistas de evolução do sistema

-(`padroes-e-praticas.md`): padrões aplicados (MediatR, AutoMapper, FluentValidation, etc.)

-(`requisitos-atendidos.md`): como cada requisito do desafio foi contemplado

-(`tech-stack.md`): tecnologias utilizadas e justificativas
-(`servicos.md`): casos de uso dos serviços disponíveis no projeto Fluxus

---

## Autor

Desenvolvido por **Daniel Vasconcelos**.

Repositório: [https://github.com/danielfv91/fluxus-project](https://github.com/danielfv91/fluxus-project)
