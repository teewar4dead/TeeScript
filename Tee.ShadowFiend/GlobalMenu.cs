using System.Collections.Generic;
using System.Windows.Input;

using Divine.Entity;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Menu;
using Divine.Menu.Items;
using Divine.Numerics;

namespace Tee.ShadowFiend
{
    class GlobalMenu
    {
        public static MenuSwitcher OnOff { get; set; }
        public static MenuHoldKey MainCombo { get; set; }
        public static MenuSwitcher HitRun { get; set; }
        public static MenuHoldKey UltComboKey { get; set; }
        public static MenuHoldKey AutoRazeKey { get; set; }
        public static MenuSwitcher AutoRazeMouseKey { get; set; }
        public static MenuSwitcher PauseGame { get; set; }
        public static MenuSwitcher UltGameChat { get; set; }
        public static MenuSwitcher ShowRadiusRaze { get; set; }
        public static MenuSwitcher LinkaShow { get; set; }
        public static readonly Hero SFhero = EntityManager.LocalHero;
        public static Dictionary<AbilityId, bool> ListSpellsToggler = new Dictionary<AbilityId, bool>
            {
                { AbilityId.nevermore_shadowraze1,true},
                { AbilityId.nevermore_shadowraze2,true},
                { AbilityId.nevermore_shadowraze3,true},
            };
        public static Dictionary<AbilityId, bool> ListItemsToggler = new Dictionary<AbilityId, bool>
            {
                { AbilityId.item_black_king_bar,true},
                { AbilityId.item_phase_boots,true},
                { AbilityId.item_orchid,true},
                { AbilityId.item_bloodthorn,true},
                { AbilityId.item_silver_edge,true},
                { AbilityId.item_gungir,true},
                { AbilityId.item_mjollnir,true},
                { AbilityId.item_nullifier,true},
                { AbilityId.item_shivas_guard,true},
                { AbilityId.item_ethereal_blade,true},
                { AbilityId.item_eternal_shroud,true},
                { AbilityId.item_satanic,true},
                { AbilityId.item_sheepstick,true},
                { AbilityId.item_manta,true},
                { AbilityId.item_diffusal_blade,true},
                { AbilityId.item_essence_ring,true},
                { AbilityId.item_arcane_ring,true},
                { AbilityId.item_bullwhip,true},
                { AbilityId.item_spider_legs,true}
            };
        public static Dictionary<AbilityId, bool> ListLinkenToggler = new Dictionary<AbilityId, bool>
        {
            { AbilityId.item_cyclone,true},
            { AbilityId.item_orchid,true},
            { AbilityId.item_bloodthorn,true},
            { AbilityId.item_ethereal_blade,true},
            { AbilityId.item_abyssal_blade,true},
            { AbilityId.item_sheepstick,true},
            { AbilityId.item_nullifier,true}
        };
        public GlobalMenu()
        {
            
            var RootMenu = MenuManager.CreateRootMenu("Tee.ShadowFiend");
            OnOff = RootMenu.CreateSwitcher("Enable", true);
            RootMenu.SetFontColor(Color.Indigo);
            MainCombo = RootMenu.CreateHoldKey("Combo", Key.None);
            HitRun = RootMenu.CreateSwitcher("Hit Run", true);

            RootMenu.CreateAbilityToggler("Spells", ListSpellsToggler);

            RootMenu.CreateItemToggler("Items", ListItemsToggler);
            var LinkenMenu = RootMenu.CreateMenu("Cast to linken's sphere");
            LinkaShow = LinkenMenu.CreateSwitcher("Show Linkens Sphere", true);
            LinkenMenu.CreateSpellToggler("", ListLinkenToggler);
            LinkenMenu.SetAbilityImage(AbilityId.item_sphere);
            UltComboKey = RootMenu.CreateHoldKey("Ult Combo", Key.None);
            UltComboKey.SetAbilityImage(AbilityId.item_cyclone);
            AutoRazeKey = RootMenu.CreateHoldKey("Auto Raze", Key.None);
            AutoRazeKey.SetAbilityImage(AbilityId.nevermore_shadowraze1);
            AutoRazeMouseKey = RootMenu.CreateSwitcher("Auto Raze is mouse", true);
            AutoRazeMouseKey.SetAbilityImage(AbilityId.nevermore_shadowraze1);
            PauseGame = RootMenu.CreateSwitcher("Pause game at Killing", true);
            PauseGame.SetAbilityImage(AbilityId.item_cyclone);
            UltGameChat = RootMenu.CreateSwitcher("Send to chat \"?\"", true);
            UltGameChat.SetAbilityImage(AbilityId.nevermore_requiem);
            ShowRadiusRaze = RootMenu.CreateSwitcher("Show Raze Radius", true);
            ShowRadiusRaze.SetAbilityImage(AbilityId.nevermore_shadowraze3);
            new MainSF();
        }
    }
}
