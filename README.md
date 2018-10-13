# PipeDriveApi

[![NuGet](https://img.shields.io/nuget/v/PipeDriveApi.svg?style=flat-square)](https://www.nuget.org/packages/PipeDriveApi/)

> .Net API for interacting with the PipeDrive API. Fully Async, support for custom fields and API Rate limiting.

## Example - Basics

```cs
var myKey = "MY_PIPEDRIVE_API_KEY";
var client = new PipeDriveClient(myKey);

var activityService = new ActivityEntityService<Activity>(client);
var emails = await activityService.GetAllByType("email");

var dealsService = new DealEntityService<Deal>(client);
var deals = await dealsService.GetAllAsync();

var personsService = new PersonEntityService<Person>(client);
var persons = await personsService.GetAllAsync();
```

## Example - Custom Fields

```cs
var myKey = "MY_PIPEDRIVE_API_KEY";
var client = new PipeDriveClient(myKey);

var orgService = new OrganizationEntityService<MyCustomOrganization>(client);
var orgs = await orgService.GetAsync();

public class MyCustomOrganization : Organization
{
   [CustomField("5d65d158579525f6d46b7d381fad397d74778553")]
   public string FavoriteShoeSize { get; set; }
}
```
