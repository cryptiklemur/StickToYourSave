using Concord;
using Verse;

namespace StickToYourSave.Patches;

[Patch(typeof(GameDataSaveLoader))]
internal static class GameDataSaveLoader_SaveGame_Patch
{
    [Inject(At.Tail, nameof(GameDataSaveLoader.SaveGame), parameterTypes: [typeof(string)])]
    private static void Postfix(string fileName)
    {
        SaveLock.OnGameSaved(fileName);
    }
}
