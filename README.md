# HADotNet.Entities
A wrapper around the excellent [HADotNet.Core REST api](https://github.com/qJake/HADotNet) by @qJake with strongly typed Entity classes

## Features
- .NET Standard 2.0 cross-platform library
- DI-friendly initialization
- Home Assistant entities represented by strongly typed C# classes

## Does NOT (yet?) include
- Websockets or any real-time update strategy

## Currently (partially) implemented HA domains
- binary_sensor
- climate
- light
- switch
- media_player
- sensor
- switch

## Example usage

#### Initialization
```csharp
// Initialize the HADotNet.Core clients
ClientFactory.Initialize("https://my-ha-instance.duckdns.org", "long-lived-access-token");

var entityClient = ClientFactory.GetClient<EntityClient>();
var statesClient = ClientFactory.GetClient<StatesClient>();

// Initialize an instance of the EntitiesService
var entitiesService = new EntitiesService(entityClient, statesClient);
```

#### Toggle a light
```csharp
// Get all light entities
var lights = await entitiesService.GetEntities<Light>();

// Turn on all lights
await Task.WhenAll(lights.Select(light => light.TurnOn()));
```

#### Set the temperature of a climate entity
```csharp
// Get a single climate entity
var climate = await entitiesService.GetEntity<Climate>("living_room"); // climate.living_room

Console.WriteLine($"Current target temperature: {climate.Temperature}");

await climate.SetTemperature(20);

Console.WriteLine($"New target temperature: {climate.Temperature}");
```

#### Explicitly sync an entity after 10 seconds
```csharp
var light = await entitiesService.GetEntity<Light>("living_room"); // light.living_room

Thread.Sleep(10000);

await light.Update();
```
