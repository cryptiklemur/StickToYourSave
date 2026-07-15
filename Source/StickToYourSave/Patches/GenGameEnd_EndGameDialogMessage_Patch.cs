using Concord;
using RimWorld;
using UnityEngine;

namespace StickToYourSave.Patches;

[Patch(typeof(GenGameEnd))]
internal static class GenGameEnd_EndGameDialogMessage_Patch
{
    [Inject(At.Tail, nameof(GenGameEnd.EndGameDialogMessage), parameterTypes: [typeof(string), typeof(bool), typeof(Color)])]
    private static void Postfix()
    {
        SaveLock.Clear();
    }
}
