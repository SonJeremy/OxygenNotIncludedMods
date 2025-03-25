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

using PeterHan.PLib.AVC;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using PeterHan.PLib.Database;
using PeterHan.PLib.Buildings;
using PeterHan.PLib.PatchManager;
using SonJeremy.WirelessPower.BuildingConfig;

namespace SonJeremy.WirelessPower
{
    public sealed class WirelessPowerPatch : UserMod2
    {
        public override void OnLoad(Harmony HarmonyInstance)
        {
            base.OnLoad(HarmonyInstance);

            PUtil.InitLibrary();
            
            LocString.CreateLocStringKeys(typeof(ModStrings.UI));
            //LocString.CreateLocStringKeys(typeof(ModStrings.MISC));
            //LocString.CreateLocStringKeys(typeof(ModStrings.BUILDING));
            LocString.CreateLocStringKeys(typeof(ModStrings.BUILDINGS));

            new PLocalization().Register();
            new POptions().RegisterOptions(this, typeof(ModOptions));

            var BuildingManager = new PBuildingManager();
            BuildingManager.Register(WirelessBatteryConfig.CreateBuilding());

            new PVersionCheck().Register(this, new SteamVersionChecker());
            new PPatchManager(HarmonyInstance).RegisterPatchClass(typeof(WirelessPowerPatch));
        }
    }
}