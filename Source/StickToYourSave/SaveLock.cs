using System;
using System.IO;
using System.Linq;
using RimWorld.Planet;
using Verse;

namespace StickToYourSave;

public static class SaveLock
{
    private static string? pendingLoadFile;

    public static bool IsLocked
    {
        get
        {
            LockSettings settings = StickToYourSaveMod.Settings;
            if (!settings.HasLock) return false;
            if (AnyTrackedFileExists(settings)) return true;
            Clear();
            return false;
        }
    }

    public static string LockedName => StickToYourSaveMod.Settings.lockedWorldName ?? "your colony";

    public static bool BlocksLoad(string saveFileName)
    {
        if (!IsLocked) return false;
        return !StickToYourSaveMod.Settings.trackedSaveFiles.Contains(saveFileName, StringComparer.OrdinalIgnoreCase);
    }

    public static bool BlocksNewColony()
    {
        return IsLocked;
    }

    public static void NotePendingLoad(string saveFileName)
    {
        pendingLoadFile = saveFileName;
    }

    public static void OnGameLoaded()
    {
        if (pendingLoadFile == null) return;
        BindToCurrentGame(pendingLoadFile);
        pendingLoadFile = null;
    }

    public static void OnGameSaved(string fileName)
    {
        BindToCurrentGame(fileName);
    }

    public static void Clear()
    {
        LockSettings settings = StickToYourSaveMod.Settings;
        settings.lockedWorldId = 0;
        settings.lockedSeed = null;
        settings.lockedWorldName = null;
        settings.trackedSaveFiles.Clear();
        StickToYourSaveMod.SaveSettings();
    }

    private static void BindToCurrentGame(string fileName)
    {
        WorldInfo? info = Current.Game?.World?.info;
        if (info == null) return;
        LockSettings settings = StickToYourSaveMod.Settings;
        bool sameWorld = settings.HasLock
            && settings.lockedWorldId == info.persistentRandomValue
            && settings.lockedSeed == info.seedString;
        if (!sameWorld)
        {
            settings.lockedWorldId = info.persistentRandomValue;
            settings.lockedSeed = info.seedString;
            settings.lockedWorldName = info.name;
            settings.trackedSaveFiles.Clear();
        }
        if (!settings.trackedSaveFiles.Contains(fileName, StringComparer.OrdinalIgnoreCase))
        {
            settings.trackedSaveFiles.Add(fileName);
        }
        StickToYourSaveMod.SaveSettings();
    }

    private static bool AnyTrackedFileExists(LockSettings settings)
    {
        return settings.trackedSaveFiles.Any(name => File.Exists(GenFilePaths.FilePathForSavedGame(name)));
    }
}
