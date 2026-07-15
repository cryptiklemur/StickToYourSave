using Concord;
using RimWorld;
using Verse;

namespace StickToYourSave.Patches;

[Patch]
internal abstract class WindowStack_Add_Patch : WindowStack
{
    [Inject(At.Head, nameof(WindowStack.Add), parameterTypes: [typeof(Window)])]
    private Control Prefix(Window window)
    {
        if (window is not Page_SelectScenario) return Control.Continue;
        if (!SaveLock.BlocksNewColony()) return Control.Continue;
        Find.WindowStack.Add(new Dialog_MessageBox("STYS_NewColonyBlocked".Translate(SaveLock.LockedName)));
        return Control.Cancel;
    }
}
