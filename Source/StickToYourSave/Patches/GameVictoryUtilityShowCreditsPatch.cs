using Concord;
using RimWorld;

namespace StickToYourSave.Patches;

[Patch(typeof(GameVictoryUtility))]
internal static class GameVictoryUtilityShowCreditsPatch {
    [Inject(At.Tail, nameof(GameVictoryUtility.ShowCredits))]
    private static void Postfix() {
        SaveLock.Clear();
    }
}
