# TaskManager API

API REST desenvolvida em **ASP.NET Core** para gerenciamento de tarefas (tasks), com persistência em **SQLite**.

## Tecnologias utilizadas

- ASP.NET Core 8
- Entity Framework Core 8
- SQLite
- Swagger
- C#

---

## Requisitos para rodar o projeto

### 1. .NET SDK

- **.NET SDK 8.0.417**
- Download oficial:  
  <https://dotnet.microsoft.com/en-us/download/dotnet/8.0>

Verifique se o dotnet está instalado:

```bash
dotnet --version
```

Para instalar no Linux execute:
```bash
sudo apt update && sudo apt install -y dotnet-sdk-8.0
```

O projeto foi desenvolvido utilizando ASP.NET Core 8 e Entity Framework Core 8.

### 2. Banco de Dados

**SQLite**

- Não é necessário instalar servidor ou serviço adicional.
- O banco é criado automaticamente via Entity Framework Core Migrations.
- O arquivo .db será gerado localmente ao rodar as migrations.

---

## Como rodar o projeto

### 1. Clonar o repositório

Caso ainda não tenha feito o clone:

```bash
git clone git@github.com:lrcorrea/magnadata-task-manager-test.git
```

Abra a pasta da API

```bash
cd TaskManager.Api
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 3. Aplicar migrations (criar banco)
```bash
dotnet ef database update
```

Caso não tenha dotnet ef instalado rode o comando:

```bash
dotnet tool install --global dotnet-ef
```

**Configuração do PATH (Necessário no Linux)**: Se o comando dotnet ef não for encontrado após a instalação, rode o comando abaixo para exportar o caminho das ferramentas para o seu terminal:

```bash
export PATH="$PATH:$HOME/.dotnet/tools"
```

### 4. Executar a API

```bash
dotnet run
```

No terminal será exibido em algum lugar:

```bash
Now listening on: http://localhost:8080
```

### 5. Acessar o Swagger

Acesse no navegador:

```bash
http://localhost:8080/swagger
```
