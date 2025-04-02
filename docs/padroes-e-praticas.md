# Padrões e Práticas Técnicas Aplicadas no Projeto Fluxus

Este documento descreve os principais padrões de projeto, bibliotecas e boas práticas aplicadas ao desenvolvimento do projeto Fluxus. Todas essas escolhas foram tomadas com foco em escalabilidade, manutenibilidade, testabilidade e aderência a padrões modernos de desenvolvimento backend em .NET.

---

## MediatR - Mediador para CQRS

O [MediatR](https://github.com/jbogard/MediatR) é uma biblioteca baseada no padrão *Mediator*, que permite desacoplar o envio de comandos e consultas de seus respectivos manipuladores. No Fluxus, ele é a base para a implementação do padrão **CQRS**.

### Como foi utilizado:

- **Commands** e **Queries** implementam interfaces do MediatR
- Cada `Handler` contém a lógica de negócio isolada para o respectivo uso
- Os controllers não sabem quem trata o comando – apenas o enviam para o MediatR

### Benefícios:
- Forte **desacoplamento** entre camadas
- Facilita **testes unitários** dos casos de uso
- Torna a orquestração de regras mais clara e modular

---

## AutoMapper - Mapeamento de Objetos

[AutoMapper](https://automapper.org/) é uma biblioteca de mapeamento de objetos que evita código repetitivo ao transformar entre entidades, DTOs e modelos.

### Estratégia aplicada:

- Mapeamentos de entrada/saída definidos exclusivamente na camada `WebApi`
- Mapeamentos internos (DTOs e entidades) ficam na `Application`
- A camada `Domain` **não conhece** AutoMapper

### Exemplos:
- `TransactionRequest` → `CreateTransactionCommand`
- `Transaction` → `TransactionResponse`

### Benefícios:
- **Redução de boilerplate**
- **Segregação de responsabilidades** por camada
- Facilita manutenção e clareza no fluxo de dados

---

## FluentValidation - Validações Centralizadas

[FluentValidation](https://docs.fluentvalidation.net/) foi adotado como padrão para validar DTOs de entrada.

### Aplicação no projeto:
- Cada `Request` possui sua respectiva `Validator`
- Validações aplicadas antes da chamada ao handler
- Retorno padronizado de erros no modelo `ApiResponse`

### Exemplo:
- `AuthenticateUserRequestValidator`
- `CreateTransactionRequestValidator`

### Benefícios:
- Código limpo e expressivo para validações complexas
- Separação clara entre validação e lógica de negócio
- Padronização das regras de entrada

---

## Bogus - Dados Fake para Testes

[Bogus](https://github.com/bchavez/Bogus) é utilizado para gerar dados simulados de forma simples, flexível e rica em variedade.

### Estratégia aplicada:
- Criação de `FakerBuilder` para cada entidade testável
- Ex: `SaleFakerBuilder`, `GenerateDailyCashFlowReportTestData`

### Benefícios:
- Facilidade de manutenção dos testes
- Dados realistas e automatizados
- Reutilização em diversos cenários de teste

---

## Boas Práticas de Desenvolvimento

### SOLID e Clean Code
- Classes pequenas com responsabilidade única
- Dependências injetadas (Inversão de Controle)
- Nenhuma regra de negócio nos controllers

### Commit Semântico + Git Flow
- Branches nomeadas por contexto (ex: `feature/relatorio-cache`)
- Commits claros como `feat`, `fix`, `test`, `refactor`, `docs`

### Respostas Padronizadas na API
- Uso do wrapper `ApiResponse` e `ApiResponseWithData`
- Melhora consistência e comunicação com clientes da API

---

## Conclusão

Os padrões e práticas aqui descritos foram fundamentais para garantir uma arquitetura limpa, manutenível, desacoplada e testável. A combinação de MediatR, AutoMapper, FluentValidation e Bogus, juntamente com boas práticas como SOLID, contribuiu para a qualidade técnica do projeto Fluxus.
