/* ReSharper disable
 * 
 * UnusedType.Global
 * UnusedMember.Global
 * MemberCanBePrivate.Global
 * FieldCanBeMadeReadOnly.Global
 * 
 * @TODO: This is Mod Strings, no above Inspection is required.!
 */

namespace SonJeremy.TrashCans.Mods
{
    public static class ModStrings
    {
        public static LocString GasTrashCansID = "GasTrashCans";
        public static LocString GasTrashCansName = "Gas Trash Cans";
        public static LocString GasTrashCansEffect = "Store <link=\"ELEMENTSGAS\">Gas</link> items as normal Storage for auto or manual empty trash. If filter is set, wrong item will drop as a cans.";
        public static LocString GasTrashCansDescription = "When the air’s full of unwanted gases, your colony could be suffocating from the excess. Expel useless gases that don’t belong in your atmosphere. Breathe easy!";

        public static LocString SolidTrashCansID = "SolidTrashCans";
        public static LocString SolidTrashCansName = "Solid Trash Cans";
        public static LocString SolidTrashCansEffect = "Store <link=\"ELEMENTSSOLID\">Solid</link> items as normal Storage for auto or manual empty trash. If filter is set, wrong item will drop as a unpacked solid.";
        public static LocString SolidTrashCansDescription= "Rocks, dirt, and debris can accumulate fast.  Don't let a pile of junk weigh you down—store it away, and focus on building your colony's future!";

        public static LocString LiquidTrashCansID = "LiquidTrashCans";
        public static LocString LiquidTrashCansName = "Liquid Trash Cans";
        public static LocString LiquidTrashCansEffect = "Store <link=\"ELEMENTSLIQUID\">Liquid</link> items as normal Storage for auto or manual empty trash. If filter is set, wrong item will drop as a cans.";
        public static LocString LiquidTrashCansDescription = "Out with the old, in with the new. Liquids that no longer serve a purpose have a place to go! Send excess liquids to oblivion! No more unwanted puddles cluttering your base.!";
        
        public static LocString ArtifactTrashCansID = "ArtifactTrashCans";
        public static LocString ArtifactTrashCansName = "Artifact Trash Cans";
        public static LocString ArtifactTrashCansEffect = "Store Artifact items in Special Storage for auto or manual empty trash.";
        public static LocString ArtifactTrashCansDescription = "We know, artifacts can be valuable... but sometimes they just take up too much room. Say goodbye to relics gathering dust!";


        public static class BUILDINGS
        {
            public static class PREFABS
            {
                public static class GASTRASHCANS
                {
                    public static LocString EFFECT = GasTrashCansEffect;
                    public static LocString DESC = GasTrashCansDescription;
                    public static LocString NAME = STRINGS.UI.FormatAsLink(GasTrashCansName, GasTrashCansID);
                }

                public static class SOLIDTRASHCANS
                {
                    public static LocString EFFECT = SolidTrashCansEffect;
                    public static LocString DESC = SolidTrashCansDescription;
                    public static LocString NAME = STRINGS.UI.FormatAsLink(SolidTrashCansName, SolidTrashCansID);
                }

                public static class LIQUIDTRASHCANS
                {
                    public static LocString EFFECT = LiquidTrashCansEffect;
                    public static LocString DESC = LiquidTrashCansDescription;
                    public static LocString NAME = STRINGS.UI.FormatAsLink(LiquidTrashCansName, LiquidTrashCansID);
                }

                public static class ARTIFACTTRASHCANS
                {
                    public static LocString EFFECT = ArtifactTrashCansEffect;
                    public static LocString DESC = ArtifactTrashCansDescription;
                    public static LocString NAME = STRINGS.UI.FormatAsLink(ArtifactTrashCansName, ArtifactTrashCansID);
                }
            }
        }

        public static class BUILDING
        {
            public static class STATUSITEMS
            {
                public static class GASTRASHCANS
                {
                    public static class AUTO_TRASH
                    {
                        public static LocString TOOLTIP = "Trash Cans will {TRASHCANS_STATUS_TOOLTIP}";
                        public static LocString NAME = "Auto Trash: <b>{TRASHCANS_AUTO_TRASH_STATUS}</b>";
                    }

                    public static class FILTER_STATE
                    {
                        public static LocString TOOLTIP = "{TRASHCANS_FILTER_STATE_TOOLTIP}";
                        public static LocString NAME = "Filter State: <b>{TRASHCANS_FILTER_STATE_STATUS}</b>";
                    }
                }
                
                public static class SOLIDTRASHCANS
                {
                    public static class AUTO_TRASH
                    {
                        public static LocString TOOLTIP = "Trash Cans will {TRASHCANS_STATUS_TOOLTIP}";
                        public static LocString NAME = "Auto Trash: <b>{TRASHCANS_AUTO_TRASH_STATUS}</b>";
                    }

                    public static class FILTER_STATE
                    {
                        public static LocString TOOLTIP = "{TRASHCANS_FILTER_STATE_TOOLTIP}";
                        public static LocString NAME = "Filter State: <b>{TRASHCANS_FILTER_STATE_STATUS}</b>";
                    }
                }

                public static class LIQUIDTRASHCANS
                {
                    public static class AUTO_TRASH
                    {
                        public static LocString TOOLTIP = "Trash Cans will {TRASHCANS_STATUS_TOOLTIP}";
                        public static LocString NAME = "Auto Trash: <b>{TRASHCANS_AUTO_TRASH_STATUS}</b>";
                    }

                    public static class FILTER_STATE
                    {
                        public static LocString TOOLTIP = "{TRASHCANS_FILTER_STATE_TOOLTIP}";
                        public static LocString NAME = "Filter State: <b>{TRASHCANS_FILTER_STATE_STATUS}</b>";
                    }
                }

                public static class ARTIFACTTRASHCANS
                {
                    public static class AUTO_TRASH
                    {
                        public static LocString TOOLTIP = "Trash Cans will {TRASHCANS_STATUS_TOOLTIP}";
                        public static LocString NAME = "Auto Trash: <b>{TRASHCANS_AUTO_TRASH_STATUS}</b>";
                    }
                }
            }
        }

        public static class UI
        {
            public static class TRASH_CANS
            {
                public static LocString SOLID_STORAGE_UNIT = "Kg";

                public static LocString TRASHCANS_STATUS_TOOLTIP_DISABLED = "<b>NOT</b> auto empty trash.!";
                public static LocString TRASHCANS_STATUS_TOOLTIP_ENABLED = "auto empty in <b>{NEXT_AUTO_TRASH_SECOND}s</b> from an total of <b>{TOTAL_WAIT_TIME}s</b>.";

                public static LocString TRASHCANS_AUTO_TRASH_STATUS_ENABLED = "<b>Enabled ~ {NEXT_AUTO_TRASH_SECOND}s</b>";
                public static LocString TRASHCANS_AUTO_TRASH_STATUS_DISABLED = "<b>Disabled</b>";

                public static LocString TRASHCANS_AUTO_DELIVERY_CONNECTED = "<b>Connected<b>";
                public static LocString TRASHCANS_AUTO_DELIVERY_NOT_CONNECTED = "<b>Enabled - NOT Connected<b>";
                public static LocString TRASHCANS_AUTO_DELIVERY_TOOLTIP_NOT_CONNECTED = "Not connected with any Conveyor Rail or Loader.";
                public static LocString TRASHCANS_AUTO_DELIVERY_TOOLTIP_ENABLED = "When auto delivery enabled. This Trash Cans will accept all materials send by Conveyor Rail.";

                public static LocString TRASHCANS_FILTER_STATE_SET = "<b>Set</b>";
                public static LocString TRASHCANS_FILTER_STATE_NOT_SET = "<b>Not Set</b>";

                public static LocString GAS_TRASHCANS_FILTER_STATE_TOOLTIP_DISABLED = "Filter is not set. Trash Cans will <b>Store All Type Of <link=\"ELEMENTSGAS\">Gases</link></b>.";
                public static LocString LIQUID_TRASHCANS_FILTER_STATE_TOOLTIP_DISABLED = "Filter is not set. Trash Cans will <b>Store All Type Of <link=\"ELEMENTSLIQUID\">Liquids</link></b>.";
                public static LocString SOLID_TRASHCANS_FILTER_STATE_TOOLTIP_DISABLED = "Filter is not set. Trash Cans will <b>Store All Type Of <link=\"ELEMENTSSOLID\">Solid Materials</link></b>.";

                public static LocString GAS_TRASHCANS_FILTER_STATE_TOOLTIP_ENABLED = "Matching element are stored, while others are dropped as a can.";
                public static LocString LIQUID_TRASHCANS_FILTER_STATE_TOOLTIP_ENABLED = "Matching element are stored, while others are dropped as a can.";
                public static LocString SOLID_TRASHCANS_FILTER_STATE_TOOLTIP_ENABLED = "Matching element are stored, while others are dropped as unloaded.";
            }

            public static class TRASH_CANS_OPTIONS
            {
                public static LocString GAS_CATEGORY = "Gas Trash Cans Options";
                public static LocString SOLID_CATEGORY = "Solid Trash Cans Options";
                public static LocString LIQUID_CATEGORY = "Liquid Trash Cans Options";
                public static LocString ARTIFACT_CATEGORY = "Artifact Trash Cans Options";


                public static LocString CAN_FLOOD = "Can Flood";
                public static LocString CAPACITY_KG = "Capacity KG";
                public static LocString CAN_OVER_HEAT = "Can Over Heat";
                public static LocString ENABLE_AUTO_TRASH = "Enable Auto Trash";
                public static LocString REQUIRE_POWER = "Require Power To Operate";
                public static LocString MAX_AUTO_TRASH_INTERVAL = "Max Auto Trash Interval";
                public static LocString ENABLE_AUTO_DELIVERY = "Enable Auto Delivery With Conveyor Rail";
                public static LocString ENERGY_CONSUMPTION_WHEN_ACTIVE = "Energy Consumption When Active";
                public static LocString ENABLE_AUTO_DELIVERY_TOOLTIP = "Using Conveyor Rail for delivery. Duplicants work will not be affected.";
            }

            public static class TRASH_CANS_SIDE_SCREEN
            {
                public static class DROP_ITEMS_BUTTON
                {
                    public static LocString TITLE = "Drop Contents";
                    public static LocString TOOLTIP = "Drop All its contents when touch.!";
                }

                public static class EMPTY_TRASH_BUTTON
                {
                    public static LocString TEXT = "Empty Trash";
                    public static LocString TOOLTIP = "Empty your trash, tidy your colony.!";
                }

                public static class AUTO_TRASH_CHECKBOX
                {
                    public static LocString LABEL = "Enable Auto Trash";
                    public static LocString TOOLTIP = "Will automatically empty after a given period of time.!";
                }

                public static class AUTO_TRASH_SLIDER
                {
                    public static LocString TITLE = "";
                    public static LocString UNIT = " s";
                    public static LocString TOOLTIPKEY = "";
                    public static LocString TOOLTIP = "Make your Decision how each auto trash period.!";
                }
            }
        }

        public static class MISC
        {
            public static class TAGS
            {
                public static LocString MISCPICKUPABLE = "Pickupable";
            }
        }
    }
}
