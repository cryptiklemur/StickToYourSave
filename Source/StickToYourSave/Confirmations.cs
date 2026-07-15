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

    public static void Block(TaggedString text)
    {
        Find.WindowStack.Add(new Dialog_NedryMessage(text));
    }

    private static void Show(TaggedString text, Action confirmedAct)
    {
        Find.WindowStack.Add(new Dialog_NedryMessage(text,
            buttonAText: "Confirm".Translate(), buttonAAction: confirmedAct,
            buttonBText: "GoBack".Translate(), buttonADestructive: true));
    }
}
