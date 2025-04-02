# Documentação dos Serviços (Casos de Uso)

Este documento descreve os principais **casos de uso (serviços)** disponíveis no projeto Fluxus, com foco em suas responsabilidades, fluxos internos e respectivos endpoints da API.

---

## Autenticação

- **Responsável:** `AuthenticateUserCommandHandler`
- **Endpoint:** `POST /api/auth`
- **Descrição:** Valida as credenciais do usuário e retorna um token JWT se as informações forem válidas.
- **Validação:** Email e senha obrigatórios com `FluentValidation`
- **Fluxo:**
```text
Request → Validator → Handler → AuthService → Token gerado
```

---

## Criação de Transação Financeira

- **Responsável:** `CreateTransactionCommandHandler`
- **Endpoint:** `POST /api/transaction`
- **Descrição:** Registra um novo lançamento de entrada ou saída no fluxo de caixa.
- **Validação:**
  - Campo `type` (debit/credit) obrigatório
  - Valor positivo
- **Fluxo:**
```text
Request → Validator → Handler → UnitOfWork → Repository → Persistência
```

---

## Consolidação Diária do Fluxo de Caixa

- **Responsável:** `ListDailyConsolidationQueryHandler`
- **Endpoint:** `GET /api/transaction/consolidation`
- **Descrição:** Retorna um resumo consolidado por dia com totais de entrada, saída e saldo final.
- **Validação:** Datas válidas (via query string)
- **Fluxo:**
```text
Request → Mapper → Query → Handler → Repository → Agrupamento → DTO Response
```

---

## Listagem do Fluxo de Caixa Diário

- **Responsável:** `DailyCashFlowQueryHandler`
- **Endpoint:** `GET /api/cashflow/daily`
- **Descrição:** Retorna todas as transações de um usuário agrupadas por data, incluindo os saldos parciais.
- **Fluxo:**
```text
Request → Mapper → Query → Handler → Repository → DTO Response
```

---

## Exportação de Relatório em PDF

- **Responsável:** `GenerateDailyCashFlowReportQueryHandler`
- **Endpoint:** `POST /api/cashflowreport/daily/report`
- **Descrição:** Exporta um relatório PDF com o fluxo de caixa consolidado em um período.
- **Validação:** Data inicial e final obrigatórias
- **Recursos adicionais:**
  - Timeout (504) e falha controlada (500)
  - Cache com chave baseada em usuário + intervalo
- **Fluxo:**
```text
Request → Mapper → Query → Handler → Repository → Model → QuestPDF → FileResult
```

---

## Conclusão

Estes serviços representam os principais pontos funcionais da aplicação e são estruturados com base em **DDD + CQRS + MediatR** para garantir clareza, isolamento e testabilidade. Cada handler executa seu caso de uso específico, mantendo o código desacoplado e sustentável.
