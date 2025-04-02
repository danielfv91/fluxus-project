# Estratégia de Testes Automatizados no Projeto Fluxus

O projeto Fluxus possui uma base sólida de testes automatizados, com foco em **unidade**, **isolamento**, **velocidade** e **cobertura funcional**. Esta estratégia foi projetada para garantir confiabilidade da aplicação, facilitar refatorações e evitar regressões.

---

## Objetivo da Estratégia

- Validar a lógica de negócio de forma isolada
- Assegurar que handlers, controllers e serviços se comportam corretamente
- Testar cenários positivos e negativos
- Garantir que regressões sejam detectadas rapidamente

---

## Ferramentas Utilizadas

| Ferramenta           | Uso                                                              |
|----------------------|------------------------------------------------------------------|
| **xUnit**            | Framework de testes principal                                    |
| **Moq**              | Mock de dependências como repositórios, cache, serviços externos |
| **FluentAssertions** | Verificações expressivas e legíveis                              |
| **Bogus**            | Geração de dados fake realistas e reutilizáveis                  |

---

## Organização dos Testes

A estrutura de testes segue a mesma divisão modular do projeto:

```
tests/
└── Fluxus.Unit/
    ├── Controllers/
    ├── Features/
    ├── Validators/
    ├── Mocks/
    └── TestData/
```

- `Controllers/`: testa o comportamento dos controllers da WebApi (HTTP, mapeamento, responses)
- `Features/`: testa os handlers (commands/queries) isoladamente
- `Validators/`: testa validadores com entradas válidas e inválidas
- `Mocks/`: helpers para configuração de dependências mockadas
- `TestData/`: classes com dados simulados gerados via Bogus

---

## Abordagens e Técnicas

### Isolamento

Todos os testes de unidade são escritos com dependências mockadas, garantindo foco **apenas na unidade testada**.

### Nomenclatura

Os métodos de teste seguem o padrão:
```
[UnitOfWork]_[Condition]_[ExpectedResult]()
```
Exemplo:
```
AuthenticateUser_ShouldReturnOk_WhenValidRequest()
```

### Testes Positivos e Negativos

- Cenários felizes (sucesso com dados válidos)
- Cenários inválidos (ex: validações falhando)
- Situações excepcionais (ex: falha no cache, timeout)

### Testes com dados dinâmicos

- Uso extensivo do **Bogus** para gerar transações, usuários, datas
- Dados variados ajudam a identificar falhas não óbvias

---

## Exemplos de Casos Cobertos

- Autenticação bem-sucedida e falha por validação
- Criação de transações com mocks de repositórios
- Consolidação de fluxo de caixa com retorno agrupado
- Exportação de relatório: sucesso, cache hit, erro interno e timeout

---

## Execução dos Testes

Para rodar todos os testes:
```bash
dotnet test
```

Os testes também podem ser executados individualmente via Visual Studio ou linha de comando com filtro por categoria.

---

## Conclusão

A estratégia de testes do Fluxus foi projetada para ser robusta, clara e confiável. Com uma combinação de boas práticas, ferramentas modernas e cobertura ampla, os testes automatizados dão segurança para evoluir o sistema com confiança.
