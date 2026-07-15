# Stick To Your Save

A RimWorld 1.6 mod that locks you to your current colony's saves. Loading anything else is blocked, and so is starting a new colony, until you win, get wiped, or delete the colony's saves.

Patching is done with [Concord](https://steamcommunity.com/sharedfiles/filedetails/?id=3758333473) instead of Harmony.

## How the lock works

The lock lives in mod settings, outside any save file. It stores the world's identity (its persistent random value and seed) plus the save file names that belong to the playthrough.

- Saving binds the lock to the current colony and records the file name. Autosaves count.
- Loading an allowed save records it the same way.
- Loading anything else gets a refusal dialog instead of a load.
- Opening the scenario select screen while locked gets the same treatment.

The lock releases at three points: the credits screen (any victory), the game-over dialog (colony wipe), or when every tracked save file is gone from disk. There's also a "Clear save lock" button on the main menu behind three confirmations, and disabling the mod in the mod list goes through the same three.

## Building

```
dotnet build Source/StickToYourSave/StickToYourSave.csproj -c Release
```

Compiles against [Krafs.Rimworld.Ref](https://www.nuget.org/packages/Krafs.Rimworld.Ref) by default. To build against a real game install instead:

```
dotnet build Source/StickToYourSave/StickToYourSave.csproj -c Release -p:GameManagedDir=/path/to/RimWorld/RimWorldLinux_Data/Managed
```

Output lands in `Assemblies/`. The Concord packages (`Concord.Ref`, `Concord.Analyzers`) are compile-time only. The runtime DLL ships with the Concord workshop mod.

## Releasing

CI builds the mod against real 1.6 assemblies pulled from a game image produced by [steam-game-image-action](https://github.com/cryptiklemur/steam-game-image-action). Releases go through semantic-release. Workshop publishing with [semantic-release-steam](https://www.npmjs.com/package/semantic-release-steam) turns on once you fill in the workshop id in `release.config.mjs` after the first manual publish.

Required repo secrets: `STEAM_USERNAME`, `STEAM_CONFIG_VDF_B64`.

## License

MIT
