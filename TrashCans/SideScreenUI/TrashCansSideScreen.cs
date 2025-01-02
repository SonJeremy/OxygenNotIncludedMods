using UnityEngine;
using KSerialization;

using static SonJeremy.TrashCans.Mods.ModStrings.UI.TRASH_CANS_SIDE_SCREEN;

namespace SonJeremy.TrashCans.SideScreenUI
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class TrashCansSideScreen : SideScreen, ICheckboxControl, IIntSliderControl
    {
        private TrashCans TrashCans;

        public string CheckboxLabel => GetCheckboxLabel();
        public string CheckboxTooltip => GetCheckboxTooltip();

        public string SliderTitleKey => GetSliderTitleKey();
        public string CheckboxTitleKey => GetCheckboxTitleKey();
        
        public string GetSliderTooltipKey(int IndexKey) => GetSliderTooltipKey();

        public string SliderUnits => GetSliderUnitText();

        protected override void OnSpawn()
        {
            base.OnSpawn();

            TrashCans = this.FindComponent<TrashCans>();
        }

        public bool GetCheckboxValue() => TrashCans != null && TrashCans.AutoTrash;

        public void SetCheckboxValue(bool CheckboxStatus)
        {
            if (TrashCans == null) return;
            TrashCans.UpdateAutoTrashStatus(CheckboxStatus);
        }

        public int SliderDecimalPlaces(int IndexKey) => 0;

        public float GetSliderMin(int IndexKey) => 30f;

        public float GetSliderMax(int IndexKey) => 4200f;

        public float GetSliderValue(int IndexKey) => TrashCans == null ? 60f : TrashCans.WaitTime;

        public void SetSliderValue(float WaitTime, int IndexKey)
        {
            if (TrashCans == null) return;

            TrashCans.UpdateAutoTrashTime(Mathf.RoundToInt(WaitTime));
        }

        public string GetSliderTooltip(int IndexKey) => "";

        private string GetCheckboxLabel()
        {
            return AUTO_TRASH_CHECKBOX.LABEL;
        }

        private string GetCheckboxTooltip()
        {
            return AUTO_TRASH_CHECKBOX.TOOLTIP;
        }

        private LocString GetCheckboxTitleKey()
        {
            return (LocString) nameof(AUTO_TRASH_CHECKBOX);
        }

        private string GetSliderTitleKey()
        {
            return AUTO_TRASH_SLIDER.TITLE;
        }

        private string GetSliderTooltipKey()
        {
            return AUTO_TRASH_SLIDER.TOOLTIPKEY;
        }

        private string GetSliderUnitText()
        {
            return AUTO_TRASH_SLIDER.UNIT;
        }
    }
}
