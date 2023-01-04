using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MusicFadingOut
{
    [HarmonyPatch(typeof(FMOD_CustomEmitter))]
    [HarmonyPatch("Stop")]
    static class FMODCustomEmitterStopPatch
    {
        static void Prefix(FMOD_CustomEmitter __instance, ref bool __state)
        {
            __state = __instance.playing;
        }

        static void Postfix(FMOD_CustomEmitter __instance, bool __state)
        {
            if (__state != __instance.playing)
            {
                if (__instance.asset && __instance.asset.name.EndsWith("music"))
                {
                    __instance.StartCoroutine(FadeOut(__instance));
                }
            }
        }

        static IEnumerator FadeOut(FMOD_CustomEmitter customEmitter)
        {
            var evt = customEmitter.GetEventInstance();
            var fadeDuration = Config.Instance.fadeDurationSeconds;

            if (evt.getVolume(out var originalVolume, out var finalVolume) == FMOD.RESULT.OK)
            {
                var fadeRate = originalVolume / fadeDuration;
                var currentVolume = originalVolume;

                while (currentVolume > 0f)
                {
                    evt.setVolume(currentVolume);
                    currentVolume -= Time.deltaTime * fadeRate;

                    yield return null;

                    if (customEmitter.playing)
                    {
                        evt.setVolume(originalVolume);
                        yield break;
                    }
                }

                evt.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                evt.setVolume(originalVolume);
            } 
            else
            {
                Logger.Warning("Could not get current volume of fading music event instance");
            }
        }
    }
}
