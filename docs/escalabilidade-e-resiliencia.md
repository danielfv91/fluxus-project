# Escalabilidade e Resiliência no Projeto Fluxus

Este documento descreve as estratégias aplicadas no projeto Fluxus para garantir **alta disponibilidade**, **resposta rápida sob carga** e **tolerância a falhas** em pontos críticos da aplicação.

As implementações descritas aqui foram feitas com base nos requisitos não funcionais do desafio, que exigem que o sistema aguente até 50 requisições por segundo com no máximo 5% de perda, e que partes do sistema continuem operando mesmo quando outras estiverem indisponíveis.

---

## Rate Limiting (ASP.NET Core 8)

Para controlar o volume de requisições simultâneas em endpoints sensíveis, foi implementado **Rate Limiting nativo do ASP.NET Core 8**.

### Onde foi aplicado:
- Endpoint de exportação de relatório em PDF: `/api/cashflowreport/daily/report`

### Configuração utilizada:
- **Permitido:** até 50 requisições por segundo
- **Fila de espera:** até 5 requisições
- **Resposta em caso de excesso:** HTTP 429 (Too Many Requests)

### Vantagens:
- Previne sobrecarga no serviço de geração de relatórios
- Controla o uso em momentos de pico
- Melhora a estabilidade geral da aplicação

---

## Cache em Memória (`IMemoryCache`)

Para evitar reprocessamento desnecessário e acelerar o tempo de resposta de chamadas repetidas, foi implementado **cache em memória** para o endpoint de geração de relatórios.

### Estratégia:
- Cache baseado em: `userId`, `DateFrom`, `DateTo`
- Armazenamento por 5 minutos
- Utilização de `IMemoryCache` com fallback simples

### Exemplo de chave:
```
pdf:relatorio:{userId}:{DateFrom}:{DateTo}
```

### Benefícios:
- Reduz o custo computacional de gerar PDFs repetidos
- Melhora o tempo de resposta em chamadas frequentes
- Pode ser substituído facilmente por Redis no futuro

---

## Tolerância a Falhas

O projeto implementa uma estratégia de **tratamento explícito de falhas** para proteger o sistema em cenários adversos, garantindo resiliência e boa comunicação com o cliente.

### Implementações no controller de relatório:
- **Timeout (TaskCanceledException):** retorna `504 Gateway Timeout` com mensagem amigável
- **Erros inesperados:** capturados com `try/catch` e retorno `500 Internal Server Error` com `detail`

### Exemplo de retorno em caso de falha:
```json
{
  "success": false,
  "message": "Erro inesperado ao gerar relatório.",
  "detail": "Exception.Message"
}
```

### Vantagens:
- Garante funcionamento previsível mesmo sob falhas
- Facilita o diagnóstico por quem consome a API
- Evita que falhas internas comprometam o sistema inteiro

---

## Independência entre Serviços

O sistema foi desenhado para que **a queda de uma parte (ex: relatório)** **não afete o funcionamento de outras** (ex: lançamento de transações).

### Como isso é garantido:
- Arquitetura em camadas desacopladas
- Handlers e controllers isolados
- Nenhum serviço depende diretamente do relatório para operar

---

## Testes Automatizados de Resiliência

- Cobertura de testes para cenários de timeout e falhas no relatório
- Simulação de cache hit e miss em testes
- Validação da resposta HTTP correta em todos os casos críticos

---

## Preparação para Escala

O projeto já está pronto para evoluir em direção a uma arquitetura ainda mais escalável:

- **Substituição do cache local por Redis** (distribuído)
- **Uso de Polly** para políticas de Retry e Circuit Breaker
- **Adoção de filas (ex: RabbitMQ)** para geração assíncrona de relatórios

---

## Conclusão

O projeto Fluxus foi estruturado com mecanismos de controle de carga, prevenção de falhas e desacoplamento entre serviços, garantindo que o sistema se mantenha estável, resiliente e preparado para crescimento em ambientes reais.
