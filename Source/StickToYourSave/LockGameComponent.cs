using Verse;

namespace StickToYourSave;

public class LockGameComponent : GameComponent
{
    public LockGameComponent(Game _)
    {
    }

    public override void LoadedGame()
    {
        SaveLock.OnGameLoaded();
    }
}
