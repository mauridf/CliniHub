# CliniHub - Sistema de Gestão de Clínicas Médicas

![image](https://github.com/user-attachments/assets/7a940c2f-84f6-4d4b-a8d4-80843cff2c36)


## 📌 Visão Geral
O CliniHub é um sistema completo para gestão de clínicas médicas, oferecendo:

- 🏥 Agendamento de consultas e exames
- 👨‍⚕️ Gestão de profissionais de saúde
- 📋 Prontuários eletrônicos
- 📊 Laudos e receituários digitais
- 🔐 Controle de acesso por perfis

## 🚀 Tecnologias Utilizadas

### Backend
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- AutoMapper
- FluentValidation

## ⚙️ Configuração do Ambiente

### Pré-requisitos
- .NET 9 SDK
- PostgreSQL 15+

### Instalação

Clone o repositório:

```bash
git clone https://github.com/seu-usuario/CliniHub.git
cd CliniHub
```

Configure o banco de dados:

Edite o arquivo `appsettings.json` com suas credenciais do PostgreSQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=5432;Database=CliniHub;User Id=postgres;Password=sua-senha;"
}
```

Execute as migrações:

```bash
dotnet ef database update
```

Inicie a aplicação:

```bash
dotnet run
```

## 📚 Estrutura do Projeto

```
CliniHub/
├── CliniHub.Api/           # Camada de apresentação (API)
├── CliniHub.Application/   # Casos de uso e serviços
├── CliniHub.Core/          # Domínio e interfaces
├── CliniHub.Infrastructure # Implementações de infraestrutura
└── CliniHub.Tests/         # Testes unitários e de integração
```

## 🔐 Autenticação

O sistema utiliza JWT (JSON Web Tokens) para autenticação. Exemplo de requisição:

```bash
curl -X POST "https://localhost:5001/api/auth/login"      -H "Content-Type: application/json"      -d '{"email":"admin@clinic.com","password":"Senha@123"}'
```

## 📝 Documentação da API

Acesse a documentação Swagger em:

```
https://localhost:5001/swagger
```

## 🧪 Testes

Execute os testes com:

```bash
dotnet test
```

## 🌟 Recursos Principais

| Módulo         | Funcionalidades                                 |
|----------------|--------------------------------------------------|
| Autenticação   | Login, Registro, Recuperação de senha            |
| Agenda         | Agendamentos, Bloqueios, Disponibilidade         |
| Pacientes      | Cadastro, Histórico, Documentos                  |
| Médicos        | CRM, Especialidades, Laudos                      |
| Administração  | Usuários, Permissões, Configurações              |

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie sua branch (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Distribuído sob a licença MIT. Veja LICENSE para mais informações.

## ✉️ Contato

Equipe CliniHub - contato@clinhub.com.br
