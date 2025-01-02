using System;
using Newtonsoft.Json;

using PeterHan.PLib;
using PeterHan.PLib.Options;

namespace SonJeremy.TrashCans.Mods
{
    [Serializable]
    [RestartRequired]
    [ConfigFile(IndentOutput: true, SharedConfigLocation: true)]
    public sealed class ModOptions : SingletonOptions<ModOptions>
    {
        #region ArtifactTrashCanOptions
        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.REQUIRE_POWER", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [JsonProperty]
        public bool ArtifactTrashCansRequirePower { set; get; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_TRASH", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [JsonProperty]
        public bool ArtifactTrashCansEnableAutoTrash { set; get; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_OVER_HEAT", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [JsonProperty]
        public bool ArtifactTrashCansCanOverheat { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_FLOOD", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [JsonProperty]
        public bool ArtifactTrashCansCanFlood { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENERGY_CONSUMPTION_WHEN_ACTIVE", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(10f, 120f)]
        [JsonProperty]
        public float ArtifactTrashCansEnergyConsumptionWhenActive { set; get; } = 60f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.MAX_AUTO_TRASH_INTERVAL", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(30f, 4200f)]
        [JsonProperty]
        public float ArtifactTrashCansMaxAutoTrashInterval { set; get; } = 30f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAPACITY_KG", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(100f, 500000f)]
        [JsonProperty]
        public float ArtifactTrashCansCapacityKg { set; get; } = 2500f;
        #endregion




        #region LiquidTrashCanOptions
        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.REQUIRE_POWER", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [JsonProperty]
        public bool LiquidTrashCansRequirePower { set; get; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_TRASH", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [JsonProperty]
        public bool LiquidTrashCansEnableAutoTrash { set; get; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_OVER_HEAT", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [JsonProperty]
        public bool LiquidTrashCansCanOverheat { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_FLOOD", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [JsonProperty]
        public bool LiquidTrashCansCanFlood { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENERGY_CONSUMPTION_WHEN_ACTIVE", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(10f, 120f)]
        [JsonProperty]
        public float LiquidTrashCansEnergyConsumptionWhenActive { set; get; } = 60f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.MAX_AUTO_TRASH_INTERVAL", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(30f, 4200f)]
        [JsonProperty]
        public float LiquidTrashCansMaxAutoTrashInterval { set; get; } = 30f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAPACITY_KG", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(100f, 500000f)]
        [JsonProperty]
        public float LiquidTrashCansCapacityKg { set; get; } = 2500f;
        #endregion




        #region SolidTrashCanOptions
        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.REQUIRE_POWER", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansRequirePower { set; get; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_TRASH", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansEnableAutoTrash { set; get; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_DELIVERY", "STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_DELIVERY_TOOLTIP", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansEnableAutoDelivery { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_OVER_HEAT", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansCanOverheat { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_FLOOD", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansCanFlood { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENERGY_CONSUMPTION_WHEN_ACTIVE", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(10f, 120f)]
        [JsonProperty]
        public float SolidTrashCansEnergyConsumptionWhenActive { set; get; } = 60f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.MAX_AUTO_TRASH_INTERVAL", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(30f, 4200f)]
        [JsonProperty]
        public float SolidTrashCansMaxAutoTrashInterval { set; get; } = 30f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAPACITY_KG", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(100f, 500000f)]
        [JsonProperty]
        public float SolidTrashCansCapacityKg { set; get; } = 2500f;
        #endregion




        #region GasTrashCanOptions
        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.REQUIRE_POWER", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [JsonProperty]
        public bool GasTrashCansRequirePower { set; get; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_TRASH", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [JsonProperty]
        public bool GasTrashCansEnableAutoTrash { set; get; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_OVER_HEAT", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [JsonProperty]
        public bool GasTrashCansCanOverheat { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_FLOOD", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [JsonProperty]
        public bool GasTrashCansCanFlood { set; get; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENERGY_CONSUMPTION_WHEN_ACTIVE", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(10f, 120f)]
        [JsonProperty]
        public float GasTrashCansEnergyConsumptionWhenActive { set; get; } = 60f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.MAX_AUTO_TRASH_INTERVAL", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(30f, 4200f)]
        [JsonProperty]
        public float GasTrashCansMaxAutoTrashInterval { set; get; } = 30f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAPACITY_KG", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(100f, 500000f)]
        [JsonProperty]
        public float GasTrashCansCapacityKg { set; get; } = 2500f;
        #endregion
    }
}
