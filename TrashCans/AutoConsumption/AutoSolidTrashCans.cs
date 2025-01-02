using HarmonyLib;

using UnityEngine;

using System.Linq;
using System.Collections.Generic;
using SonJeremy.TrashCans.MachineState;

namespace SonJeremy.TrashCans.AutoConsumption
{
    public class AutoSolidTrashCans : KMonoBehaviour, IConduitConsumer
    {
        #pragma warning disable CS0649
        [MyCmpGet] private Storage SolidStorage;
        #pragma warning restore CS0649

        public ConduitType ConduitType;
        
        Storage IConduitConsumer.Storage => SolidStorage;
        ConduitType IConduitConsumer.ConduitType => ConduitType;


        private int InputCell;
        private List<Tag> FilteredTags;
        private TreeFilterable FilterTree;

        private FilteredStorage SolidTrashCansFilterable;


        private bool IsConnected
        {
            get
            {
                var CellObject = Grid.Objects[InputCell, 20];

                if (CellObject == null) return false;

                return CellObject.GetComponent<BuildingComplete>() != null;
            }
        }


        public bool IsFiltered => FilteredTags != null && FilteredTags.Count > 0;


        private SolidConduitFlow GetConduitFlow() => Game.Instance.solidConduitFlow;

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();

            var ControlledCapacity = new FilterableTrashCans { Storage = SolidStorage };

            var TrashCansChore = Db.Get().ChoreTypes.Get(Db.Get().ChoreTypes.StorageFetch.Id);

            SolidTrashCansFilterable = new FilteredStorage(this, null, (IUserControlledCapacity) ControlledCapacity.GetUserControlledCapacity(), false, TrashCansChore);

            SolidTrashCansFilterable.SetHasMeter(false);
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();

            GetConduitFlow().AddConduitUpdater(ConduitUpdate);
            InputCell = this.FindComponent<Building>().GetUtilityInputCell();
        }

        protected override void OnCleanUp()
        {
            base.OnCleanUp();
            SolidTrashCansFilterable.CleanUp();
            GetConduitFlow().RemoveConduitUpdater(ConduitUpdate);
        }

        private void ConduitUpdate(float TickTime)
        {
            RefreshFilteredTag();
            var SolidDeliveryFlow = GetConduitFlow();

            if (IsConnected == true)
            {
                var IsTrashCansOperational = GetComponent<Operational>().IsOperational;
                var SolidContent = SolidDeliveryFlow.GetContents(InputCell);

                if (SolidContent.pickupableHandle.IsValid() && IsTrashCansOperational)
                {
                    var StorageCapacity = SolidStorage.capacityKg;
                    var MassAvailable = SolidStorage.MassStored();
                    
                    var StorageAvailable = Mathf.Max(0.0f, StorageCapacity - MassAvailable);

                    if (StorageAvailable > 0)
                    {
                        var SolidOnRail = SolidDeliveryFlow.GetPickupable(SolidContent.pickupableHandle);
                        
                        var SolidContentMass = SolidOnRail.PrimaryElement.Mass;
                        
                        if (SolidContentMass <= StorageAvailable || SolidContentMass > StorageCapacity)
                        {
                            var SolidUnpacked = SolidDeliveryFlow.RemovePickupable(InputCell);
                            
                            var IsUnpacked = (bool) SolidUnpacked;
                            var ElementTag = SolidUnpacked.GetComponent<KPrefabID>().PrefabTag;

                            var IsFilteredThisElement = FilteredTags.Contains(ElementTag);

                            if (IsUnpacked)
                            {
                                if (FilteredTags.Count == 0 || (FilteredTags.Count != 0 && IsFilteredThisElement == true))
                                {
                                    GetComponent<TrashCansMachineState>().PlayWorkable();
                                    SolidStorage.Store(SolidUnpacked.gameObject, true, false, false);

                                    return;
                                }
                                
                                SolidUnpacked.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Ore);
                            }
                        }
                    }
                }
            }

            if (SolidStorage != null) SolidStorage.storageNetworkID = GetConnectedNetworkID();
        }

        private int GetConnectedNetworkID()
        {
            var TrashCansGameObject = Grid.Objects[InputCell, 20];

            var SolidConduitObject = TrashCansGameObject?.GetComponent<SolidConduit>();
            var NetworkObject = SolidConduitObject?.GetNetwork();

            return NetworkObject?.id ?? -1;
        }

        private void RefreshFilteredTag()
        {
            FilterTree = this.FindComponent<TreeFilterable>();

            var ChangedTags = Traverse.Create(FilterTree).Method("GetTags").GetValue<HashSet<Tag>>();

            var ChangedFilteredTags = ChangedTags.Where(Tag => Tag != null).ToList();

            FilteredTags = ChangedFilteredTags;
        }
    }
}
