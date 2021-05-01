using Divine;
using Divine.Menu;
using Divine.Menu.Items;

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
            RendererManager.LoadTexture("Fontan", "panorama/images/spellicons/fountain_heal_png.vtex_c");
            var RootMenu = MenuManager.CreateRootMenu("CourierController");
            RootMenu.SetAbilityTexture(AbilityId.item_courier);
            CourierOnOff = RootMenu.CreateSwitcher("Включить", false);
            CourierMoveSlot = RootMenu.CreateSwitcher("Автоматически освободить слот при доставке предмета", false);
            CourierMoveSlot.SetAbilityTexture(AbilityId.courier_transfer_items);
            CourierBuyWard = RootMenu.CreateSwitcher("Авто-покупка вардов", false);
            CourierBuyWard.SetAbilityTexture(AbilityId.item_ward_observer);
            CourierShield = RootMenu.CreateSwitcher("Использовать щит в случае необходимости", false);
            CourierShield.SetAbilityTexture(AbilityId.courier_shield);
            CourierFontan = RootMenu.CreateSwitcher("Держать расстояния от фонтана", false);
            CourierFontan.SetTexture("Fontan");
            CourierFast = RootMenu.CreateSelector("Использовать ускорения", selectorRun);
            CourierFast.SetAbilityTexture(AbilityId.courier_burst);
            new Base();
        }
    }
}
