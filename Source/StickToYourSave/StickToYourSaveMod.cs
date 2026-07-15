using Concord;
using Verse;

namespace StickToYourSave;

public class StickToYourSaveMod : Mod
{
    public const string PackageId = "cryptiklemur.sticktoyoursave";

    public static LockSettings Settings { get; private set; } = null!;

    public StickToYourSaveMod(ModContentPack content) : base(content)
    {
        Settings = GetSettings<LockSettings>();
        Patcher.Apply(typeof(StickToYourSaveMod).Assembly);
    }

    public static void SaveSettings()
    {
        LoadedModManager.GetMod<StickToYourSaveMod>().WriteSettings();
    }
}
