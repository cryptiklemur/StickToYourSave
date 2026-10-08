using System;
using System.IO;
using System.Linq;
using RimWorld.Planet;
using Verse;

namespace StickToYourSave;

public static class SaveLock {
    private static string? pendingLoadFile;

    public static bool IsLocked {
        get {
            LockSettings settings = StickToYourSaveMod.Settings;
            if (!settings.HasLock) return false;
            if (AnyTrackedFileExists(settings)) return true;
            Clear();
            return false;
        }
    }

    public static string LockedName => StickToYourSaveMod.Settings.LockedWorldName ?? "your colony";

    public static bool BlocksLoad(string saveFileName) {
        if (!IsLocked) return false;
        return !StickToYourSaveMod.Settings.TrackedSaveFiles.Contains(saveFileName, StringComparer.OrdinalIgnoreCase);
    }

    public static bool BlocksNewColony() {
        return IsLocked;
    }

    public static void NotePendingLoad(string saveFileName) {
        pendingLoadFile = saveFileName;
    }

    public static void OnGameLoaded() {
        if (pendingLoadFile == null) return;
        BindToCurrentGame(pendingLoadFile);
        pendingLoadFile = null;
    }

    public static void OnGameSaved(string fileName) {
        BindToCurrentGame(fileName);
    }

    public static void Clear() {
        LockSettings settings = StickToYourSaveMod.Settings;
        settings.LockedWorldId = 0;
        settings.LockedSeed = null;
        settings.LockedWorldName = null;
        settings.TrackedSaveFiles.Clear();
        StickToYourSaveMod.SaveSettings();
    }

    private static void BindToCurrentGame(string fileName) {
        WorldInfo? info = Current.Game?.World?.info;
        if (info == null) return;
        LockSettings settings = StickToYourSaveMod.Settings;
        bool sameWorld = settings.HasLock
            && settings.LockedWorldId == info.persistentRandomValue
            && settings.LockedSeed == info.seedString;
        if (!sameWorld) {
            settings.LockedWorldId = info.persistentRandomValue;
            settings.LockedSeed = info.seedString;
            settings.LockedWorldName = info.name;
            settings.TrackedSaveFiles.Clear();
        }
        if (!settings.TrackedSaveFiles.Contains(fileName, StringComparer.OrdinalIgnoreCase)) {
            settings.TrackedSaveFiles.Add(fileName);
        }
        StickToYourSaveMod.SaveSettings();
    }

    private static bool AnyTrackedFileExists(LockSettings settings) {
        return settings.TrackedSaveFiles.Any(name => File.Exists(GenFilePaths.FilePathForSavedGame(name)));
    }
}
