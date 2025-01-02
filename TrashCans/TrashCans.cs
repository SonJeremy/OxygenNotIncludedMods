using HarmonyLib;
using UnityEngine;
using KSerialization;
using SonJeremy.TrashCans.Mods;
using SonJeremy.TrashCans.SideScreenUI;
using SonJeremy.TrashCans.MachineState;
using SonJeremy.TrashCans.AutoConsumption;

namespace SonJeremy.TrashCans
{
    [SerializationConfig(MemberSerialization.OptIn)]

    public class TrashCans : KMonoBehaviour, ISidescreenButtonControl, ISim1000ms, ISim200ms
    {
        public float WaitTime => SerializeWaitTime;
        public bool AutoTrash => SerializeAutoTrash;
        private float CurrentTime => SerializeCurrentTime;
        
        #pragma warning disable CS0649
        [MyCmpGet] private Storage BaseStorage;
        [MyCmpGet] private Operational BaseOperational;
        [MyCmpGet] private AutoSolidTrashCans SolidAutoTrashCans;
        [MyCmpGet] private AutoFluidTrashCans FluidAutoTrashCans;
        [MyCmpGet] private BuildingEnabledButton BuildingStateButton;
        #pragma warning restore CS0649

        [Serialize] private float SerializeWaitTime;
        [Serialize] private bool SerializeAutoTrash;
        [Serialize] private float SerializeCurrentTime;

        
        public string SidescreenButtonText => GetSideScreenButtonText();

        public string SidescreenButtonTooltip => GetSideScreenButtonTooltip();

        public int HorizontalGroupID() => -1;

        public bool SidescreenEnabled() => true;

        public int ButtonSideScreenSortOrder() => 0;

        public bool SidescreenButtonInteractable() => true;

        public void SetButtonTextOverride(ButtonMenuTextOverride ButtonText) {}

        private string TrashCansType => GetComponent<Building>().Def.PrefabID.ToUpper();
        
        
        protected override void OnSpawn()
        {
            base.OnSpawn();

            Subscribe((int) GameHashes.RefreshUserMenu, OnRefresh);

            var AutoTrashStatusType = $"{TrashCansType}.AUTO_TRASH";
            var FilterStateStatusType = $"{TrashCansType}.FILTER_STATE";

            var AutoTrashStatus = new StatusItem(AutoTrashStatusType, "BUILDING", string.Empty,
                StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID)
            {
                resolveStringCallback = (StringInfo, GameObject) =>
                {
                    if (StringInfo == null) return null;
                    if (GameObject == null) return StringInfo;

                    if (StringInfo.Contains("{TRASHCANS_AUTO_TRASH_STATUS}") == true)
                    {
                        if (AutoTrash == true)
                        {
                            var RemainingTime = Mathf.RoundToInt(WaitTime - CurrentTime).ToString();
                            string AutoTrashEnabledStatus = ModStrings.UI.TRASH_CANS.TRASHCANS_AUTO_TRASH_STATUS_ENABLED;
                            
                            AutoTrashEnabledStatus = AutoTrashEnabledStatus.Replace("{NEXT_AUTO_TRASH_SECOND}", RemainingTime);

                            StringInfo = StringInfo.Replace("{TRASHCANS_AUTO_TRASH_STATUS}", AutoTrashEnabledStatus);
                        }
                        else
                            StringInfo = StringInfo.Replace("{TRASHCANS_AUTO_TRASH_STATUS}",
                                ModStrings.UI.TRASH_CANS.TRASHCANS_AUTO_TRASH_STATUS_DISABLED);
                    }

                    if (StringInfo.Contains("{TRASHCANS_STATUS_TOOLTIP}") == true)
                    {
                        if (AutoTrash == true)
                        {
                            var RemainingTime = Mathf.RoundToInt(WaitTime - CurrentTime).ToString();
                            string AutoTrashEnabledTooltip = ModStrings.UI.TRASH_CANS.TRASHCANS_STATUS_TOOLTIP_ENABLED;
                            
                            AutoTrashEnabledTooltip = AutoTrashEnabledTooltip.Replace("{TOTAL_WAIT_TIME}", WaitTime.ToString());
                            AutoTrashEnabledTooltip = AutoTrashEnabledTooltip.Replace("{NEXT_AUTO_TRASH_SECOND}", RemainingTime);

                            StringInfo = StringInfo.Replace("{TRASHCANS_STATUS_TOOLTIP}", AutoTrashEnabledTooltip);
                        }
                        else
                            StringInfo = StringInfo.Replace("{TRASHCANS_STATUS_TOOLTIP}",
                                ModStrings.UI.TRASH_CANS.TRASHCANS_STATUS_TOOLTIP_DISABLED);
                    }

                    return StringInfo;
                }
            };
            
            FindOrAdd<TrashCansSideScreen>();

            GetComponent<KSelectable>().AddStatusItem(AutoTrashStatus, this);

            if (TrashCansType == "ARTIFACTTRASHCANS") return;

            var FilterStateStatus = new StatusItem(FilterStateStatusType, "BUILDING", string.Empty,
                StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID)
            {
                resolveStringCallback = (StringInfo, GameObject) =>
                {
                    if (GameObject == null) return StringInfo;

                    if (StringInfo.Contains("{TRASHCANS_FILTER_STATE_STATUS}"))
                    {
                        bool FilterState;

                        switch (TrashCansType)
                        {
                            case "GASTRASHCANS":
                                FilterState = FluidAutoTrashCans.IsFiltered;
                                break;
                            case "SOLIDTRASHCANS":
                                FilterState = SolidAutoTrashCans.IsFiltered;
                                break;
                            case "LIQUIDTRASHCANS":
                                FilterState = FluidAutoTrashCans.IsFiltered;
                                break;
                            default:
                                FilterState = false;
                                break;
                        }

                        StringInfo = StringInfo.Replace("{TRASHCANS_FILTER_STATE_STATUS}", FilterState == true 
                            ? ModStrings.UI.TRASH_CANS.TRASHCANS_FILTER_STATE_SET 
                            : ModStrings.UI.TRASH_CANS.TRASHCANS_FILTER_STATE_NOT_SET);
                    }

                    if (StringInfo.Contains("{TRASHCANS_FILTER_STATE_TOOLTIP}"))
                    {
                        bool FilterState;
                        string ToolTipEnabled;
                        string ToolTipDisabled;

                        switch (TrashCansType)
                        {
                            case "GASTRASHCANS":
                                FilterState = FluidAutoTrashCans.IsFiltered;
                                ToolTipEnabled = ModStrings.UI.TRASH_CANS.GAS_TRASHCANS_FILTER_STATE_TOOLTIP_ENABLED;
                                ToolTipDisabled = ModStrings.UI.TRASH_CANS.GAS_TRASHCANS_FILTER_STATE_TOOLTIP_DISABLED;
                                break;
                            case "SOLIDTRASHCANS":
                                FilterState = SolidAutoTrashCans.IsFiltered;
                                ToolTipEnabled = ModStrings.UI.TRASH_CANS.SOLID_TRASHCANS_FILTER_STATE_TOOLTIP_ENABLED;
                                ToolTipDisabled = ModStrings.UI.TRASH_CANS
                                    .SOLID_TRASHCANS_FILTER_STATE_TOOLTIP_DISABLED;
                                break;
                            case "LIQUIDTRASHCANS":
                                FilterState = FluidAutoTrashCans.IsFiltered;
                                ToolTipEnabled = ModStrings.UI.TRASH_CANS.LIQUID_TRASHCANS_FILTER_STATE_TOOLTIP_ENABLED;
                                ToolTipDisabled = ModStrings.UI.TRASH_CANS
                                    .LIQUID_TRASHCANS_FILTER_STATE_TOOLTIP_DISABLED;
                                break;
                            default:
                                FilterState = false;
                                ToolTipEnabled = null;
                                ToolTipDisabled = null;
                                break;
                        }

                        StringInfo = FilterState == true
                            ? StringInfo.Replace("{TRASHCANS_FILTER_STATE_TOOLTIP}",
                                ToolTipEnabled ?? "Not Type Of Trash Cans")
                            : StringInfo.Replace("{TRASHCANS_FILTER_STATE_TOOLTIP}",
                                ToolTipDisabled ?? "Not Type Of Trash Cans");
                    }

                    return StringInfo;
                }
            };

            GetComponent<KSelectable>().AddStatusItem(FilterStateStatus, this);
        }

        public void OnRefresh(object EventObject)
        {
            var DropItemButton = new KIconButtonMenu.ButtonInfo(
                "action_empty_contents",
                GetUserButtonText(),
                DropItems,
                Action.NumActions,
                null,
                null,
                null,
                GetUserButtonTooltip()
            );

            Game.Instance.userMenu.AddButton(gameObject, DropItemButton);
        }

        public void EmptyTrash()
        {
            if (BaseStorage.IsEmpty() == true) return;
            if (BaseOperational.IsOperational == false) return;

            this.FindComponent<TrashCansMachineState>().PlayWorkable();
            Traverse.Create(this.FindComponent<Storage>()).Method("ClearItems").GetValue();
        }

        public void OnSidescreenButtonPressed() => EmptyTrash();

        public void UpdateAutoTrashStatus(bool AutoStatus)
        {
            if (AutoStatus == SerializeAutoTrash) return;

            SerializeCurrentTime = 0;
            SerializeAutoTrash = AutoStatus;
        }

        public void UpdateAutoTrashTime(int UpdateWaitTime)
        {
            var CurrentSerializeWaitTime = Mathf.FloorToInt(SerializeWaitTime);
            
            if (UpdateWaitTime == CurrentSerializeWaitTime || UpdateWaitTime < 30)
                return;
            
            SerializeCurrentTime = 0;
            SerializeWaitTime = UpdateWaitTime;
        }

        public void UpdateWaitTime(float SimSecond) => SerializeCurrentTime += SimSecond;

        public void ResetWaitTime() => SerializeCurrentTime = 0;

        public void Sim1000ms(float Second)
        {
            if (AutoTrash == false || BaseOperational.IsOperational == false) return;

            UpdateWaitTime(Second);

            if (CurrentTime < WaitTime) return;
            
            ResetWaitTime();
            EmptyTrash();
        }

        public void Sim200ms(float TickTime)
        { 
            /* Primitive state condition. Building is disabled. */
            if (BuildingStateButton.IsEnabled == false)
            {
                BaseOperational.SetActive(false);
                return;
            }

            if (BaseStorage.IsFull() == true && AutoTrash == false) 
                BaseOperational.SetActive(false);

            if (BaseStorage.IsFull() == false && BaseOperational.IsFunctional == true && BaseOperational.IsOperational == true)
                BaseOperational.SetActive(true);

            /* Must Be At The End Of Check Cycles.
             * We will assume that when Auto-Trash is enabled
             * This building must be always consume energy for checking things up.
             */
            if (AutoTrash == true)
                BaseOperational.SetActive(true);
        }

        private void DropItems()
        {
            Storage TrashCansStorage = this.FindComponent<Storage>();

            if (TrashCansStorage != null && TrashCansStorage.MassStored() > 0)
            {
                if (GetComponent<Operational>().IsOperational == true) 
                    this.FindComponent<TrashCansMachineState>().PlayWorkable();

                TrashCansStorage.DropAll();
            }
        }

        private string GetSideScreenButtonText()
        {
            switch (TrashCansType)
            {
                default:
                    return ModStrings.UI.TRASH_CANS_SIDE_SCREEN.EMPTY_TRASH_BUTTON.TEXT;
            }
        }

        private string GetSideScreenButtonTooltip()
        {
            switch (TrashCansType)
            {
                default:
                    return ModStrings.UI.TRASH_CANS_SIDE_SCREEN.EMPTY_TRASH_BUTTON.TOOLTIP;
            }
        }

        private string GetUserButtonText()
        {
            switch (TrashCansType)
            {
                default:
                    return ModStrings.UI.TRASH_CANS_SIDE_SCREEN.DROP_ITEMS_BUTTON.TITLE;
            }
        }

        private string GetUserButtonTooltip()
        {
            switch (TrashCansType)
            {
                default:
                    return ModStrings.UI.TRASH_CANS_SIDE_SCREEN.DROP_ITEMS_BUTTON.TOOLTIP;
            }
        }
    }
}
