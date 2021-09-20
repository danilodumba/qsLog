# Como usar? [![NuGet](https://img.shields.io/nuget/v/qsLog.svg)](https://nuget.org/packages/qsLog)

1. Referencie em seu projeto o [qs.log](https://nuget.org/packages/qsLog) 
2. Adicione o projeto no startup.cs
    No método `ConfigureServices` adicione o qs.log.
    ```csharp
        services.AddQsLog(this.Configuration);
    ```
3. Em configure use:
```csharp
    app.UseQsLog();
```
>**Note:** Informe o codigo acima antes de todos os app.Use existentes.

4. No appsettings.json

4.1 Para usar via POST. 

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

4.2 Para usar via HabbitMQ.

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