using HarmonyLib;
using IsekaiMod.ModLogic;
using System;
using TabletopTweaks.Core.Utilities;
using UnityModManagerNet;

namespace IsekaiMod {

    internal static class Main {

        public static ModContextTTTBase IsekaiContext;

        public static bool Load(UnityModManager.ModEntry modEntry) {
            IsekaiContext = new ModContextTTTBase(modEntry);
            try {
                var harmony = new Harmony(modEntry.Info.Id);
                IsekaiContext.ModEntry.OnSaveGUI = OnSaveGUI;
                IsekaiContext.ModEntry.OnGUI = UMMSettingsUI.OnGUI;
                harmony.PatchAll();
                PostPatchInitializer.Initialize(IsekaiContext);
                return true;
            }
            catch (Exception e) {
                Log(e.ToString());
                throw e;
            }
        }

        public static void Log(string msg) {
            
            IsekaiContext.Logger.Log(msg);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public static void LogDebug(string msg) {
            IsekaiContext.Logger.Log(msg);
        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry) {
            IsekaiContext.SaveAllSettings();
        }
    }
}