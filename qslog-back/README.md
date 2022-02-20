# Como foi desenvolvido o backend?

O backend do qs.log foi desenvolvido em C# utilizando .NET 5.0. 

Neste projeto você encontrará alguns padrões como:

- DDD
- Testes de Unidade
- Repository
- CQRS
- Clean Arquitecture
- Mediator
- Command

O projeto está desenvolvido para utilizar o banco de dados MySql ou MongoDB.  
Para o gerenciamento de filas esta implementado o HabbitMQ.

# Como instalar?

1. Para rodar com MySql.

Adicione a configuração no seu appsettings.
```json
"ConnectionStrings": {
    "MySqlConn": "server=[seu-server];user id=[seu-usuario];password=[sua-senha];port=3306;database=qslog;"
  },
``` 

Rode os scripts na pasta qsLog.Infra.MySql.Scripts

>**Note:** Rode os scripts na sequencia das versões.

Na classe `Statup.cs` no método `ConfigureServices` adicione o MySql.
```csharp
    services.AddInfraDatabaseMySql(Configuration.GetConnectionString("MySqlConn"));
```

2. Para rodar com MongoDB.

Adicione a configuração no seu appsettings.
```json
"MongoConnection": {
    "ConnectionString": "mongodb://[sua-conexao]",
    "Database": "logDB"
  },
``` 

Na classe `Statup.cs` no método `ConfigureServices` adicione o MongoDB.
```csharp
     services.AddInfraDatabaseMongoDB(Configuration);
```

3. Configurar o HabbitMQ.

Inclua no seu appsettings a tag.

```json
"QsLogSettings": {
    "UseHabbitMQ": "true",  
    "RabbitConnection": "[Sua conexao com o HabbitMQ]",
    "Queue": "[Nome da sua fila]"
  }
```


|Campo|Descrição|
|--|--|
|UseHabbitMQ  | Informe true para usar o habbitMQ |
|RabbitConnection  | Usa string de conexao para conectar ao HabbitMQ |
|Queue  | Nome da fila para ser gerado o log. Lembre - se que deve ser a mesma fila configurada na sua aplicacao. Veja o qslog-pack. |

>**Note:** Usar o HabbitMQ é opcional. Pode ser utilizado de forma sincrona.

4. Rode a aplicação aplicação

Ao iniciar a aplicação sera gerada um usuário e senha ADMIN para acesso ao sistema.

>**Note:** Lembre de sempre trocar sua senha de administrador.

5. Chave para JWT.

Troque a chave para gerar seu token JWT.

```json
    "SecurityKey": "[SUA-CHAVE]"
``` 
