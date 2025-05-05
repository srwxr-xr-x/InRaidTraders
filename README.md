# In-Raid Traders

![GitHub license](https://img.shields.io/github/license/srwxr-xr-x/InRaidTraders.svg)
![GitHub issues](https://img.shields.io/github/issues/srwxr-xr-x/InRaidTraders.svg)

A WIP, In-Raid Traders mod for Single Player Tarkov

---
    
## Installation
    
You will need **SPT version 3.11.x** installed. Unzip and drag the mod into the root of your SPT install, where the EXE is located.
The mod should be under the path *SPTLocation/BepInEx/plugins/In-RaidTraders.dll*.
    
### Reporting Issues and Bugs
    
You can report bugs and crashes by opening an issue on our [issue tracker](https://github.com/srwxr-xr-x/InRaidTraders/issues).
    
### Building with Rider

In-RaidTraders uses a typical C# project structure and can be built by simply running the default `Debug` or `Release` task. 
After Rider finishes building the project, you can find the built mod in `bin/Debug` or `bin/Release`. Feel free to open a pull request!

### Adding your trader as an In-Raid Trader:

To add your trader to IRT as a custom trader in game currently is simple. 
- Add a localization JSON to the `InRaidTraders-Server/data/Locales/en/`, an example exists under `en` that shows where to place your JSON and how to format it.
- Add a Config JSON to the `InRaidTraders-Core/Configs/`, It contains options for the trader mongo ID, name which is used in localization, whether you can access them anywhere using a `UnityEngine.KeyCode` integer keybind, the location of the interactable cube in the world, rotation, and scale, and the map. Included is a debug option that renders the cube, reccommended to see for average user. 
Currently only 1 map is supported, next update should fix this.

### Development

If you are going to help develop the mod (which I highly recommend!) I only ask that you pull request your feature!

$${\color{lightblue}Trans \space \color{pink}Rights \space \color{white}Are \space \color{pink}Human \space \color{lightblue}Rights}$$
