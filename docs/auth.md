# Autenticação no Projeto Fluxus

O projeto Fluxus utiliza autenticação baseada em **JWT (JSON Web Token)** com geração de token após login e proteção dos endpoints via `Bearer Token`.

---

## Endpoint de login

```
POST /api/auth
```

### Requisição
```json
{
  "email": "email@email.com",
  "password": "suasenha"
}
```

### Resposta
```json
{
  "token": "<jwt-token>",
  "email": "email@email.com",
  "name": "Administrador",
  "role": "Admin"
}
```

---

## Segurança do Token

- O token é assinado com chave simétrica definida em `Jwt:SecretKey`
- Expira após 1 hora
- Inclui as seguintes claims:
  - `sub` → ID do usuário
  - `email`
  - `name`
  - `role`

---

## Protegendo endpoints

Adicione o atributo `[Authorize]` nos controllers ou endpoints que devem exigir autenticação.
Exemplo:

```csharp
[Authorize]
[HttpGet("protegido")]
public IActionResult GetDadosProtegidos()
{
    return Ok("Você está autenticado!");
}
```

---

## Testando via Swagger

1. Acesse `POST /api/auth` e envie credenciais válidas
2. Copie o token retornado
3. Clique em **Authorize** no topo do Swagger
4. Cole o token como:
```
Bearer <seu-token-aqui>
```
5. Teste os endpoints protegidos normalmente

---

## Configurações relacionadas

### `appsettings.json`
```json
"Jwt": {
  "SecretKey": "sua-chave-super-secreta"
}
```

### Registro no `Program.cs`
```csharp
builder.Services.AddJwtAuthentication(builder.Configuration);
```

---

Mais detalhes sobre arquitetura geral do projeto: `docs/architecture.md`
