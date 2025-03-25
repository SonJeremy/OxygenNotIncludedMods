using System;
using TUNING;
using UnityEngine;

using System.Linq;
using System.Collections.Generic;

using PeterHan.PLib.Core;
using PeterHan.PLib.Buildings;

namespace SonJeremy.WirelessPower.BuildingConfig
{
    public  sealed class WirelessBatteryConfig : IBuildingConfig
    {
        private static PBuilding WirelessBattery;
        
        public static PBuilding CreateBuilding()
        {
            WirelessBattery = new PBuilding(ModStrings.WirelessBatteryID, ModStrings.WirelessBatteryName)
            {
                HP = 30,
                Width = 2,
                Height = 3,
                
                HeatGeneration = 0f,
                ConstructionTime = 60f,

                Placement = BuildLocationRule.OnFloor,
                RotateMode = PermittedRotations.FlipH,
                
                Tech = "Acoustics",
                Category = "Power",
                AudioCategory = "Metal",
                SubCategory = "batteries",
                Animation = "wireless_battery_kanim",

                Noise = NOISE_POLLUTION.NOISY.TIER1,
                Decor = BUILDINGS.DECOR.PENALTY.TIER2,
                
                ViewMode = OverlayModes.Power.ID,

                Ingredients = { new BuildIngredient(MATERIALS.REFINED_METALS, 3) },
                LogicIO = { PBuilding.CompatLogicPort(LogicPortSpriteType.Input, new CellOffset(0, 0)) }
            };
            
            return WirelessBattery;
        }

        public override BuildingDef CreateBuildingDef()
        {
            LocString.CreateLocStringKeys(typeof(ModStrings.BUILDINGS));
            SoundEventVolumeCache.instance.AddVolume(WirelessBattery.Animation, "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER1);
            
            var WirelessBatteryDef = WirelessBattery.CreateDef();
            
            WirelessBatteryDef.Entombable = false;
            WirelessBatteryDef.AudioCategory = "Metal";
            WirelessBatteryDef.ExhaustKilowattsWhenActive = 0f;
            WirelessBatteryDef.ViewMode = OverlayModes.Power.ID;
            WirelessBatteryDef.SelfHeatKilowattsWhenActive = 0.5f;

            return WirelessBatteryDef;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
    }
}