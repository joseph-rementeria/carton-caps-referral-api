# Carton Caps Referral Service API

This repository contains the two deliverables for the interview process. The first deliverable, the API definition, was implemented using OpenAPI 3.0. The second part is an ASP.NET application that follows the Domain Driven Design for scalable, maintainable API. 

## API Definition

You can find first deliverable in the folder `/docs` and an specialised README file pointing to public SwaggerHub project with a user friendly display with the API definition


Link for easy access: [SwaggerHub: Carton Caps Referral API](https://app.swaggerhub.com/apis/notapplicable-afc/carton-caps-referral-api-definition/v1)

## ASP.NET RESTful API

### Description
This is a RESTful backend designed using Clean Architecture.
We have 4 layers:
* **Domain**: the models of the app, these are categorized in Value Objects and Aggregates
    * **Domain Objects**: view them as mear measurements encapsulated in classes for their proper validation. For instance: and ID, an email or a referral code.
    * **Aggregates** view them as identifieable objects. in this case a referral instance.
* **Application**: `to be defined`
* **Infrastructure**: `to be defined`
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