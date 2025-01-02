/* ReSharper disable
 * 
 * UnusedType.Global
 * UnusedMember.Global
 * MemberCanBePrivate.Global
 * FieldCanBeMadeReadOnly.Global
 * 
 * @TODO: This is Mod Patch, no above Inspection is required.!
 */

using KMod;
using TUNING;
using HarmonyLib;

using SonJeremy.TrashCans.Mods;
using SonJeremy.TrashCans.BuildingConfig;

using PeterHan.PLib.AVC;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using PeterHan.PLib.Database;
using PeterHan.PLib.Buildings;
using PeterHan.PLib.PatchManager;

namespace SonJeremy.TrashCans
{
    public sealed class TrashCansPatch : UserMod2
    {
        public override void OnLoad(Harmony HarmoryInstance)
        {
            base.OnLoad(HarmoryInstance);

            PUtil.InitLibrary();

            LocString.CreateLocStringKeys(typeof(ModStrings.UI));
            LocString.CreateLocStringKeys(typeof(ModStrings.MISC));
            LocString.CreateLocStringKeys(typeof(ModStrings.BUILDING));
            LocString.CreateLocStringKeys(typeof(ModStrings.BUILDINGS));

            new PLocalization().Register();
            new POptions().RegisterOptions(this, typeof(ModOptions));

            var BuildingManager = new PBuildingManager();
            
            BuildingManager.Register(GasTrashCanConfig.CreateBuilding());
            BuildingManager.Register(SolidTrashCanConfig.CreateBuilding());
            BuildingManager.Register(LiquidTrashCanConfig.CreateBuilding());
            BuildingManager.Register(ArtifactTrashCanConfig.CreateBuilding());

            new PVersionCheck().Register(this, new SteamVersionChecker());
            new PPatchManager(HarmoryInstance).RegisterPatchClass(typeof(TrashCansPatch));
        }

        [PLibMethod(RunAt.BeforeDbInit)]
        internal static void OnBeforeDbInit()
        {
            /* Add Artifact to FilterableTree */ 
            if (STORAGEFILTERS.SPECIAL_STORAGE.Contains(GameTags.MiscPickupable) == false)
                STORAGEFILTERS.SPECIAL_STORAGE.Add(GameTags.MiscPickupable);

            if (STORAGEFILTERS.SPECIAL_STORAGE.Contains(GameTags.Artifact) == false)
                STORAGEFILTERS.SPECIAL_STORAGE.Add(GameTags.Artifact);

            if (STORAGEFILTERS.SPECIAL_STORAGE.Contains(GameTags.CharmedArtifact) == false)
                STORAGEFILTERS.SPECIAL_STORAGE.Add(GameTags.CharmedArtifact);

            if (STORAGEFILTERS.SPECIAL_STORAGE.Contains(GameTags.TerrestrialArtifact) == false)
                STORAGEFILTERS.SPECIAL_STORAGE.Add(GameTags.TerrestrialArtifact);
        }

        [HarmonyPatch(typeof(TreeFilterable), "RefreshTint")]
        public static class TreeFilterableRefreshTint
        {
            public static bool Prefix(TreeFilterable __instance)
            {
                if (__instance == null) return true;
                
                object HasFluidTrashCans = __instance.FindComponent<TrashCans>();
                var IsArtifactArtifactTrashCans = __instance.FindComponent<Building>().Def.PrefabID.ToUpper();

                if (HasFluidTrashCans == null || IsArtifactArtifactTrashCans == "ARTIFACTTRASHCANS") return true;
                
                __instance.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoStorageFilterSet, false, __instance.gameObject);

                return false;
            }
        }
    }
}