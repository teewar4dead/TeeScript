using Divine;
using Divine.Menu;
using Divine.Menu.Items;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        //public static MenuSwitcher PauseGame { get; set; }
        public static MenuSwitcher ShowRadiusRaze { get; set; }
        public static MenuSwitcher LinkaShow { get; set; }
        public static MenuSlider VectorX { get; set; }
        public static MenuSlider VectorY { get; set; }
        public static readonly Hero SFhero = EntityManager.LocalHero;
        public static Dictionary<AbilityId, bool> ListSpellsToggler = new Dictionary<AbilityId, bool>
            {
                { AbilityId.nevermore_shadowraze1,true},
                { AbilityId.nevermore_shadowraze2,true},
                { AbilityId.nevermore_shadowraze3,true},
            };
        public static Dictionary<AbilityId, bool> ListItemsToggler = new Dictionary<AbilityId, bool>
            {
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
            LinkenMenu.SetAbilityTexture(AbilityId.item_sphere);
            UltComboKey = RootMenu.CreateHoldKey("Ult Combo", Key.None);
            UltComboKey.SetAbilityTexture(AbilityId.nevermore_requiem);
            AutoRazeKey = RootMenu.CreateHoldKey("Auto Raze", Key.None);
            AutoRazeKey.SetAbilityTexture(AbilityId.nevermore_shadowraze1);
            AutoRazeMouseKey = RootMenu.CreateSwitcher("Auto Raze is mouse", true);
            AutoRazeMouseKey.SetAbilityTexture(AbilityId.nevermore_shadowraze1);
            //PauseGame = RootMenu.CreateSwitcher("Pause game at Killing", true);
            //PauseGame.SetAbilityTexture(AbilityId.item_cyclone);
            ShowRadiusRaze = RootMenu.CreateSwitcher("Show Raze Radius", true);
            ShowRadiusRaze.SetAbilityTexture(AbilityId.nevermore_shadowraze3);
            var panel = RootMenu.CreateMenu("Panel settings");
            VectorX = panel.CreateSlider("X", 0, 0, (int)RendererManager.ScreenSize.X);
            VectorY = panel.CreateSlider("Y", 0, 0, (int)RendererManager.ScreenSize.Y);
            new MainSF();
        }
    }
}
