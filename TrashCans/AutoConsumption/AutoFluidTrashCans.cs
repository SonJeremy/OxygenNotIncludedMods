using UnityEngine;

using System.Linq;
using System.Collections.Generic;

using PeterHan.PLib.Core;
using SonJeremy.TrashCans.MachineState;
using SonJeremy.SModUtil.OxygenNotIncluded;

namespace SonJeremy.TrashCans.AutoConsumption
{
    public class AutoFluidTrashCans : KMonoBehaviour, ISecondaryInput
    {
        [MyCmpGet] public Storage FluidStorage;
        
        [SerializeField] public ConduitType FluidConduitType;
        [SerializeField] public ConduitPortInfo FilterablePortInfo;
        
        private int InputCell;
        private List<Tag> FilteredTags;
        private TreeFilterable FilterTree;
        private Building TrashCansBuilding;
        
        private const float ConsumptionRate = 25f;
        
        private FilteredStorage SolidTrashCansFilterable;
        private readonly Operational.State OperatingState;
        private SimHashes LastConsumedFluid = SimHashes.Vacuum;
        private FlowUtilityNetwork.NetworkItem FluidNetworkFlow;

        
        public AutoFluidTrashCans(Operational.State OperatingState)
        {
            this.OperatingState = OperatingState;
        }


        private bool IsConnected
        {
            get
            {
                var FluidLayerID = FluidConduitType == ConduitType.Gas ? 12 : 16;
                var CellObject = Grid.Objects[InputCell, FluidLayerID];

                if (CellObject == null) return false;

                return CellObject.GetComponent<BuildingComplete>() != null;
            }
        }


        public bool IsFiltered => FilteredTags != null && FilteredTags.Count > 0;

        public CellOffset GetSecondaryConduitOffset(ConduitType _) => FilterablePortInfo?.offset ?? TrashCansBuilding.Def.UtilityInputOffset;

        public bool HasSecondaryConduitType(ConduitType HasType)
        {
            if (FilterablePortInfo == null) return false;
            return FilterablePortInfo.conduitType == HasType;
        }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();

            TrashCansBuilding = this.FindComponent<Building>();

            var ControlledCapacity = new FilterableTrashCans { Storage = FluidStorage };
            var TrashCansChore = Db.Get().ChoreTypes.Get(Db.Get().ChoreTypes.StorageFetch.Id);

            SolidTrashCansFilterable = new FilteredStorage(this, null, (IUserControlledCapacity) ControlledCapacity.GetUserControlledCapacity(), false, TrashCansChore);
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();

            RefreshFilteredTag();

            InputCell = GetInputCell();

            CreateConduitConsumer(FluidConduitType, InputCell, out FluidNetworkFlow);
            GetConduitFlow().AddConduitUpdater(ConduitUpdate, ConduitFlowPriority.First);
        }

        protected override void OnCleanUp()
        {
            base.OnCleanUp();
            SolidTrashCansFilterable.CleanUp();
            GetConduitFlow().RemoveConduitUpdater(ConduitUpdate);
            
            Conduit.GetNetworkManager(FilterablePortInfo.conduitType).RemoveFromNetworks(InputCell, FluidNetworkFlow, true);
            Conduit.GetNetworkManager(FilterablePortInfo.conduitType).ForceRebuildNetworks();
        }

        private int GetInputCell()
        {
            var SecondaryInputComponent = GetComponents<ISecondaryInput>();

            foreach (var SecondaryInput in SecondaryInputComponent)
            {
                if (SecondaryInput.HasSecondaryConduitType(FluidConduitType))
                {
                    return Grid.OffsetCell(TrashCansBuilding.NaturalBuildingCell(), SecondaryInput.GetSecondaryConduitOffset(FluidConduitType));
                }
            }

            return Grid.OffsetCell(TrashCansBuilding.NaturalBuildingCell(), SecondaryInputComponent[0].GetSecondaryConduitOffset(FluidConduitType));
        }
        
        private ConduitFlow GetConduitFlow()
        {
            switch (FluidConduitType)
            {
                case ConduitType.Gas:
                    return Game.Instance.gasConduitFlow;
                case ConduitType.Liquid:
                    return Game.Instance.liquidConduitFlow;
                default:
                case ConduitType.MAX:
                case ConduitType.None:
                case ConduitType.Solid:
                    return null;
            }
        }

        private void ConduitUpdate(float TickTime)
        {
            if (IsConnected == false) return;

            RefreshFilteredTag();

            if (FluidStorage.IsFull()) return;

            var FluidFlow = GetConduitFlow();
            var PipedFluidContents = FluidFlow.GetContents(InputCell);

            if (PipedFluidContents.mass <= 0.0) return;

            var OperationalStateRequirement = this.FindComponent<Operational>().MeetsRequirements(OperatingState);

            if (OperationalStateRequirement == false) return;

            var ContentMass = PipedFluidContents.mass;
            var ConsumptionMass = Mathf.Min(ContentMass.GetPercent(0.9), ConsumptionRate * TickTime);

            var PipedElements = ElementLoader.FindElementByHash(PipedFluidContents.element);

            if (PipedFluidContents.element != LastConsumedFluid)
                DiscoveredResources.Instance.Discover(PipedElements.tag, PipedElements.materialCategory);

            LastConsumedFluid = PipedFluidContents.element;

            var IsFilterThisFluid = FilteredTags.Contains(PipedElements.tag);


            if (FilteredTags.Count == 0 || (FilteredTags.Count != 0 && IsFilterThisFluid == true))
            {
                PipedFluidContents.ConsolidateMass();
                FluidFlow.RemoveElement(InputCell, ConsumptionMass);

                GetComponent<TrashCansMachineState>().PlayWorkable();
                Store(PipedFluidContents, PipedElements, ConsumptionMass);
            }
            else if (FilteredTags.Count != 0 && IsFilterThisFluid == false)
            {

                PipedFluidContents.ConsolidateMass();
                FluidFlow.RemoveElement(InputCell, ContentMass);

                Store(PipedFluidContents, PipedElements, ContentMass);

                var FluidGameObject = FluidStorage.FindFirst(PipedElements.tag);

                FluidStorage.Drop(FluidGameObject, false);
            }
        }

        private void Store(ConduitFlow.ConduitContents PipedFluidContents, Element PipedElements, float ConsumptionMass)
        {
            switch (FluidConduitType)
            {
                case ConduitType.Gas:
                    if (PipedElements.IsGas)
                    {
                        FluidStorage.AddGasChunk(
                            PipedFluidContents.element, ConsumptionMass, PipedFluidContents.temperature,
                            PipedFluidContents.diseaseIdx, 0, true, false
                        );

                        break;
                    }

                    PUtil.LogWarning($"Non-Gas Type On Gas Trash Cans: {PipedElements.id.ToString()}");
                    break;
                case ConduitType.Liquid:
                    if (PipedElements.IsLiquid)
                    {
                        FluidStorage.AddLiquid(
                            PipedFluidContents.element, ConsumptionMass, PipedFluidContents.temperature,
                            PipedFluidContents.diseaseIdx, 0, true, false
                        );

                        break;
                    }

                    PUtil.LogWarning($"Non-Liquid Type On Liquid Trash Cans: {PipedElements.id.ToString()}");
                    break;
                default:
                case ConduitType.MAX:
                case ConduitType.None:
                case ConduitType.Solid:
                    PUtil.LogWarning($"Non Serve This Type Of Material Send To Trash Cans: {PipedElements.id.ToString()}");
                    break;
            }
        }

        public void RefreshFilteredTag()
        {
            FilterTree = this.FindComponent<TreeFilterable>();

            var ChangedTags = FilterTree.GetTags();

            var ChangedFilteredTags = ChangedTags.Where(Tag => Tag != null).ToList();

            FilteredTags = ChangedFilteredTags;
        }

        private void CreateConduitConsumer(ConduitType FluidType, int SecondaryInputCell, out FlowUtilityNetwork.NetworkItem FluidNetworkItem)
        {
            var FluidConsumer = gameObject.AddComponent<ConduitConsumer>();

            FluidConsumer.conduitType = FluidType;
            FluidConsumer.useSecondaryInput = true;

            var FluidNetworkManager = Conduit.GetNetworkManager(FluidType);

            FluidNetworkItem = new FlowUtilityNetwork.NetworkItem(FluidType, Endpoint.Sink, SecondaryInputCell, gameObject);
            FluidNetworkManager.AddToNetworks(InputCell, FluidNetworkItem, true);
        }
    }
}
