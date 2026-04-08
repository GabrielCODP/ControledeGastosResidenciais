# 📘 Documentação

Este documento foi criado para auxiliar qualquer pessoa que for executar o projeto pela primeira vez após clonar do GitHub.

---

# ⚠️ Passo 1 — Configurar o appsettings.json/appsettings.Development.json

Antes de rodar o projeto, é obrigatório verificar a **connection string** do banco de dados.

Abra:

```
appsettings.json
```

Localize:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=SEU_BANCO;User Id=SEU_USUARIO;Password=SUA_SENHA;"
}
```

Atualize conforme seu ambiente.

### Exemplo (MYSQL local)

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;DataBase=MyExpenseControl;Uid=root;Pwd=SENHADOBD"
}
```

Checklist importante:

✔ Servidor correto
✔ Nome do banco existente
✔ Usuário com permissão
✔ Senha correta
✔ Porta liberada

Se a connection string estiver errada, a aplicação não irá iniciar corretamente.

---

# 🔐 Passo 2 — Cadastro de usuário (obrigatório)

Antes de utilizar  é necessário cadastrar um usuário.

Fluxo obrigatório:

1️⃣ Registrar usuário

Endpoint:

```
POST/user
```

Exemplo:

```json
{
  "name": "string",
  "age": 0,
  "email": "string",
  "password": "string"
}
```

---

# 🔑 Passo 4 — Login

Após cadastrar o usuário:

Execute login:

```
POST /login
```

Exemplo:

```json
{
  "email": "string@teste.com",
  "password": "string"
}
```

Resposta:

```
JWT Token
```

Esse token será usado nas próximas requisições.

---

# 🪪 Passo 5 — Utilizar token JWT

Todos os endpoints protegidos exigem autenticação.

Adicionar header:

```
Authorization: Bearer SEU_TOKEN_AQUI
```

Sem isso:

```
401 Unauthorized
```
