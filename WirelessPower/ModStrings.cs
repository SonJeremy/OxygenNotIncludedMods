/* ReSharper disable
 *
 * UnusedType.Global
 * UnusedMember.Global
 * ConvertToConstant.Global
 * MemberCanBePrivate.Global
 * FieldCanBeMadeReadOnly.Global
 *
 * @TODO: This is Mod Strings, no above Inspection is required.!
 */

using SonJeremy.SModUtil.OxygenNotIncluded;

namespace SonJeremy.WirelessPower
{
    public static class ModStrings
    {
        public static string WirelessBatteryID = "WirelessBattery";
        public static LocString WirelessBatteryName = "Wireless Battery";
        public static LocString WirelessBatteryEffect = $"Store {STRINGS.UI.FormatAsLink("power", "POWER")} for later access on the Wireless Power Grid.{SUtil.NewLine}Slightly loses charge over time.";

        public static LocString WirelessBatteryDescription =
            "Wireless Battery may have one of operation state below:"
            + SUtil.NewLine +
            "<b>Standby:</b> <color=#ffff00ff>Indicated by yellow</color>, this state indicates the building is connected and awaiting power transfer."
            + SUtil.NewLine +
            "<b>Online:</b> <color=#5FDB37FF>Indicated by green</color>, this state indicates the building is connected and is actively sending or receiving power."
            + SUtil.NewLine +
            "<b>Offline:</b> <color=F44A47FF>Indicated by red</color>, this indicates the building is disconnected from the wireless grid (no wireless senders/receivers exist on the specified channel).";

        public static string WirelessReceiverID = "WirelessReceiver";
        public static LocString WirelessReceiverName = "Wireless Receiver";
        public static LocString WirelessReceiverEffect = $"Fetch {STRINGS.UI.FormatAsLink("power", "POWER")} from Wireless Batteries on the Wireless Power Grid.{SUtil.NewLine}Input the specified amount of {STRINGS.UI.FormatAsLink("power", "POWER")} into the connected circuit.";

        public static LocString WirelessReceiverDescription =
            "Wireless Receiver may have one of operation state below:"
            + SUtil.NewLine +
            "<b>Standby:</b> <color=#ffff00ff>Indicated by yellow</color>, this state indicates the building is connected and awaiting power transfer. This occurs when either all wireless batteries on the specified channel are empty or all batteries on the connected circuit are at their capacity thresholds."
            + SUtil.NewLine +
            "<b>Online:</b> <color=#5FDB37FF>Indicated by green</color>, this state indicates the building is connected and receiving power."
            + SUtil.NewLine +
            "<b>Offline:</b> <color=F44A47FF>Indicated by red</color>, this indicates the building is disconnected from the wireless grid (no wireless batteries exist on the specified channel).";

        public static string WirelessSenderID = "WirelessSender";
        public static LocString WirelessSenderName = "Wireless Sender";
        public static LocString WirelessSenderEffect = $"Send {STRINGS.UI.FormatAsLink("power", "POWER")} to wireless batteries on the same channel of the Wireless Power Grid.{SUtil.NewLine}{STRINGS.UI.FormatAsLink("Power", "POWER")} is sent from the current circuit in the amount specified.";

        public static LocString WirelessSenderDescription =
            "Wireless Sender may have one of operation state below:"
            + SUtil.NewLine +
            "<b>Standby:</b> <color=#ffff00ff>Indicated by yellow</color>, this state indicates the building is connected and awaiting power transfer (all connected batteries are full)."
            + SUtil.NewLine +
            "<b>Online:</b> <color=#5FDB37FF>Indicated by green</color>, this state indicates the building is connected and sending power."
            + SUtil.NewLine +
            "<b>Offline:</b> <color=#F44A47FF>Indicated by red</color>, this indicates the building is disconnected from the wireless grid (no wireless batteries exist on the specified channel).";
        
        public static class UI
        {
            public static class WIRELESS_POWER_OPTIONS
            {
                public static LocString MISC_CATEGORY = "Misc";
                public static LocString WIRELESS_BATTERY_CATEGORY = "Wireless Battery";
                public static LocString WIRELESS_SENDER_RECEIVER_CATEGORY = "Wireless Sender & Receiver";

                public static LocString USE_ENERGY_FALL_OFF = "Use Energy Fall-Off";
                public static LocString ENERGY_FALL_OFF_RATE = "Energy Fall-Off Rate";
                public static LocString USE_COLOR_STATUS_ITEM = "Coloring for Building Status Item";
                public static LocString MAX_NUMBER_OF_CHANNEL = "Total Number of Wireless Power Grid Channel";

                public static LocString MIN_TRANSMIT_POWER = "Minimum Transfer Power";
                public static LocString DEFAULT_TRANSMIT_POWER = "Default Transfer Power";

            }
        }
        
        public static class BUILDINGS
        {
            public static class PREFABS
            {
                public static class WIRELESSBATTERY
                {
                    public static LocString EFFECT = WirelessBatteryEffect;
                    public static LocString DESC = WirelessBatteryDescription;
                    public static LocString NAME = STRINGS.UI.FormatAsLink(WirelessBatteryName, WirelessBatteryID);
                }
                
                public static class WIRELESSSENDER
                {
                    public static LocString EFFECT = WirelessSenderEffect;
                    public static LocString DESC = WirelessSenderDescription;
                    public static LocString NAME = STRINGS.UI.FormatAsLink(WirelessSenderName, WirelessSenderID);
                }
                
                public static class WIRELESSRECEIVER
                {
                    public static LocString EFFECT = WirelessReceiverEffect;
                    public static LocString DESC = WirelessReceiverDescription;
                    public static LocString NAME = STRINGS.UI.FormatAsLink(WirelessReceiverName, WirelessReceiverID);
                }
            }
        }
    }
}