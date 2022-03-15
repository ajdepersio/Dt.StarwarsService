# Dt.StarwarsService

Programming exercise to create a "Backends for Frontends" API around StarWars API (https://swapi.dev)

If you have credentials, you can see it in action here (https://dtstarwarsservicefunctions20220314173357.azurewebsites.net/api/swagger/ui)

## Functional Requirements
* List all Starships
* List all Manufacturers
* List all Starships by a given Manufacturer

## Non-Functional Requirements
* Authentication (done via Azure Function Function Keys)
* Responds in JSON format
* Not using 3rd party StarWars API libraries
* Using whatever else I want

### Technologies
* .NET 6
* Azure Functions
* Polly
* XUnit
* Moq
* Roslynator
* Swagger/OpenAPI