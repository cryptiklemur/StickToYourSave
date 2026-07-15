using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace StickToYourSave;

public static class NedryFrames
{
    private const int FrameCount = 5;
    private const float SecondsPerFrame = 0.25f;

    private static readonly List<Texture2D> Frames = [];
    private static bool loaded;

    public static bool Any
    {
        get
        {
            EnsureLoaded();
            return Frames.Count > 0;
        }
    }

    public static Texture2D? Current
    {
        get
        {
            EnsureLoaded();
            if (Frames.Count == 0) return null;
            int index = Mathf.FloorToInt(Time.realtimeSinceStartup / SecondsPerFrame) % Frames.Count;
            return Frames[index];
        }
    }

    private static void EnsureLoaded()
    {
        if (loaded) return;
        loaded = true;
        for (int i = 0; i < FrameCount; i++)
        {
            Texture2D? frame = ContentFinder<Texture2D>.Get($"StickToYourSave/Nedry/frame_{i:00}", false);
            if (frame != null) Frames.Add(frame);
        }
    }
}
