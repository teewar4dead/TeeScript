using System.Collections.Generic;
using System.Reflection;

using Divine.Menu;
using Divine.Menu.Items;
using Divine.Renderer;

namespace Tee.Snatcher
{
    internal class GlobalMenu
    {
        public static Dictionary<string, bool> ListRuneToggler = new Dictionary<string, bool>
            {
                { "Bounty", true },
                { "Aegis", true },
                { "Cheese", true },
                { "Refresher", true },
                { "Aghanim", true },
                { "Gem", true },
                { "Rapier", true },
                { "Neutrals", true }
            };
        public static MenuSwitcher OnOff { get; set; }
        public static MenuToggler PicUp { get; set; }
        public GlobalMenu()
        {
            var testImage = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Tee.Snatcher.Bounty_Rune_minimap_icon.png");

            RendererManager.LoadImage("Bounty", testImage);
            RendererManager.LoadImage("Aegis", "panorama/images/items/aegis_png.vtex_c");
            RendererManager.LoadImage("Cheese", "panorama/images/items/cheese_png.vtex_c");
            RendererManager.LoadImage("Refresher", "panorama/images/items/refresher_shard_png.vtex_c");
            RendererManager.LoadImage("Aghanim", "panorama/images/compendium/international2020/aghanim_icon_psd.vtex_c");
            RendererManager.LoadImage("Gem", "panorama/images/items/gem_png.vtex_c");
            RendererManager.LoadImage("Rapier", "panorama/images/items/rapier_png.vtex_c");
            RendererManager.LoadImage("Neutrals", "panorama/images/items/poor_mans_shield_png.vtex_c");




            var RootMenu = MenuManager.CreateRootMenu("Tee.Snatcher");
            OnOff = RootMenu.CreateSwitcher("Enable", true);

            PicUp = RootMenu.CreateToggler("PickUp", ListRuneToggler);
            new Snatcher();
        }
    }
}