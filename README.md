# Black.Beard.OpenApi
Helpers for open api

[![Build status](https://ci.appveyor.com/api/projects/status/6ywrrb8pnhyqiabi?svg=true)](https://ci.appveyor.com/project/gaelgael5/black-beard-openapi)

## create a open api contract from a type
```csharp

// type to generate open api contract.
var type = typeof(ModelTest1);

// create a context with all types that contains the open api contract
var ctx = type.GenerateOpenApiContract()
              .GenerateOpenApiContract(t2)                                   // add another type
              .ApplyDocumentation(LoadDocumentationExtension.LoadXmlFiles()) // apply documentation
    ;

// generate the open api contract
OpenApiDocument openApi = ctx.Generate();

// save the contract in a file
openApi.SaveOpenApiContract("file path");

```