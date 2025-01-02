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
    public sealed class ArtifactTrashCanConfig : IBuildingConfig
    {
        private static PBuilding ArtifactTrashCan;

        public static PBuilding CreateBuilding()
        {
            ArtifactTrashCan = new PBuilding(ModStrings.ArtifactTrashCansID, ModStrings.ArtifactTrashCansName)
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
                Animation = "trashcan_artifact_kanim",

                Noise = NOISE_POLLUTION.NONE,
                Decor = BUILDINGS.DECOR.BONUS.TIER1,
                Floods = ModOptions.Instance.ArtifactTrashCansCanFlood,
                
                ViewMode = OverlayModes.None.ID,
                AddAfter = ModStrings.LiquidTrashCansID,

                Ingredients = { new BuildIngredient(MATERIALS.REFINED_METALS, tier: 2) },
                LogicIO = { PBuilding.CompatLogicPort(LogicPortSpriteType.Input, new CellOffset(0, 1)) }
            };

            if (ModOptions.Instance.ArtifactTrashCansRequirePower)
            {
                var RequiredPowerWatt = ModOptions.Instance.ArtifactTrashCansEnergyConsumptionWhenActive;
                ArtifactTrashCan.PowerInput = new PowerRequirement(Mathf.Max(60f, RequiredPowerWatt), new CellOffset(0, 1));
            }

            if (ModOptions.Instance.ArtifactTrashCansCanOverheat)
                ArtifactTrashCan.OverheatTemperature = 1600f;

            return ArtifactTrashCan;
        }

        public override BuildingDef CreateBuildingDef()
        {
            LocString.CreateLocStringKeys(typeof(ModStrings.BUILDINGS));
            PGameUtils.CopySoundsToAnim(ArtifactTrashCan.Animation, "trashcan_artifact_kanim");

            var ArtifactTrashCanDef = ArtifactTrashCan.CreateDef();

            return ArtifactTrashCanDef;
        }

        public override void DoPostConfigureUnderConstruction(GameObject ArtifactTrashCansGameObject)
        {
            base.DoPostConfigureUnderConstruction(ArtifactTrashCansGameObject);
            ArtifactTrashCan?.CreateLogicPorts(ArtifactTrashCansGameObject);
        }

        public override void DoPostConfigurePreview(BuildingDef ArtifactTrashCansConfig, GameObject ArtifactTrashCansGameObject)
        {
            base.DoPostConfigurePreview(ArtifactTrashCansConfig, ArtifactTrashCansGameObject);
            ArtifactTrashCan?.CreateLogicPorts(ArtifactTrashCansGameObject);
        }

        public override void DoPostConfigureComplete(GameObject ArtifactTrashCansGameObject)
        {
            var ArtifactFilterTags = new List<Tag>
            {
                GameTags.Artifact,
                GameTags.MiscPickupable,
                GameTags.CharmedArtifact,
                GameTags.TerrestrialArtifact
            };

            ArtifactFilterTags = ArtifactFilterTags.Union(ArtifactFilterTags).ToList();

            ArtifactTrashCan.CreateLogicPorts(ArtifactTrashCansGameObject);

            SoundEventVolumeCache.instance.AddVolume("trashcan_artifact_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);

            Prioritizable.AddRef(ArtifactTrashCansGameObject);
            var ArtifactStorage = ArtifactTrashCansGameObject.AddOrGet<Storage>();

            ArtifactStorage.showInUI = true;
            ArtifactStorage.showDescriptor = true;
            ArtifactStorage.allowItemRemoval = true;
            ArtifactStorage.showCapacityStatusItem = true;
            ArtifactStorage.showCapacityAsMainStatus = true;

            ArtifactStorage.storageFilters = ArtifactFilterTags;

            ArtifactStorage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            ArtifactStorage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            ArtifactStorage.capacityKg = ModOptions.Instance.ArtifactTrashCansCapacityKg;

            ArtifactStorage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier> {
                Storage.StoredItemModifier.Seal,
                Storage.StoredItemModifier.Hide,
                Storage.StoredItemModifier.Insulate,
                Storage.StoredItemModifier.Preserve
            });

            if (ModOptions.Instance.ArtifactTrashCansRequirePower)
                ArtifactTrashCansGameObject.AddOrGet<EnergyConsumer>();

            ArtifactTrashCansGameObject.AddOrGet<UserNameable>();
            ArtifactTrashCansGameObject.AddOrGet<LogicOperationalController>();
            ArtifactTrashCansGameObject.AddOrGetDef<RocketUsageRestriction.Def>();

            ArtifactTrashCansGameObject.AddComponent<TrashCans>();
            ArtifactTrashCansGameObject.AddOrGet<AutoSolidTrashCans>();
            ArtifactTrashCansGameObject.AddOrGet<TrashCansMachineState>();
        }
    }
}
