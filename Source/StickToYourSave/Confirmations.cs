using System;
using Verse;

namespace StickToYourSave;

public static class Confirmations
{
    public static void TripleConfirm(Action onConfirmed)
    {
        Show("STYS_Confirm1".Translate(),
            () => Show("STYS_Confirm2".Translate(),
                () => Show("STYS_Confirm3".Translate(), onConfirmed)));
    }

    private static void Show(TaggedString text, Action confirmedAct)
    {
        Find.WindowStack.Add(Dialog_MessageBox.CreateConfirmation(text, confirmedAct, destructive: true));
    }
}
