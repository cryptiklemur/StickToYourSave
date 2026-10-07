using System.Collections.Generic;
using Verse;

namespace StickToYourSave;

public class LockSettings : ModSettings {
    private List<string> trackedSaveFiles = [];

    public int LockedWorldId { get; set; }

    public string? LockedSeed { get; set; }

    public string? LockedWorldName { get; set; }

    public List<string> TrackedSaveFiles => trackedSaveFiles;

    public bool HasLock => LockedSeed != null;

    public override void ExposeData() {
        base.ExposeData();
        int worldId = LockedWorldId;
        string? seed = LockedSeed;
        string? worldName = LockedWorldName;
        Scribe_Values.Look(ref worldId, "lockedWorldId");
        Scribe_Values.Look(ref seed, "lockedSeed");
        Scribe_Values.Look(ref worldName, "lockedWorldName");
        Scribe_Collections.Look(ref trackedSaveFiles, "trackedSaveFiles", LookMode.Value);
        LockedWorldId = worldId;
        LockedSeed = seed;
        LockedWorldName = worldName;
        trackedSaveFiles ??= [];
    }
}
