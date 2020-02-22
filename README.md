# Micro-Architecture Sandbox
Just my own personal stomping grounds for testing and experimenting with microservice architecture and related technologies.  The intent is to log my journey in the hopes to create a great reference source for other projects.

#### Tech / Pattern Highlights ####
- .Net Core 2.2
- Polly / Resilience Patterns
- Gateway Pattern
- Swagger
- NGSwagStudio C# Clients
- XUnit Test Framework
- MOQ Mocking Framework
- Angular 8
- NGRX
- ng-swagger-gen typescript clients

#### How to Run ####
__Server Side:__
- Configure all Services to run as projects (not IISExpress)
- Configure the projects to run with the correct port (see AppSettings.json file)
- Run all apis -- Front end talks to Gateway exclusively

__Angular:__
- install npm globally
- install angular cli globally
- Run: npm install
- install ng-swagger-gen globally
- go to /src/Web
- Run: ng serve
