using System.Collections.Generic;
using System.Linq;
using Concord;
using UnityEngine;
using Verse;

namespace StickToYourSave.Patches;

[Patch(typeof(OptionListingUtility))]
internal static class MainMenu_ClearLockButton_Patch
{
    [Inject(At.Head, nameof(OptionListingUtility.DrawOptionListing), parameterTypes: [typeof(Rect), typeof(List<ListableOption>)])]
    private static void Prefix(Rect rect, List<ListableOption> optList)
    {
        if (Current.ProgramState != ProgramState.Entry) return;
        if (!SaveLock.IsLocked) return;
        string newColonyLabel = "NewColony".Translate();
        if (!optList.Any(opt => opt.label == newColonyLabel)) return;
        string clearLabel = "STYS_ClearLock".Translate();
        if (optList.Any(opt => opt.label == clearLabel)) return;
        optList.Add(new ListableOption(clearLabel, () => Confirmations.TripleConfirm(SaveLock.Clear)));
    }
}
