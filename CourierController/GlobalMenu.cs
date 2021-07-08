using Divine.Entity.Entities.Abilities.Components;
using Divine.Menu;
using Divine.Menu.Items;
using Divine.Renderer;

namespace CourierController
{
    class GlobalMenu
    {
        public static MenuSwitcher CourierOnOff { get; set; }
        public static MenuSwitcher CourierMoveSlot { get; set; }
        public static MenuSwitcher CourierBuyWard { get; set; }
        public static MenuSwitcher CourierShield { get; set; }
        public static MenuSelector CourierFast { get; set; }
        public static MenuSwitcher CourierFontan { get; set; }
        public static string[] selectorRun = { "Отключить", "При доставке", "По кд" };
        public GlobalMenu()
        {
            RendererManager.LoadImage("Fontan", "panorama/images/spellicons/fountain_heal_png.vtex_c");
            var RootMenu = MenuManager.CreateRootMenu("CourierController");
            RootMenu.SetAbilityImage(AbilityId.item_courier);
            CourierOnOff = RootMenu.CreateSwitcher("Включить", false);
            CourierMoveSlot = RootMenu.CreateSwitcher("Автоматически освободить слот при доставке предмета", false);
            CourierMoveSlot.SetAbilityImage(AbilityId.courier_transfer_items);
            CourierBuyWard = RootMenu.CreateSwitcher("Авто-покупка вардов", false);
            CourierBuyWard.SetAbilityImage(AbilityId.item_ward_observer);
            CourierShield = RootMenu.CreateSwitcher("Использовать щит в случае необходимости", false);
            CourierShield.SetAbilityImage(AbilityId.courier_shield);
            CourierFontan = RootMenu.CreateSwitcher("Держать расстояния от фонтана", false);
            CourierFontan.SetImage("Fontan");
            CourierFast = RootMenu.CreateSelector("Использовать ускорения", selectorRun);
            CourierFast.SetAbilityImage(AbilityId.courier_burst);
            new Base();
        }
    }
}
