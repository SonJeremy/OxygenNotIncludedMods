using TUNING;
using UnityEngine;

using System.Linq;
using System.Collections.Generic;

using PeterHan.PLib.Core;
using PeterHan.PLib.Buildings;

using SonJeremy.TrashCans.Mods;
using SonJeremy.TrashCans.MachineState;
using SonJeremy.TrashCans.AutoConsumption;

namespace SonJeremy.TrashCans.BuildingConfig
{
    public sealed class SolidTrashCanConfig : IBuildingConfig
    {
        private static PBuilding SolidTrashCan;

        public static PBuilding CreateBuilding()
        {
            if (ModOptions.Instance.SolidTrashCansEnableAutoDelivery)
                SolidTrashCan = new PBuilding(ModStrings.SolidTrashCansID, ModStrings.SolidTrashCansName)
                {
                    AddAfter = ModStrings.GasTrashCansID,
                    ViewMode = OverlayModes.SolidConveyor.ID,

                    Ingredients = { new BuildIngredient(MATERIALS.REFINED_METALS, tier: 2) },
                    InputConduits = { new ConduitConnection(ConduitType.Solid, new CellOffset(0, 0)) },
                    LogicIO = { PBuilding.CompatLogicPort(LogicPortSpriteType.Input, new CellOffset(0, 1)) }
                };
            else
                SolidTrashCan = new PBuilding(ModStrings.SolidTrashCansID, ModStrings.SolidTrashCansName)
                {
                    ViewMode = OverlayModes.None.ID,
                    AddAfter = ModStrings.GasTrashCansID,

                    Ingredients = { new BuildIngredient(MATERIALS.REFINED_METALS, tier: 2) },
                    LogicIO = { PBuilding.CompatLogicPort(LogicPortSpriteType.Input, new CellOffset(0, 1)) }
                };

            SolidTrashCan.HP = 100;
            SolidTrashCan.Width = 1;
            SolidTrashCan.Height = 2;
            SolidTrashCan.HeatGeneration = 0f;
            SolidTrashCan.ConstructionTime = 60f;

            SolidTrashCan.Placement = BuildLocationRule.OnFloor;
            SolidTrashCan.RotateMode = PermittedRotations.Unrotatable;

            SolidTrashCan.Category = "Base";
            SolidTrashCan.Tech = "SmartStorage";
            SolidTrashCan.SubCategory = "storage";
            SolidTrashCan.AudioCategory = "Metal";
            SolidTrashCan.Animation = "trashcan_solid_kanim";

            SolidTrashCan.Noise = NOISE_POLLUTION.NONE;
            SolidTrashCan.Decor = BUILDINGS.DECOR.BONUS.TIER1;
            SolidTrashCan.Floods = ModOptions.Instance.SolidTrashCansCanFlood;

            if (ModOptions.Instance.SolidTrashCansRequirePower)
            {
                var RequiredPowerWatt = ModOptions.Instance.SolidTrashCansEnergyConsumptionWhenActive;
                SolidTrashCan.PowerInput = new PowerRequirement(Mathf.Max(60f, RequiredPowerWatt), new CellOffset(0, 1));
            }

            if (ModOptions.Instance.SolidTrashCansCanOverheat)
                SolidTrashCan.OverheatTemperature = 1600f;

            return SolidTrashCan;
        }

        public override BuildingDef CreateBuildingDef()
        {
            LocString.CreateLocStringKeys(typeof(ModStrings.BUILDINGS));
            PGameUtils.CopySoundsToAnim(SolidTrashCan.Animation, "trashcan_solid_kanim");
            
            var SolidTrashCanDef = SolidTrashCan.CreateDef();
            
            if (ModOptions.Instance.SolidTrashCansEnableAutoDelivery)
            {
                SolidTrashCanDef.InputConduitType = ConduitType.Solid;
                GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, ModStrings.SolidTrashCansID);
            }
            
            return SolidTrashCanDef;
        }

        public override void DoPostConfigureUnderConstruction(GameObject SolidTrashCansGameObject)
        {
            base.DoPostConfigureUnderConstruction(SolidTrashCansGameObject);
            SolidTrashCan?.CreateLogicPorts(SolidTrashCansGameObject);
        }

        public override void DoPostConfigurePreview(BuildingDef SolidTrashCansConfig, GameObject SolidTrashCansGameObject)
        {
            base.DoPostConfigurePreview(SolidTrashCansConfig, SolidTrashCansGameObject);
            SolidTrashCan?.CreateLogicPorts(SolidTrashCansGameObject);
        }

        public override void DoPostConfigureComplete(GameObject SolidTrashCansGameObject)
        {
            var SolidFilterTags = new List<Tag>();

            SolidFilterTags.AddRange(STORAGEFILTERS.FOOD);
            SolidFilterTags.AddRange(STORAGEFILTERS.SPECIAL_STORAGE);
            SolidFilterTags.AddRange(STORAGEFILTERS.NOT_EDIBLE_SOLIDS);            
            SolidFilterTags.AddRange(STORAGEFILTERS.STORAGE_LOCKERS_STANDARD);

            SolidFilterTags = SolidFilterTags.Union(SolidFilterTags).ToList();

            SolidTrashCan.CreateLogicPorts(SolidTrashCansGameObject);

            SoundEventVolumeCache.instance.AddVolume("trashcan_solid_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);

            Prioritizable.AddRef(SolidTrashCansGameObject);
            var SolidStorage = SolidTrashCansGameObject.AddOrGet<Storage>();

            SolidStorage.showInUI = true;
            SolidStorage.showDescriptor = true;
            SolidStorage.allowItemRemoval = true;
            SolidStorage.showCapacityStatusItem = true;
            SolidStorage.showCapacityAsMainStatus = true;

            SolidStorage.storageFilters = SolidFilterTags;
            SolidStorage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            SolidStorage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            SolidStorage.capacityKg = ModOptions.Instance.SolidTrashCansCapacityKg;

            SolidStorage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier> {
                Storage.StoredItemModifier.Seal,
                Storage.StoredItemModifier.Hide,
                Storage.StoredItemModifier.Insulate,
                Storage.StoredItemModifier.Preserve
            });

            if (ModOptions.Instance.SolidTrashCansRequirePower)
                SolidTrashCansGameObject.AddOrGet<EnergyConsumer>();

            if (ModOptions.Instance.SolidTrashCansEnableAutoDelivery)
                SolidTrashCansGameObject.AddOrGet<AutoSolidTrashCans>();
            else
                SolidTrashCansGameObject.AddOrGet<StorageLocker>();

            SolidTrashCansGameObject.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;

            SolidTrashCansGameObject.AddOrGet<UserNameable>();
            SolidTrashCansGameObject.AddOrGet<LogicOperationalController>();
            SolidTrashCansGameObject.AddOrGetDef<RocketUsageRestriction.Def>();

            SolidTrashCansGameObject.AddComponent<TrashCans>();
            SolidTrashCansGameObject.AddOrGet<TrashCansMachineState>();
        }
    }
}
