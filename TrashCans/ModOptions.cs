using System;
using Newtonsoft.Json;

using PeterHan.PLib;
using PeterHan.PLib.Options;

namespace SonJeremy.TrashCans
{
    [Serializable]
    [RestartRequired]
    [ConfigFile(IndentOutput: true, SharedConfigLocation: true)]
    public sealed class ModOptions : SingletonOptions<ModOptions>
    {
        #region ArtifactTrashCanOptions
        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.REQUIRE_POWER", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [JsonProperty]
        public bool ArtifactTrashCansRequirePower { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_TRASH", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [JsonProperty]
        public bool ArtifactTrashCansEnableAutoTrash { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_OVER_HEAT", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [JsonProperty]
        public bool ArtifactTrashCansCanOverheat { get; set; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_FLOOD", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [JsonProperty]
        public bool ArtifactTrashCansCanFlood { get; set; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENERGY_CONSUMPTION_WHEN_ACTIVE", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(10f, 120f)]
        [JsonProperty]
        public float ArtifactTrashCansEnergyConsumptionWhenActive { get; set; } = 60f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.MAX_AUTO_TRASH_INTERVAL", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(30f, 4200f)]
        [JsonProperty]
        public float ArtifactTrashCansMaxAutoTrashInterval { get; set; } = 30f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAPACITY_KG", "", "STRINGS.UI.TRASH_CANS_OPTIONS.ARTIFACT_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(100f, 500000f)]
        [JsonProperty]
        public float ArtifactTrashCansCapacityKg { get; set; } = 2500f;
        #endregion




        #region LiquidTrashCanOptions
        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.REQUIRE_POWER", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [JsonProperty]
        public bool LiquidTrashCansRequirePower { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_TRASH", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [JsonProperty]
        public bool LiquidTrashCansEnableAutoTrash { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_OVER_HEAT", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [JsonProperty]
        public bool LiquidTrashCansCanOverheat { get; set; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_FLOOD", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [JsonProperty]
        public bool LiquidTrashCansCanFlood { get; set; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENERGY_CONSUMPTION_WHEN_ACTIVE", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(10f, 120f)]
        [JsonProperty]
        public float LiquidTrashCansEnergyConsumptionWhenActive { get; set; } = 60f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.MAX_AUTO_TRASH_INTERVAL", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(30f, 4200f)]
        [JsonProperty]
        public float LiquidTrashCansMaxAutoTrashInterval { get; set; } = 30f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAPACITY_KG", "", "STRINGS.UI.TRASH_CANS_OPTIONS.LIQUID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(100f, 500000f)]
        [JsonProperty]
        public float LiquidTrashCansCapacityKg { get; set; } = 2500f;
        #endregion




        #region SolidTrashCanOptions
        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.REQUIRE_POWER", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansRequirePower { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_TRASH", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansEnableAutoTrash { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_DELIVERY", "STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_DELIVERY_TOOLTIP", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansEnableAutoDelivery { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_OVER_HEAT", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansCanOverheat { get; set; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_FLOOD", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [JsonProperty]
        public bool SolidTrashCansCanFlood { get; set; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENERGY_CONSUMPTION_WHEN_ACTIVE", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(10f, 120f)]
        [JsonProperty]
        public float SolidTrashCansEnergyConsumptionWhenActive { get; set; } = 60f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.MAX_AUTO_TRASH_INTERVAL", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(30f, 4200f)]
        [JsonProperty]
        public float SolidTrashCansMaxAutoTrashInterval { get; set; } = 30f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAPACITY_KG", "", "STRINGS.UI.TRASH_CANS_OPTIONS.SOLID_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(100f, 500000f)]
        [JsonProperty]
        public float SolidTrashCansCapacityKg { get; set; } = 2500f;
        #endregion




        #region GasTrashCanOptions
        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.REQUIRE_POWER", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [JsonProperty]
        public bool GasTrashCansRequirePower { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENABLE_AUTO_TRASH", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [JsonProperty]
        public bool GasTrashCansEnableAutoTrash { get; set; } = true;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_OVER_HEAT", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [JsonProperty]
        public bool GasTrashCansCanOverheat { get; set; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAN_FLOOD", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [JsonProperty]
        public bool GasTrashCansCanFlood { get; set; }


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.ENERGY_CONSUMPTION_WHEN_ACTIVE", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(10f, 120f)]
        [JsonProperty]
        public float GasTrashCansEnergyConsumptionWhenActive { get; set; } = 60f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.MAX_AUTO_TRASH_INTERVAL", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(30f, 4200f)]
        [JsonProperty]
        public float GasTrashCansMaxAutoTrashInterval { get; set; } = 30f;


        [Option("STRINGS.UI.TRASH_CANS_OPTIONS.CAPACITY_KG", "", "STRINGS.UI.TRASH_CANS_OPTIONS.GAS_CATEGORY")]
        [DynamicOption(typeof(FloatOptionsEntry))]
        [Limit(100f, 500000f)]
        [JsonProperty]
        public float GasTrashCansCapacityKg { get; set; } = 2500f;
        #endregion
    }
}
