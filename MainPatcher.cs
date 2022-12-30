using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MusicFadingOut
{
    public static class MainPatcher
    {
        public static void Patch()
        {
            var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "subnautica.repkins.music-fading-out-fix");
            Logger.Info("Successfully patched");

            Config.Load();
            Logger.Info("Config successfully loaded");
        }
    }
}
