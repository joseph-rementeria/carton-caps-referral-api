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
    * **Mediators**: implemented using the library `MediatR`, it is used to decouple the commands/queries with their handlers. A huge advantage if the contract with the next layer is to be maintained while the handler's logic can be altered if needed.
* **Infrastructure**: In this case the infrastructure layer is very thin, it is only a wrapper on top of `List`. This is a tradeoff done for this mock API. normally, this layer will have the heavy translation from DTOs to database or other persisted models and include things like migrations, updates, scripts, queries, calls to stored procedures, etc. But for this API since the Clean Architecture and the overall design and fucntionality is the goal, this layer is the simpliest of them all. It also includes some dummy data. 
* **Presentation**: Here called API. It clicks everything together, the main functionality that this project adds is the controllers and the ASP.NET framework.

But, why this much complexity? It all comes to maintainability! Here, the layers like Infra and API can be changed to something else (another database, another framework even a direct UI) that the usecases and the domain stays intact. Also another huge advantage is the posibility to enforce quite well the principle of Single responsability, with the usecases in its own query/command, the classes have only one reason to change.

### Runing and testing 
#### Prerequisites 
* An IDE such as Visual Studio Code
* .NET 8 SDK installed

#### Build and test
To build the app
``` bash
dotnet build
```
To run the unit tests
```bash 
dotnet test
```
#### Secrets setup
Setup these secrets in the presentation layer. These can be changed but keep them handy to generate the tokens
``` bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:SecurityKey" "YourLongAndComplexTestingSecretKey12345678"
dotnet user-secrets set "Jwt:Issuer" "TestIssuer"
dotnet user-secrets set "Jwt:Audience" "TestAudience"
```

#### Runing
In Clean Architecture (or Domain Driven Design). The only layer runnable is the API or presentaion layer. Therefore, to run the app, only the API project is runnable
``` bash
dotnet run --project CartonCaps.API
```
#### How to test
To ease the testing, this API has no full authentication nor authorization.
Nonetheless, one usecase, the list of all the referrals, contains a validation for the claim/attribute called userID. To be even easier to test, the token is already created with the default secret. This secret was created using Postman. But what if the secret is not the default one? the go to Postman, create a new Get operation, click on Auth tab, on Type, select JWT Bearer, and put the secret with the value setup on the app's secrets. if the issuer and audience where also modified, change the next if necesary:
* **Algorith**: `HS256`
* **Secret**: `YourLongAndComplexTestingSecretKey12345678`
* **Payload**
``` json
{
  "sub": "4f0b78e3-0c15-4a5f-b88e-6c8a7b1b3e9e", 
  "name": "Test User",
  "iss": "TestIssuer",
  "aud": "TestAudience"
}
```

Here are the curl commands to test the app:

``` bash
# GET for returns the list of all the referrals for the auth user
curl --location 'http://localhost:5258/api/referrals?page=1&pageSize=25' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0ZjBiNzhlMy0wYzE1LTRhNWYtYjg4ZS02YzhhN2IxYjNlOWUiLCJuYW1lIjoiVGVzdCBVc2VyIiwiaXNzIjoiVGVzdElzc3VlciIsImF1ZCI6IlRlc3RBdWRpZW5jZSJ9.kVCJO5J39yLh1ah-4UcitBgkZ6Vziu20ZqwqYtVqUz4'
```

``` bash 
# PUT to creates a referal 
curl --location 'http://localhost:5258/api/referrals' \
--header 'Content-Type: application/json' \
--data '{
    "code": "5t5t5t",
    "trackingId": "4e3b3c3d-5e6f-7580-0102-44667890ab3d"
}'
```

``` bash 
# GET for the specific referral id, the ID is auto generated by the app so this will need to be changed
curl --location 'http://localhost:5258/api/referrals/536d1163-bbaf-4a79-a5ab-bf408862d0f7'
```

``` bash 
# change the status to installed.
curl --location --request PUT 'http://localhost:5258/api/referrals/installed' \
--header 'Content-Type: application/json' \
--data '{
    "trackingId": "4e3b3c3d-5e6f-7580-0102-44667890ab3d"
}'
```

``` bash 
# change the status to registered
curl --location --request PUT 'http://localhost:5258/api/referrals/536d1163-bbaf-4a79-a5ab-bf408862d0f7/registered' \
--header 'Content-Type: application/json' \
--data-raw '{
    "email": "john.doe@test.com"
}'
```

``` bash
curl --location --request PUT 'http://localhost:5258/api/referrals/536d1163-bbaf-4a79-a5ab-bf408862d0f7/rewarded' \
--header 'Content-Type: application/json' \
--data '{}'
```