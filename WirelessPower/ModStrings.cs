/* ReSharper disable
 *
 * UnusedType.Global
 * UnusedMember.Global
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
        public static string WirelessBatteryName = "Wireless Battery";
        public static string WirelessBatteryID = "WirelessBattery";

        public static string WirelessBatteryEffect =
            $"Store {STRINGS.UI.FormatAsLink("power", "POWER")} for later access on the Wireless Power Grid."
            + SUtil.NewLine +
            $"Slightly loses charge over time.";

        public static string WirelessBatteryDescription =
            "Wireless Battery may have one of operation state below:"
            + SUtil.NewLine +
            "<b>Standby:</b> <color=#ffff00ff>Indicated by yellow</color>, this state indicates the building is connected and awaiting power transfer."
            + SUtil.NewLine +
            "<b>Online:</b> <color=#5FDB37FF>Indicated by green</color>, this state indicates the building is connected and is actively sending or receiving power."
            + SUtil.NewLine +
            "<b>Offline:</b> <color=F44A47FF>Indicated by red</color>, this indicates the building is disconnected from the wireless grid (no wireless senders/receivers exist on the specified channel).";

        public static string WirelessReceiverName = "Wireless Receiver";
        public static string WirelessReceiverID = "WirelessReceiver";

        public static string WirelessReceiverEffect =
            $"Fetch {STRINGS.UI.FormatAsLink("power", "POWER")} from Wireless Batteries on the Wireless Power Grid."
            + SUtil.NewLine +
            $"Input the specified amount of {STRINGS.UI.FormatAsLink("power", "POWER")} into the connected circuit.";

        public static string WirelessReceiverDescription =
            "Wireless Receiver may have one of operation state below:"
            + SUtil.NewLine +
            "<b>Standby:</b> <color=#ffff00ff>Indicated by yellow</color>, this state indicates the building is connected and awaiting power transfer. This occurs when either all wireless batteries on the specified channel are empty or all batteries on the connected circuit are at their capacity thresholds."
            + SUtil.NewLine +
            "<b>Online:</b> <color=#5FDB37FF>Indicated by green</color>, this state indicates the building is connected and receiving power."
            + SUtil.NewLine +
            "<b>Offline:</b> <color=F44A47FF>Indicated by red</color>, this indicates the building is disconnected from the wireless grid (no wireless batteries exist on the specified channel).";

        public static string WirelessSenderName = "Wireless Sender";
        public static string WirelessSenderID = "WirelessSender";

        public static string WirelessSenderEffect =
            $"Send {STRINGS.UI.FormatAsLink("power", "POWER")} to wireless batteries on the same channel of the Wireless Power Grid."
            + SUtil.NewLine +
            $"{STRINGS.UI.FormatAsLink("power", "POWER")} is sent from the current circuit in the amount specified.";

        public static string WirelessSenderDescription =
            "Wireless Sender may have one of operation state below:"
            + SUtil.NewLine +
            "<b>Standby:</b> <color=#ffff00ff>Indicated by yellow</color>, this state indicates the building is connected and awaiting power transfer (all connected batteries are full)."
            + SUtil.NewLine +
            "<b>Online:</b> <color=#5FDB37FF>Indicated by green</color>, this state indicates the building is connected and sending power."
            + SUtil.NewLine +
            "<b>Offline:</b> <color=F44A47FF>Indicated by red</color>, this indicates the building is disconnected from the wireless grid (no wireless batteries exist on the specified channel).";
        
        public static class UI
        {
            public static class WIRELESS_POWER_OPTIONS
            {
                
            }
        }
    }
}