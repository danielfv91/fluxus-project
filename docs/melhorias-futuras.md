# Melhorias Futuras no Projeto Fluxus

Este documento reúne sugestões de melhorias futuras para o projeto Fluxus. Todas são **práticas, incrementais e realistas**, com potencial direto de evolução do sistema dentro do domínio de controle financeiro e fluxo de caixa.

As sugestões levam em conta o atual nível de maturidade técnica do sistema, a arquitetura já implementada e as necessidades típicas de soluções deste nicho.

---

## Migração para Cache Distribuído (Redis)

### Situação atual:
- Utilização de `IMemoryCache`, limitado à instância local

### Melhoria:
- Substituir por Redis via `IDistributedCache`
- Permitir cache compartilhado entre múltiplas instâncias

### Benefícios:
- Suporte à escalabilidade horizontal
- Persistência entre reinicializações
- Maior controle de expiração

---

## Implementação de Polly (Retry, Timeout e Circuit Breaker)

### Situação atual:
- Tratamento de falhas customizado no controller

### Melhoria:
- Utilizar Polly para políticas avançadas de resiliência:
  - Retry automático em falhas transitórias
  - Timeout configurado por endpoint
  - Circuit Breaker para proteger serviços sobrecarregados

### Benefícios:
- Estabilidade sob pressão
- Respostas consistentes
- Proteção contra falhas em cascata

---

## Sistema de Notificações Inteligentes

### Ideia:
- Implementar alertas automatizados com base em condições específicas do sistema

### Exemplos:
- Notificar o usuário quando o saldo do fluxo de caixa estiver negativo ou abaixo de um limite
- Enviar lembretes de movimentações não registradas até certo horário

### Tecnologias sugeridas:
- E-mail (SMTP)
- Notificações push
- Integração futura com WhatsApp ou Telegram

---

## Agendamento de Relatórios (Job Scheduler)

### Funcionalidade futura:
- Permitir que o usuário agende a geração automática de relatórios diários ou semanais

### Tecnologias:
- Quartz.NET
- Hangfire

### Benefícios:
- Aumenta automação do sistema
- Reduz dependência de requisições manuais

---

## Painel Administrativo com Dashboard

### Ideia:
- Criar uma interface web para visualizar:
  - Fluxo de caixa consolidado
  - Alertas recentes
  - Status de geração de relatórios

### Benefícios:
- Visualização clara da saúde financeira
- Facilita decisões operacionais


---

## Conclusão

As melhorias listadas visam expandir a capacidade técnica e funcional do projeto, respeitando sua arquitetura atual. Elas podem ser implementadas de forma progressiva e agregam alto valor ao sistema, principalmente por estarem alinhadas às reais necessidades de sistemas financeiros corporativos.
