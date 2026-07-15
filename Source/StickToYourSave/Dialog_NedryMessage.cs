using System;
using Verse;

namespace StickToYourSave;

public class Dialog_NedryMessage : Dialog_MessageBox
{
    public Dialog_NedryMessage(TaggedString text, string? buttonAText = null, Action? buttonAAction = null,
        string? buttonBText = null, Action? buttonBAction = null, string? title = null, bool buttonADestructive = false)
        : base(text, buttonAText, buttonAAction, buttonBText, buttonBAction, title, buttonADestructive)
    {
    }

    public override void DoWindowContents(UnityEngine.Rect inRect)
    {
        image = NedryFrames.Current;
        base.DoWindowContents(inRect);
    }
}
