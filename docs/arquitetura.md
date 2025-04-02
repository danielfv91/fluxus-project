# Arquitetura do Projeto Fluxus

A aplicação Fluxus foi desenvolvida com uma arquitetura robusta baseada em Domain Driven Design (DDD) e CQRS (Command Query Responsibility Segregation). Este documento detalha claramente essas abordagens, explicando como cada uma contribui para a organização, manutenção e escalabilidade da solução.

---

## Domain Driven Design (DDD)

O Domain Driven Design (DDD) foi adotado no projeto Fluxus com o objetivo de refletir fielmente as regras e necessidades do domínio financeiro. Essa abordagem traz clareza, facilita a comunicação entre desenvolvedores e especialistas de negócio e promove uma arquitetura orientada ao domínio.

### Conceitos principais aplicados:

- **Entidades:** objetos de negócio que possuem identidade única e são essenciais para o domínio, como `Transaction` e `CashFlow`.
- **Value Objects:** objetos que são definidos apenas por seus atributos e valores, não possuem identidade própria e são imutáveis.
- **Repositories:** interfaces abstratas que definem métodos claros para acesso e manipulação dos dados persistidos.
- **Specifications:** padrões para consultas complexas e reutilizáveis ao banco de dados.

---

## Command Query Responsibility Segregation (CQRS)

CQRS é um padrão arquitetural adotado para claramente separar operações que modificam o estado (Commands) das operações que apenas consultam o estado atual do sistema (Queries). A separação simplifica o desenvolvimento, permite otimizações específicas e torna o código mais intuitivo e organizado.

### Como implementamos CQRS:

- **Commands:** Operações de escrita, implementadas através de handlers específicos. Exemplo: criação de transações financeiras.
- **Queries:** Operações de leitura, isoladas em consultas otimizadas e eficientes. Exemplo: listagem e relatórios de fluxo de caixa.

Utilizamos a biblioteca MediatR para facilitar a implementação do CQRS, permitindo um forte desacoplamento entre comandos/queries e os respectivos handlers.

---

## Camadas da Arquitetura

### 1. Camada de Apresentação (`Fluxus.WebApi`)

- Responsável por expor endpoints HTTP REST.
- Controllers com responsabilidade única, claros e enxutos.
- DTOs (Data Transfer Objects) definidos claramente para entrada (Requests) e saída (Responses).
- Validações centralizadas usando FluentValidation.

### 2. Camada de Aplicação (`Fluxus.Application`)

- Implementa os casos de uso do sistema, utilizando o padrão CQRS.
- Commands e Queries claramente separados.
- Handlers responsáveis pela lógica de negócios, fáceis de testar e manter.
- Uso dedicado do AutoMapper para conversão entre DTOs e entidades de domínio.

### 3. Camada de Domínio (`Fluxus.Domain`)

- Centraliza todas as regras, validações e entidades relacionadas ao domínio financeiro.
- Entidades são auto-suficientes e refletem diretamente as regras do negócio.
- Regras de negócio são isoladas de infraestrutura e frameworks.

### 4. Camada de Infraestrutura (`Fluxus.ORM`)

- Responsável pela persistência dos dados utilizando Entity Framework Core.
- Configurações específicas e detalhadas para o banco PostgreSQL.
- Migrations gerenciadas claramente para evolução do banco de dados.

### 5. Camada Comum (`Fluxus.Common`)

- Serviços transversais utilizados por todo o sistema, como geração de relatórios PDF, segurança com JWT e caching.

---

## Diagrama da Solução

Veja o diagrama visual (textual) da arquitetura para melhor compreensão dos relacionamentos e responsabilidades entre as camadas no arquivo `diagrama-da-solucao.md`.

---

Esta documentação deve ser consultada em conjunto com os demais documentos técnicos para melhor compreensão da aplicação e de suas decisões arquiteturais.
