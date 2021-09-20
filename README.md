# O que é o qs.log
O qs.log é um sistema simplificado para gerenciamento de logs e para usar como ditatica para quem quiser aprender alguns padrões de projeto e estilos arquiteturais como: 
- DDD
- Testes de Unidade
- Repository
- CQRS
- Clean Arquitecture
- Mediator
- ...

Pode ser usado com banco de dados relacionais ou não relacionais. 
Pode ser usado tambem com o HabbitMQ. 

## Estrutura

O projeto esta separado em:
- [Backend (pasta qslog-back)](https://github.com/danilodumba/qsLog/tree/master/qslog-back)
- [Frontend (pasta qslog-front)](https://github.com/danilodumba/qsLog/tree/master/qslog-front)
- [Pacote (pasta qslog-pack)](https://github.com/danilodumba/qsLog/tree/master/qslog-pack)

### Backend

O backend foi escrito em .NET 5.0 podendo ser utilizado com banco de dados MySql ou MongoDB. 

### Frontend

O front foi escrito com Angular 9.0. 

### Pacote

O pacote foi escrido em .NET 3.1 para melhor compatibilidade com alguns projetos legados. Esta disponivel no nuget.
