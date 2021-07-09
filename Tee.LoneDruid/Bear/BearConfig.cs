using System.Collections.Generic;
using System.Windows.Input;

using Divine.Entity.Entities.Abilities.Components;
using Divine.Menu.Items;

namespace TeeLoneDruid
{
    internal sealed class BearConfig
    {
        public static MenuItemToggler AbilityToggler { get; set; }

        public static MenuSwitcher SummonBear { get; set; }

        public static MenuHoldKey BearCombo { get; set; }

        public static MenuSwitcher HitRun { get; set; }

        public static MenuSwitcher AutoCombo { get; set; }

        public static MenuSwitcher VBE { get; set; }

        public static Dictionary<AbilityId, bool> ListSpell = new()
        {
            { AbilityId.item_phase_boots, true },
            { AbilityId.item_spider_legs, true },
            { AbilityId.item_bullwhip, true },
            { AbilityId.item_hand_of_midas, true },
            { AbilityId.item_orchid, true },
            { AbilityId.item_bloodthorn, true },
            { AbilityId.item_mjollnir, true },
            { AbilityId.item_mask_of_madness, true },
            { AbilityId.item_abyssal_blade, true },
            { AbilityId.item_nullifier, true },
            { AbilityId.item_medallion_of_courage, true },
            { AbilityId.item_solar_crest, true },
            { AbilityId.item_satanic, true }

        };

        public BearConfig(Menu RootMenu)
        {

            SummonBear = RootMenu.CreateSwitcher("Summon the bear at the beginning of the game");
            BearCombo = RootMenu.CreateHoldKey("ComboBear", Key.None);
            HitRun = RootMenu.CreateSwitcher("Use OrbWalker", true);
            AutoCombo = RootMenu.CreateSwitcher("Auto Combo", true);
            VBE = RootMenu.CreateSwitcher("Visible by enemy", true);
            AbilityToggler = RootMenu.CreateItemToggler("Auto item", ListSpell);

        }

    }
}