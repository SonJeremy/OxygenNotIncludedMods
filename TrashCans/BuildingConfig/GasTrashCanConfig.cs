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
    public sealed class GasTrashCanConfig : IBuildingConfig
    {
        private static PBuilding GasTrashCan;

        public static PBuilding CreateBuilding()
        {
            GasTrashCan = new PBuilding(ModStrings.GasTrashCansID, ModStrings.GasTrashCansName)
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
                Animation = "trashcan_gas_kanim",

                Noise = NOISE_POLLUTION.NONE,
                Decor = BUILDINGS.DECOR.BONUS.TIER1,
                Floods = ModOptions.Instance.GasTrashCansCanFlood,
                
                AddAfter = StorageLockerSmartConfig.ID,
                ViewMode = OverlayModes.GasConduits.ID,

                Ingredients = { new BuildIngredient(MATERIALS.REFINED_METALS, tier: 2) },
                LogicIO = { PBuilding.CompatLogicPort(LogicPortSpriteType.Input, new CellOffset(0, 1)) }
            };

            GasTrashCan.HP = 100;
            GasTrashCan.Width = 1;
            GasTrashCan.Height = 2;
            GasTrashCan.HeatGeneration = 0f;
            GasTrashCan.ConstructionTime = 60f;

            GasTrashCan.Placement = BuildLocationRule.OnFloor;
            GasTrashCan.RotateMode = PermittedRotations.Unrotatable;

            GasTrashCan.Category = "Base";
            GasTrashCan.Tech = "SmartStorage";
            GasTrashCan.SubCategory = "storage";
            GasTrashCan.AudioCategory = "Metal";
            GasTrashCan.Animation = "trashcan_gas_kanim";

            GasTrashCan.Noise = NOISE_POLLUTION.NONE;
            GasTrashCan.Decor = BUILDINGS.DECOR.BONUS.TIER1;
            GasTrashCan.Floods = ModOptions.Instance.GasTrashCansCanFlood;

            if (ModOptions.Instance.GasTrashCansRequirePower)
            {
                var RequiredPowerWatt = ModOptions.Instance.GasTrashCansEnergyConsumptionWhenActive;
                GasTrashCan.PowerInput = new PowerRequirement(Mathf.Max(60f, RequiredPowerWatt), new CellOffset(0, 1));
            }

            if (ModOptions.Instance.GasTrashCansCanOverheat)
                GasTrashCan.OverheatTemperature = 1600f;

            return GasTrashCan;
        }

        public override BuildingDef CreateBuildingDef()
        {
            LocString.CreateLocStringKeys(typeof(ModStrings.BUILDINGS));
            PGameUtils.CopySoundsToAnim(GasTrashCan.Animation, "trashcan_gas_kanim");

            var GasTrashCanDef = GasTrashCan.CreateDef();

            return GasTrashCanDef;
        }

        public override void DoPostConfigureUnderConstruction(GameObject GasTrashCansGameObject)
        {
            base.DoPostConfigureUnderConstruction(GasTrashCansGameObject);
            GasTrashCan?.CreateLogicPorts(GasTrashCansGameObject);
        }

        public override void DoPostConfigurePreview(BuildingDef GasTrashCansConfig, GameObject GasTrashCansGameObject)
        {
            base.DoPostConfigurePreview(GasTrashCansConfig, GasTrashCansGameObject);
            GasTrashCan?.CreateLogicPorts(GasTrashCansGameObject);
        }

        public override void DoPostConfigureComplete(GameObject GasTrashCansGameObject)
        {
            var GasFilterTags = new List<Tag>();
            GasFilterTags.AddRange(STORAGEFILTERS.GASES);

            GasTrashCan.CreateLogicPorts(GasTrashCansGameObject);

            SoundEventVolumeCache.instance.AddVolume("trashcan_gas_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);

            Prioritizable.AddRef(GasTrashCansGameObject);

            var GasStorage = GasTrashCansGameObject.AddOrGet<Storage>();

            GasStorage.showInUI = true;
            GasStorage.showDescriptor = true;
            GasStorage.allowItemRemoval = true;
            GasStorage.showCapacityStatusItem = true;
            GasStorage.showCapacityAsMainStatus = true;

            GasStorage.storageFilters = GasFilterTags;
            GasStorage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            GasStorage.capacityKg = ModOptions.Instance.GasTrashCansCapacityKg;
            GasStorage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;

            GasStorage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier> {
                Storage.StoredItemModifier.Seal,
                Storage.StoredItemModifier.Hide,
                Storage.StoredItemModifier.Insulate
            });

            if (ModOptions.Instance.GasTrashCansRequirePower)
                GasTrashCansGameObject.AddOrGet<EnergyConsumer>();

            GasTrashCansGameObject.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
            GasTrashCansGameObject.AddComponent<ConduitSecondaryInput>().portInfo = new ConduitPortInfo(ConduitType.Gas, new CellOffset(0, 0));

            GasTrashCansGameObject.AddOrGet<UserNameable>();
            GasTrashCansGameObject.AddOrGet<LogicOperationalController>();
            GasTrashCansGameObject.AddOrGetDef<RocketUsageRestriction.Def>();

            GasTrashCansGameObject.AddOrGet<AutoFluidTrashCans>().FluidConduitType = ConduitType.Gas;

            GasTrashCansGameObject.AddComponent<TrashCans>();
            GasTrashCansGameObject.AddOrGet<TrashCansMachineState>();
        }
    }
}
