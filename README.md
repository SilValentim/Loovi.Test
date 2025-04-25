# Projeto Loovi.Test

API desenvolvida em .NET 8 seguindo os princípios da Clean Architecture, DDD, 
SOLID e Clean Code, além de principios da arquitetura exagonal. 
O projeto utiliza MediatR, FluentValidation, AutoMapper e autenticação JWT. 
Testes unitários com xUnit, Moq e FluentAssertions.

---

## 📦 Tecnologias

- ASP.NET Core 8
- MediatR
- FluentValidation
- AutoMapper
- JWT Authentication
- Entity Framework Core
- xUnit, Moq, FluentAssertions
- Swagger/OpenAPI

## 📂 Estrutura do Projeto

### Estrutura de pastas

Loovi.Test  
├── src  
│   ├── Loovi.Test.Application     
│   ├── Loovi.Test.Common          
│   ├── Loovi.Test.Domain          
│   ├── Loovi.Test.ORM             
│   ├── Loovi.Test.Unit          
│   └── Loovi.Test.WebApi    
│         
├── README.md                      # Este arquivo  
├── .gitattributes                  
├── .gitignore  
└── Loovi.Test.sln   

### Estrutura da Solução 'Loovi.Test'

#### 📦 Adapters
- Driven
  - Infrastructure
    - Loovi.Test.ORM `Projeto de acesso a dados (ORM) e repositórios`
      - Configuration `Configurações do Entity Framework Core`
      - Migrations `Migrations do banco de dados`
      - Repositories `Implementações de repositórios`
      - DependencyInjection.cs `Injeção de dependências da infraestrutura`

#### 📦 Drivers
- WebApi
  - Loovi.Test.WebApi
  
#### 📦 Core
- Application
  - Loovi.Test.Application `Camada de Application (CQRS Handlers, Validators, Profiles)`
    - Auth `Autenticação de usuários (Handlers, Validators)`
    - Tasks `Gerenciamento de tarefas (Handlers, Validators, Profiles)`
      - Common `Classes comuns relacionadas a Tasks`
      - CreateTask `Comando para criar tarefas`
      - DeleteTask `Comando para excluir tarefas`
      - GetTask `Consulta para buscar uma tarefa`
      - ListTasks `Consulta para listar tarefas`
      - UpdateTask `Comando para atualizar tarefas`
    - DependencyInjection.cs `Injeção de dependências da camada Application`

- Domain
  - Loovi.Test.Domain `Camada de Domínio (Entidades e Interfaces de Repositórios)`
    - Common `Interfaces base e utilitárias`
    - Entities `Entidades de domínio`
    - Repositories `Interfaces de repositórios`

- Crosscutting
  - Loovi.Test.Common `Componentes que cruzam todas as camadas (Auth, Responses, etc)`
    - Auth
      - Configuration `Configurações de autenticação`
      - Interfaces `Interfaces relacionadas à autenticação`
      - Jwt `Implementação de geração de JWT`
      - Models `Modelos (Request, Response, Result) de autenticação`
      - Services `Serviços auxiliares (ex: UserFakeService)`
    - Responses <`Padronização de respostas da API`
    - DependencyInjection.cs `Injeção de dependências para Crosscutting`

#### 📦 Tests
- Unit
  - Loovi.Test.Unit `Projeto de testes unitários`
    - Application
      - Tasks `Testes unitários para tarefas`
        - Commands `Testes dos Handlers de comandos`
        - Queries `Testes dos Handlers de consultas`

## 📋 Recursos Implementados

- [x] Operações sobre as tarefas (Criar, obter, atualizar, deletar e listar tarefas)
- [x] Exclusão lógica de tarefas
- [x] Listagem com paginação, filtros e ordenação
- [x] Autenticação com JWT (modo fake/demo)
- [x] Padronização de respostas da API
- [x] Validações com FluentValidation
- [x] Testes unitários

## 🆚 Comparação Com os Requisitos

### ✅ Requisitos Desejáveis Implementados

- Autenticação JWT.
- Repository Pattern.
- Mapeamento com AutoMapper;

### 🔀 Diferenças

Durante o desenvolvimento alguns itens foram desenvolvidos
 levemente diferentes do solicitado, afim de padronizar e/ou implementar melhorias.
- Nomes das propriedades das tarefas foram traduzidos para inglês.
- Implementado um endpoint para get de tarefa pelo id.
- Implementado dois endpoints para mudança de status da task.

## 🚀 Como rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/SilValentim/Loovi.Test
   ```

2. Acesse a pasta do projeto:
   ```bash
   cd Loovi.Test/src/Loovi.Test.WebApi
   ```

3. Restaure os pacotes:
   ```bash
   dotnet restore
   ```

4. Execute o projeto:
   ```bash
   dotnet run
   ```

5. Acesse a documentação Swagger (/swagger) via navegador ou ferramenta como Postman.

### 🧪 Como rodar os testes

Execute os testes unitários com o seguinte comando:

```bash
dotnet test
```

Ou acesse o gerenciador de testes pelo menu Teste ou atalho CTRL E, T



## 🛠️ Como recriar o banco de dados (Entity Framework Core - Code First)

O projeto utiliza o Entity Framework Core com abordagem **Code First**.  
Para recriar o banco de dados localmente a partir das **migrations existentes**, siga os passos abaixo utilizando o **Console do Gerenciador de Pacotes** (Package Manager Console) do Visual Studio:

1. Certifique-se de que o projeto de inicialização seja `Loovi.Test.WebApi`.

2. No Visual Studio, abra o **Console do Gerenciador de Pacotes**:
   ```
   Exibir > Outras Janelas > Console do Gerenciador de Pacotes
   ```

3. (Opcional) Exclua o banco de dados atual:
   ```powershell
   Drop-Database
   ```

4. Crie o banco de dados e aplique todas as migrations existentes:
   ```powershell
   Update-Database
   ```

> 📌 As migrations estão localizadas no projeto `Loovi.Test.ORM`, na pasta `Migrations`.  
> Caso apareça erro de múltiplos projetos, defina `Loovi.Test.ORM` como **Projeto Padrão** no Console.

## ℹ️ Notas do autor

A estrutura segue vários conceitos do SOLID, inclusive é possível observar a aplicação de todos os cinco princípios. 
Por exemplo, o princípio da responsabilidade 
única aparece na separação clara entre os Handlers, os Validators e os Profiles. 
Cada classe tem uma função bem específica. Já a inversão de dependência é aplicada 
com o uso de interfaces como ITaskItemRepository e IAuthenticationService, o que 
permite manter o domínio isolado da infraestrutura.


Reaproveitei uma implementação que já havia feito para listas e efetuei algumas adaptações.
 Ela foi feita de 
forma genérica, com suporte a paginação, ordenação e filtros. O mais interessante é 
que ela já está preparada pra ser reaproveitada com qualquer nova entidade de domínio — 
ou seja, dá pra replicar o padrão facilmente, sem reescrever a lógica de listagem.

O projeto/solução foi desenvolvido(a) com base na arquitetura hexagonal. Os limites entre 
domínio, aplicação, infraestrutura e camada externa (WebApi) estão bem definidos. 
Se no futuro houver a necessidade plugar uma outra interface, 
a base já está bem organizada para tal.

Por fim, o projeto usa Entity Framework Core com Code First. Isso facilita a evolução do banco. Com as migrations em dia, recriar o banco local ou 
aplicar mudanças é simples, tudo pode ser feito direto pelo console do Package Manager, sem sequer sair do Visual Studio.


## 🙋‍♂️ Autor

Desenvolvido por Leandro Valentim 🚀  
[LinkedIn](https://www.linkedin.com/in/leandro-valentim-ab82a6207/) | [GitHub](https://github.com/SilValentim)