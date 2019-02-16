 ###  WebAPI template with JWT token auth, DB connection and HTTP client proxy examples.

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
 - .NET Core builtin HttpClientFactory with Polly (https://jsonplaceholder.typicode.com/ as fake mock api)
 - XUnit with Moq and AutoFixture for unit test   

VSIX Package link on Visual Studio marketplace
----
https://marketplace.visualstudio.com/items?itemName=ddiyteam.WebApiTemplateNetCore

Preview
----
![Solution template](assets/solutionScreenshot.png)

![Api](assets/swaggerUIScreenshot.png)

License
----

MIT



