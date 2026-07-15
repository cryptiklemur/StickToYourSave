using Concord;
using RimWorld;

namespace StickToYourSave.Patches;

[Patch(typeof(GameVictoryUtility))]
internal static class GameVictoryUtility_ShowCredits_Patch
{
    [Inject(At.Tail, nameof(GameVictoryUtility.ShowCredits))]
    private static void Postfix()
    {
        SaveLock.Clear();
    }
}
