using System.Collections.Generic;
using Verse;

namespace StickToYourSave;

public class LockSettings : ModSettings
{
    public int lockedWorldId;
    public string? lockedSeed;
    public string? lockedWorldName;
    public List<string> trackedSaveFiles = [];

    public bool HasLock => lockedSeed != null;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref lockedWorldId, "lockedWorldId");
        Scribe_Values.Look(ref lockedSeed, "lockedSeed");
        Scribe_Values.Look(ref lockedWorldName, "lockedWorldName");
        Scribe_Collections.Look(ref trackedSaveFiles, "trackedSaveFiles", LookMode.Value);
        trackedSaveFiles ??= [];
    }
}
