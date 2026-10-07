using System.Reflection;
using Concord;
using RimWorld;
using Verse;

namespace StickToYourSave.Patches;

[Patch]
internal abstract class PageModsConfigTrySetModInactivePatch : Page_ModsConfig {
#pragma warning disable S3011
    private static readonly MethodInfo TrySetModInactiveMethod =
        typeof(Page_ModsConfig).GetMethod("TrySetModInactive", BindingFlags.Instance | BindingFlags.NonPublic)!;
#pragma warning restore S3011

    private static bool bypassConfirmations;

    [Inject(At.Head, "TrySetModInactive", parameterTypes: [typeof(ModMetaData)])]
    private Control Prefix(ModMetaData mod) {
        if (TakeBypass()) {
            return Control.Continue;
        }

        if (!mod.SamePackageId(StickToYourSaveMod.PackageId, ignorePostfix: true)) {
            return Control.Continue;
        }

        Page_ModsConfig page = this;
        Confirmations.TripleConfirm(() => {
            ArmBypass();
            TrySetModInactiveMethod.Invoke(page, [mod]);
        });
        return Control.Cancel;
    }

    private static void ArmBypass() {
        bypassConfirmations = true;
    }

    private static bool TakeBypass() {
        if (!bypassConfirmations) return false;
        bypassConfirmations = false;
        return true;
    }
}
