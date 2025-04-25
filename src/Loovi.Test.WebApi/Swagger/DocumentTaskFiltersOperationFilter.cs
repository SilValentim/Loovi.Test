using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.Intrinsics.X86;

namespace Loovi.Test.WebApi.Swagger
{
    public class DocumentTaskFiltersOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var endpointPath = context.ApiDescription.RelativePath?.ToLower();
            var httpMethod = context.ApiDescription.HttpMethod?.ToLower();

            if (endpointPath == "api/task")
            {
                if (httpMethod == "get")
                {
                    operation.Description ??= string.Empty;
                    operation.Description += @"

## Filtros disponíveis

É possível aplicar filtros, ordenação e paginação usando query strings:

### Filtros por propriedade
### Texto:
- title=abc: Retorna registros onde `tile é exatamente abc`.
- title=*abc*: Retorna registros onde `tile contém abc`.
- title=*abc: Retorna registros onde `tile termina em abc`.
- title=abc*: Retorna registros onde `tile começa em abc`.  

### Data:  
- createdAt=2024-01-01: Retorna registros onde `createdAt é 2024-01-01`  
- createdAt=>2024-01-01: Retorna registros onde `createdAt > 2024-01-01`  
- createdAt=>=2024-01-01&createdAt=<=2024-01-04: Retorna registros `createdAt >= 2024-01-01 e createdAt <= 2024-01-04 `.  

### Outros tipos(ex: numero, Guid, booleano):
- status=1: filtra onde `status == 1`
- active=true: filtra onde `active == true`

    ### Múltiplo valores(OR implícito):
- status=1&status=2: filtra onde `status == 1 || status == 2`

### Ordenação
Use o parâmetro especial `_order`:

- _order=name: ordena por `name` ascendente
- _order=name desc: ordena por `name` descendente
- _order=createdAt desc, name: ordena por `createdAt` desc e depois por `name` asc

### Paginação
Use os parâmetros especiais:

- _page=1 (padrão: 1)
- _size=10 (padrão: 10)

Retorno será paginado com:
- TotalItems
- CurrentPage
- TotalPages
- Items[]

### Exemplos de URLS
- Filtrar tarefas por título exato: `/api/task?Title=Relatório Final`
- Filtrar tarefas por título que começa com 'Rel': `/api/task?Title=Rel*`
- Filtrar tarefas por título que contém 'final': `/api/task?Title=*final*`
- Filtrar por data de vencimento maior ou igual a 1º de maio de 2025: `/api/task?DueDate=>=2025-05-01` 

## Teste pelo Swagger
Para testar pelo swagger, basta enviar o json dessa forma:
```
{  
    ""nomeDaPropriedade1"": [  
        ""valorASerFiltrado""  
    ],   
    ""nomeDaPropriedade2"": [  
        ""valorASerFiltrado""  
    ],   
    ""nomeDaPropriedade3ComMaisDeUmValor"": [  
        ""valorASerFiltrado"",
        ""valor2ASerFiltrado""  
    ]
}  
```
**Title exatamente igual a Cortar a Grama**
```
{
    ""title"": [
    ""Cortar a Grama""
    ]
}
```

**Title começa por Obra**
```
{
    ""title"": [
    ""Obra*""
    ]
}
```

**Description contém 'obra' ou 'armazem'**
```
{
    ""title"": [
    ""*obra*"",
    ""*armazen*""
    ]
}
```

**CreatedAt entre 2024-01-01 e 2024-01-04 inclusive**
```
{  
    ""createdAt"": [  
    "">=2024-01-01"",
    ""<=2024-01-04""
    ],  
}
```
";
                }
                else if(httpMethod == "post")
                {
                    operation.Description ??= string.Empty;
                    operation.Description += @"
## Endpoint para criação de uma nova tarefa
                        
- **Não é possível criar mais de uma tarefa com o mesmo nome**
- **Não é possível usar uma data do passado para dueDate**
                    ";
                }
                else if (httpMethod == "put")
                {
                    operation.Description ??= string.Empty;
                    operation.Description += @"
## Endpoint para atualizar de uma tarefa
                        
- **É necessário enviar um Id válido e criado pelo usuário logado.**
- **Não é possível criar mais de uma tarefa com o mesmo nome**
- **Não é possível usar uma data do passado para dueDate**
- **Não é possível alterar status por aqui. Use um dos endpoints correspondentes.**

                    ";
                }
                
            }
            else if (endpointPath != null &&
                    endpointPath.StartsWith("api/task/") &&
                    endpointPath.Split('/').Length == 3 &&
                    endpointPath.Contains("{id}"))
            {
                if (httpMethod == "get")
                {
                    operation.Description ??= string.Empty;
                    operation.Description += @"
## Endpoint para buscar uma tarefa pelo ID
                        
- **É necessário enviar um Id válido e criado pelo usuário logado.**
                    ";
                }
                if (httpMethod == "delete")
                {
                    operation.Description ??= string.Empty;
                    operation.Description += @"
## Endpoint para remover uma tarefa
                        
- **É necessário enviar um Id válido e criado pelo usuário logado.**
- **A exclusão é apenas lógica (flag marcado como deleted)**

                    ";
                }
            }
        }
    }
}


