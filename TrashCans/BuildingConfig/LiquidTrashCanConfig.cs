using TUNING;
using UnityEngine;

using System.Collections.Generic;

using PeterHan.PLib.Core;
using PeterHan.PLib.Buildings;

using SonJeremy.TrashCans.Mods;
using SonJeremy.TrashCans.MachineState;
using SonJeremy.TrashCans.AutoConsumption;

namespace SonJeremy.TrashCans.BuildingConfig
{
    public sealed class LiquidTrashCanConfig : IBuildingConfig
    {
        private static PBuilding LiquidTrashCan;

        public static PBuilding CreateBuilding()
        {
            LiquidTrashCan = new PBuilding(ModStrings.LiquidTrashCansID, ModStrings.LiquidTrashCansName)
            {
                HP = 100,
                Width = 1,
                Height = 2,
                HeatGeneration = 0f,
                ConstructionTime = 60f,

                Placement = BuildLocationRule.OnFloor,
                RotateMode = PermittedRotations.Unrotatable,

                Category = "Base",
                Tech = "SmartStorage",
                SubCategory = "storage",
                AudioCategory = "Metal",
                Animation = "trashcan_liquid_kanim",

                Noise = NOISE_POLLUTION.NONE,
                Decor = BUILDINGS.DECOR.BONUS.TIER1,
                Floods = ModOptions.Instance.LiquidTrashCansCanFlood,
                
                AddAfter = ModStrings.SolidTrashCansID,
                ViewMode = OverlayModes.LiquidConduits.ID,

                Ingredients = { new BuildIngredient(MATERIALS.REFINED_METALS, tier: 2) },
                LogicIO = { PBuilding.CompatLogicPort(LogicPortSpriteType.Input, new CellOffset(0, 1)) }
            };

            if (ModOptions.Instance.LiquidTrashCansRequirePower)
            {
                var RequiredPowerWatt = ModOptions.Instance.LiquidTrashCansEnergyConsumptionWhenActive;
                LiquidTrashCan.PowerInput = new PowerRequirement(Mathf.Max(60f, RequiredPowerWatt), new CellOffset(0, 1));
            }

            if (ModOptions.Instance.LiquidTrashCansCanOverheat)
                LiquidTrashCan.OverheatTemperature = 1600f;

            return LiquidTrashCan;
        }

        public override BuildingDef CreateBuildingDef()
        {
            LocString.CreateLocStringKeys(typeof(ModStrings.BUILDINGS));
            PGameUtils.CopySoundsToAnim(LiquidTrashCan.Animation, "trashcan_liquid_kanim");

            var LiquidTrashCanDef = LiquidTrashCan.CreateDef();

            return LiquidTrashCanDef;
        }

        public override void DoPostConfigureUnderConstruction(GameObject LiquidTrashCansGameObject)
        {
            base.DoPostConfigureUnderConstruction(LiquidTrashCansGameObject);
            LiquidTrashCan?.CreateLogicPorts(LiquidTrashCansGameObject);
        }

        public override void DoPostConfigurePreview(BuildingDef LiquidTrashCansConfig, GameObject LiquidTrashCansGameObject)
        {
            base.DoPostConfigurePreview(LiquidTrashCansConfig, LiquidTrashCansGameObject);
            LiquidTrashCan?.CreateLogicPorts(LiquidTrashCansGameObject);
        }

        public override void DoPostConfigureComplete(GameObject LiquidTrashCansGameObject)
        {
            var LiquidFilterTags = new List<Tag>();
            LiquidFilterTags.AddRange(STORAGEFILTERS.LIQUIDS);

            LiquidTrashCan.CreateLogicPorts(LiquidTrashCansGameObject);

            SoundEventVolumeCache.instance.AddVolume("trashcan_liquid_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);

            Prioritizable.AddRef(LiquidTrashCansGameObject);

            var LiquidStorage = LiquidTrashCansGameObject.AddOrGet<Storage>();

            LiquidStorage.showInUI = true;
            LiquidStorage.showDescriptor = true;
            LiquidStorage.allowItemRemoval = true;
            LiquidStorage.showCapacityStatusItem = true;
            LiquidStorage.showCapacityAsMainStatus = true;

            LiquidStorage.storageFilters = LiquidFilterTags;
            LiquidStorage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            LiquidStorage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            LiquidStorage.capacityKg = ModOptions.Instance.LiquidTrashCansCapacityKg;

            LiquidStorage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier> {
                Storage.StoredItemModifier.Seal,
                Storage.StoredItemModifier.Hide,
                Storage.StoredItemModifier.Insulate
            });

            if (ModOptions.Instance.LiquidTrashCansRequirePower)
                LiquidTrashCansGameObject.AddOrGet<EnergyConsumer>();

            LiquidTrashCansGameObject.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
            LiquidTrashCansGameObject.AddComponent<ConduitSecondaryInput>().portInfo = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(0, 0));

            LiquidTrashCansGameObject.AddOrGet<UserNameable>();
            LiquidTrashCansGameObject.AddOrGet<LogicOperationalController>();
            LiquidTrashCansGameObject.AddOrGetDef<RocketUsageRestriction.Def>();

            var AutoTrashCans = LiquidTrashCansGameObject.AddOrGet<AutoFluidTrashCans>();
            AutoTrashCans.FluidConduitType = ConduitType.Liquid;

            LiquidTrashCansGameObject.AddComponent<TrashCans>();
            LiquidTrashCansGameObject.AddOrGet<TrashCansMachineState>();
        }
    }
}
