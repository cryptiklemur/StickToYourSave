using Concord;
using Verse;

namespace StickToYourSave.Patches;

[Patch(typeof(GameDataSaveLoader))]
internal static class GameDataSaveLoaderSaveGamePatch {
    [Inject(At.Tail, nameof(GameDataSaveLoader.SaveGame), parameterTypes: [typeof(string)])]
    private static void Postfix(string fileName) {
        SaveLock.OnGameSaved(fileName);
    }
}
