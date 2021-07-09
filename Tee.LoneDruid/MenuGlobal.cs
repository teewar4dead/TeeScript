using Divine.Entity.Entities.Abilities.Components;
using Divine.Menu;
using Divine.Menu.Items;
using Divine.Numerics;

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
            BearMenu.SetAbilityImage(AbilityId.lone_druid_spirit_bear);
            RootMenu.SetFontColor(Color.Indigo);

            bearConfig = new BearConfig(BearMenu);


        }
    }
}
