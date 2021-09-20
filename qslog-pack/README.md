# Como usar? [![NuGet](https://img.shields.io/nuget/v/qsLog.svg)](https://nuget.org/packages/qsLog)

1. Referencie em seu projeto o [qs.log](https://nuget.org/packages/qsLog) 
2. Adicione o projeto no startup.cs

No método `ConfigureServices` adicione o qs.log.
```csharp
    services.AddQsLog(this.Configuration);
```
No método `Configure` use:
```csharp
    app.UseQsLog();
```
>**Note:** Informe o codigo acima antes de todos os app.Use existentes.

3. No appsettings.json

- Para usar via POST. 

Inclua no seu appsettings a tag.

```json
"QsLogSettings": {
    "LogApi": "http://[SUA URL]/api/log",
    "ApiKey": "3ca1f4e1-f544-4562-88022-21dd4037c764"
  }
``` 
|Campo|Descrição|
|--|--|
|LogApi  | Link da API onde instalou o backend |
|ApiKey  | Chave gerada ao criar o projeto no sistema QS.LOG |

- Para usar via HabbitMQ.

Inclua no seu appsettings a tag.

```json
"QsLogSettings": {
    "UseHabbitMQ": "true",
    "ApiKey": "3ca1f4e1-f544-4562-88022-21dd4037c764",
    "RabbitConnection": "[Sua conexao com o HabbitMQ]",
    "Queue": "[Nome da sua fila]"
  }
```

|Campo|Descrição|
|--|--|
|UseHabbitMQ  | Informe true para usar o habbitMQ |
|ApiKey  | Chave gerada ao criar o projeto no sistema QS.LOG |
|RabbitConnection  | Usa string de conexao para conectar ao HabbitMQ |
|Queue  | Nome da fila para ser gerado o log |


## Capturando os errors, warnings ou informations. 

Automaticamente seu sistema irá capturar todas as exceções não tratadas e enviará para o qs.log.

Caso queira informar alguma exceção basta usar o servico `ILogService`. 
Ao injetar o servico em sua classe use os metodos: 

- Information;
- Warning;
- Error;