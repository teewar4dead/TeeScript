using Divine.Menu;
using Divine.Menu.Items;

namespace Tee.Snatcher
{
    internal class GlobalMenu
    {
        public static MenuSwitcher OnOff { get; set; }
        public GlobalMenu()
        {
            var RootMenu = MenuManager.CreateRootMenu("Tee.Snatcher");
            OnOff = RootMenu.CreateSwitcher("Enable", true);
            new Snatcher();
        }
    }
}