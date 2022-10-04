# RabbitMQ-Sample App It's my first time using RabbitMQ 

Written in C#,
Asp.net Core (.net 6),
EF Core Code First,
Clean Code Architecture,
MediatR + CQRS Pattern,
RabbitMQ

**Create MSSQL Instance**
To create mssql localdb open CMD and type: 

> SqlLocalDb create "<Intance_Name>"

**Create Database**

You may delete the migration files in infrastructure folder

-Open Package Manager Console and type the following:
> cd .\HashGenerator.Infrastructure

> Add-Migration "<Migration_Name>"

> Update-Database
