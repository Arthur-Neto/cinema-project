# Cinema Project

[![Build Status](https://netoarthur.visualstudio.com/Cinema/_apis/build/status/Arthur-Neto.cinema-project?branchName=master)](https://netoarthur.visualstudio.com/Cinema/_build/latest?definitionId=6&branchName=master)

Project made for a job interview using the lastest(08/2020) .NET technologies.

Full development process: https://netoarthur.visualstudio.com/Cinema

## Dependencies

> Backend
>> * .NET Core 3.1
>> * AutoMapper.Extensions.Microsoft.DependencyInjection 8.0.1
>> * FluentValidation.AspNetCore 9.1.1
>> * Microsoft.AspNetCore.Authentication.JwtBearer 3.1.6
>> * Microsoft.AspNetCore.OData 7.4.1
>> * Microsoft.AspNetCore.SpaServices.Extensions 3.1.6
>> * Microsoft.EntityFrameworkCore 3.1.6
>> * Microsoft.EntityFrameworkCore.InMemory 3.1.6
>> * Microsoft.EntityFrameworkCore.Relational 3.1.6
>> * Swashbuckle.AspNetCore.Swagger 5.5.1
>> * Swashbuckle.AspNetCore.SwaggerGen 5.5.1
>> * Swashbuckle.AspNetCore.SwaggerUI 5.5.1
>> * System.IdentityModel.Tokens.Jwt 6.7.1
>> * NUnit 3.12.0
>> * NUnit3TestAdapter 3.17.0

> Frontend
>> * Angular 10
>> * @angular/material 10.1.3
>> * typescript 3.9.7

## How to run

PT-BR:

Debug:
* Para rodar a aplicação em modo Debug, instale a ultima versão do Node.js
* Abrir o .sln utilizando a ultima versão do Visual Studio, instalar as dependencias corretamente e rodar a aplicação a partir do .csproj do WebApi
* Abrir a pasta ClientApp no cmd ou algum outro terminal, e execute o comando ng serve
* Deve ser possivel acessar o site para debug através do endereço http://localhost:4002/

Compilado:
* Para rodar a aplicação, basta executar Theater.WebApi.exe e abrir no navegador o endereço http://localhost:5000/

Seed:
* Por padrão foi adicionado um cliente gerente com login admin e senha 123

## Configurando SQL Server

* No arquivo appsettings.json, alterar a chave UseInMemory para False e configurar a chave SqlServerConnectionString corretamente.
