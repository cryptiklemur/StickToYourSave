using Concord;
using Verse;

namespace StickToYourSave.Patches;

[Patch(typeof(GameDataSaveLoader))]
internal static class GameDataSaveLoader_LoadGame_Patch
{
    [Inject(At.Head, nameof(GameDataSaveLoader.LoadGame), parameterTypes: [typeof(string)])]
    private static Control Prefix(string saveFileName)
    {
        if (SaveLock.BlocksLoad(saveFileName))
        {
            Find.WindowStack.Add(new Dialog_MessageBox("STYS_LoadBlocked".Translate(SaveLock.LockedName)));
            return Control.Cancel;
        }

        SaveLock.NotePendingLoad(saveFileName);
        return Control.Continue;
    }
}
