 ###  Web API template with JWT token auth, DB connection, Health checks and HTTP client proxy examples.

```
This branch for .Net Core 2.2 template. To use newer .Net Core 3.0 swith to master branch.
```

Projects template description
----

 - WebService.API project - web api with system configurations (auth, logging, api docs)
 - WebService.Configuration project - configuration for logic services and repositories
 - WebService.BLL project - logic services
 - WebService.DAL.MySql project - repositories
 - WebService.BLL.Tests project - unit test for BLL logic
 - WebService.DbUp.MySql project - MySql database migrations

Usings
----
 - .NET Core 2.2 
 - Serilog for logging and exception handling
 - AutoMapper for models mappings
 - API versioning
 - Swagger for API documentation 
 - Micro-ORM Dapper with MySql repository  
 - DbUp for database migrations
 - .Net Core health checks
 - .NET Core builtin HttpClientFactory with Polly (https://jsonplaceholder.typicode.com/ as fake mock api)
 - XUnit with Moq and AutoFixture for unit test   

Preview
----
![Solution template](assets/solutionScreenshot.png)

![Api](assets/swaggerUIScreenshot.png)

How to use
----
- Install project template in Visual Studio 2017.<br/>Tools -> Extensions and Updates -> Online -> Visual Studio Marketplace -> Search: Web API Template (.NET Core 2.x)

- Manually download and install VSIX Package for Visual Studio 2017 from Visual Studio marketplace.<br/>https://marketplace.visualstudio.com/items?itemName=ddiyteam.WebApiTemplateNetCore


- Clone repository and open solution from WebApiTemplate folder in Visual Studio 2017 directly.


License
----

MIT



