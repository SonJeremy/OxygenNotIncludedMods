# Son Jeremy&#39;s Mods for Oxygen Not Included

Last tested on game version: **U54-647408**

**No support** for Public Testing branches, including the rolled back Legacy Vanilla (CS-469300).

Steam Workshop: [My Steam Workshop Link](https://steamcommunity.com/profiles/76561198930042307/myworkshopfiles/?appid=457140)

# Mod List

## New Features

|                                             **Name**                                              |                                                                                      **Description**                                                                                       | **Vanilla** | **Spaced Out!** | **The Frosty Planet** | **The Bionic Booster** |
|:-------------------------------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-----------:|:---------------:|:---------------------:|:----------------------:|
| [Trash Cans (Trashcans Fixed)](https://steamcommunity.com/sharedfiles/filedetails/?id=3398520692) | Keep your colony tidy! Trash Cans help dispose of unused elements, from gases to artifacts. A Re-work of [original mod](https://steamcommunity.com/sharedfiles/filedetails/?id=2037089892) |     Yes     |       Yes       |          Yes          |          Yes           |

# Compiling This Repository

This repository requires an installed copy of Oxygen Not Included to compile.
The project is currently built against JetBrains Rider, the .NET Framework 4.8 (as required by the game), and optionally [Refasmer](https://github.com/JetBrains/Refasmer) to regenerate the CI assemblies.
Make sure that the correct targeting packs, as well as MSBuild support, are installed to build.

The build scripts will automatically detect most Steam installations of Oxygen Not Included.
To customize paths for other distribution platforms or operating systems, create a copy of `Directory.Build.props.default` named `Directory.Build.props.user`.
Modify the values in it to match the correct folder locations. The "legacy" game folder is only used for "Big Merge" versions of the game and can usually be safely ignored.

*Note that ReSharper may have issues with the Steam build auto-detection; users of this plugin will need to manually specify paths to any custom Steam installations.*

# Special Thanks To

- **[@PeterHan](https://github.com/peterhaneve)**, Author of **[PLib](https://github.com/peterhaneve/ONIMods/tree/master/PLib)** and this Repo Template.
- **[@Pardeike](https://github.com/pardeike)**, Author of **[Harmony 2](https://github.com/pardeike/Harmony)**, the library that made **Oxygen Not Included** modding possible.