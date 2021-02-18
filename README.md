# HADotNet.Entities
A wrapper around the excellent [HADotNet.Core REST api](https://github.com/qJake/HADotNet) by @qJake with strongly typed Entity classes

## Features
- .NET Standard 2.0 cross-platform library
- DI-friendly initialization
- Home Assistant entities represented by strongly typed C# classes
  - Strongly typed methods (TurnOn, TurnOff etc.)
  - Strongly typed attributes (climate.CurrentTemperature, mediaPlayer.IsMuted etc.)

## Does NOT (yet?) include
- Websockets or any real-time update strategy

## Currently (partially) implemented HA domains
- [binary_sensor](src/HADotNet.Entities/Models/BinarySensor.cs)
- [climate](src/HADotNet.Entities/Models/Climate.cs) (TurnOn, TurnOff, SetTemperature, SetHvacMode)
- [light](src/HADotNet.Entities/Models/Light.cs) (TurnOn, TurnOff, Toggle)
- [switch](src/HADotNet.Entities/Models/Switch.cs)
- [media_player](src/HADotNet.Entities/Models/MediaPlayer.cs) (TurnOn, TurnOff, Toggle, SelectSource, Play, Pause, Stop, Mute, VolumeUp, VolumeDown, VolumeSet)
- [sensor](src/HADotNet.Entities/Models/Sensor.cs)
- [switch](src/HADotNet.Entities/Models/Switch.cs) (TurnOn, TurnOff, Toggle)

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
