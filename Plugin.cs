using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFadingOut
{
    [BepInPlugin("subnautica.repkins.music-fading-out-fix", "Music Fading Out Fix", "1.0.2.0")]
    public class Plugin : BaseUnityPlugin
    {
        public void Awake()
        {
            MainPatcher.Patch();
        }
    }
}
