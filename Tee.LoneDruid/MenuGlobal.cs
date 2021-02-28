using Divine.Menu;
using Divine.Menu.Items;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeeLoneDruid
{
    class MenuGlobal
    {
        private BearConfig bearConfig { get; }
        public static MenuSwitcher OnOff { get; set; }
        public MenuGlobal()
        {
            var RootMenu = MenuManager.CreateRootMenu("Tee.LoneDruid");
          
            OnOff = RootMenu.CreateSwitcher("Enable", true);
            var BearMenu = RootMenu.CreateMenu("Spirit Bear");
            BearMenu.SetAbilityTexture(Divine.AbilityId.lone_druid_spirit_bear);
            RootMenu.SetFontColor(Color.Indigo);

            bearConfig = new BearConfig(BearMenu);


        }
    }
}
