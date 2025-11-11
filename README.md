# Carton Caps Referral Service API

This repository contains the two deliverables for the interview process. The first deliverable, the API definition, was implemented using OpenAPI 3.0. The second part is an ASP.NET application that follows the Domain Driven Design for scalable, maintainable API. 

## API Definition

You can find first deliverable in the folder `/docs` and an specialised README file pointing to public SwaggerHub project with a user friendly display with the API definition


Link for easy access: [SwaggerHub: Carton Caps Referral API](https://app.swaggerhub.com/apis/notapplicable-afc/carton-caps-referral-api-definition/v1)

## ASP.NET RESTful API

### Description
This is a RESTful backend designed using Clean Architecture.
We have 4 layers:
* **Domain**: the models of the app, these are categorized in Value Objects and Aggregates. **IMPORTANT** it depends on nothing else!. It is the pure core of our app. Defines a common vocabulary and rules for the application's center: its domain.
    * **Domain Objects**: view them as mear measurements encapsulated in classes for their proper validation. For instance: and ID, an email or a referral code.
    * **Aggregates** view them as identifieable objects. in this case a referral instance.
* **Application**: the application layer contains the **usecases** for our app. It further defines the actions that can be done on the agains the application's domain. **IMPORTANT** it depends only on the Domain layer only, nothing else. Here we use an important pattern as well called Command/Query Responsability Segregation ([CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)). Here we have the next memorable concepts
    * **Commands**: in simple words, these are the read-write (WR) usecases' params.
    * **Queries**: again, oversimplified, these are the read-only (RO) usecases' params.
    * **Handlers**: encapsulates the actual logic of the usecases, either RO or RW.
    * **Mediators**: `to be defined`
* **Infrastructure**: In this case the infrastructure layer is very thin, it is only a wrapper on top of `List`. This is a tradeoff done for this mock API. normally, this layer will have the heavy translation from DTOs to database or other persisted models and include things like migrations, updates, scripts, queries, calls to stored procedures, etc. But for this API since the Clean Architecture and the overall design and fucntionality is the goal, this layer is the simpliest of them all.
* **API**: Also called presentation layer. `to be defined`

### Runing and testing 
#### Prerequisites 
* An IDE such as Visual Studio Code
* .NET 8 SDK installed

#### Build and test
To build the app
```
dotnet build
```
To run the unit tests
```
dotnet test
```

#### Runing
In Clean Architecture (or Domain Driven Design). The only layer runnable is the API or presentaion layer. Therefore, to run the app, only the API project is runnable
```
dotnet run --project CartonCaps.API
```