using System.Reflection;
using Concord;
using RimWorld;
using Verse;

namespace StickToYourSave.Patches;

[Patch]
internal abstract class Page_ModsConfig_TrySetModInactive_Patch : Page_ModsConfig
{
    private static readonly MethodInfo TrySetModInactiveMethod =
        typeof(Page_ModsConfig).GetMethod("TrySetModInactive", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static bool bypassConfirmations;

    [Inject(At.Head, "TrySetModInactive", parameterTypes: [typeof(ModMetaData)])]
    private Control Prefix(ModMetaData mod)
    {
        if (bypassConfirmations)
        {
            bypassConfirmations = false;
            return Control.Continue;
        }

        if (!mod.SamePackageId(StickToYourSaveMod.PackageId, ignorePostfix: true))
        {
            return Control.Continue;
        }

        Page_ModsConfig page = this;
        Confirmations.TripleConfirm(() =>
        {
            bypassConfirmations = true;
            TrySetModInactiveMethod.Invoke(page, [mod]);
        });
        return Control.Cancel;
    }
}
