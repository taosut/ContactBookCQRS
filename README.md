# ContactBook CQRS  
<h4> (with SPA & Event Sourcing)</h4>

<hr>
This project was built to demonstrate how to implement CQRS and Event Sourcing using MediatR, designed with DDD and using the 
following technologies:<br /><br />

<ul>
  <li>
    <a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a>
    for cross-platform server-side code with:
    <ul>
      <li>.NET Core 3.1</li>
      <li>Entity Framework Core 3.1</li>
      <li>ASP.NET WebApi Core with JWT Bearer Authentication</li>
      <li>ASP.NET Identity Core</li>
      <li>MediatR</li>
      <li>Automapper</li>
      <li>Fluent Validation</li>
      <li>Swagger</li>
      <li>HealthChecks</li>
    </ul>
  </li><br>
  <li><a href='https://angular.io/' target="_blank">Angular</a> and <a href='http://www.typescriptlang.org/' target="_blank">
    TypeScript</a> for client-side code with:</li>
    <ul>
      <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
      <li>NgBootstrap</li>
      <li>Font Awesome</li>
      <li>Toastr</li>
      <li>Angular JWT</li>
    </ul>
</ul>

<hr>

  <h1> Code design and Architecture </h1>
This demo was designed following architectural patterns and conventions in order to ensure scalability, clean code and SOLID with the following:
<br><br>
<ul>
  <li>
    <b>Presentation</b>
    <ul>
      <li>
        A complete SPA built with Angular 9 using a modular structure (Core, Shared, features), Http Services, Pipes, Components, Guards, Notification service (using Toastr), Interceptors, Error handling  and JWT authentication mechanism
      </li>
     </ul>
  </li>
  <li>
    <b>Application</b>
    <ul>
      <li>
        Application Services, Automapping (ViewModel to Domain/ Domain to ViewModel), Event Sourcing normatizers
      </li>
    </ul>
  </li>
  <li>
    <b>Domain</b>
    <ul>
      <li>
        Aggregates, Repository interfaces, Commands and CommandHandlers, Domain Events, Notification Handlers, Events and Validations (FluentValidation)
      </li>
    </ul>
  </li>
  <li>
    <b>Infrastructure</b>
    <ul>
      <li>
        Persistence implementation (Repositories and Unit of Work), Migrations, Contexts and Configurations for EF Core, Authorization
       (Claims) and Services
      </li>
    </ul>
  </li>
  
</ul>
<hr>

To run it is simple! you will need the latest Visual Studio 2019 and .NET Core SDK. 
<br>I used SQL Server for database, but since it's all using Entity Framework Core, you can easilly port it to another database.
<br><b>Note:</b> The EF migration will be executed automatically when the application runs for the first time. 
