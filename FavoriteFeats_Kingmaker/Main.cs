using System.Reflection;
using UnityModManagerNet;

namespace FavoriteFeats_Kingmaker
{
#if DEBUG
    [EnableReloading]
#endif

    static class Main
    {
        internal static UnityModManagerNet.UnityModManager.ModEntry.ModLogger Logger;
        public static bool Enabled;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
#if DEBUG
            modEntry.OnUnload = Unload;
#endif

            modEntry.OnToggle = OnToggle;
            Logger = modEntry.Logger;

            Harmony12.HarmonyInstance harmony = Harmony12.HarmonyInstance.Create(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            StateManager.ClearFavoritesCache();
            return true;
        }


#if DEBUG
        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            Harmony12.HarmonyInstance.Create(modEntry.Info.Id).UnpatchAll();
            StateManager.ClearFavoritesCache();
            return true;
        }
#endif


        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;

            return true;
        }
    }

}