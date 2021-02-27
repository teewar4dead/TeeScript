using Divine;
using Divine.Menu;
using Divine.Menu.Items;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using System;
using System.Linq;
using System.Windows.Input;

namespace AbuseTP
{
    public class Main : Bootstrapper
    {
        private readonly Sleeper SleeperOrder_1 = new Sleeper();
        private MenuHoldKey Abuse { get; set; }
        private static readonly Hero MyHero = EntityManager.LocalHero;
        protected override void OnActivate()
        {
            var RootMenu = MenuManager.CreateRootMenu("Tee.AbuseTP");
            Abuse = RootMenu.CreateHoldKey("lol", "Abuse Key", Key.None);
            Abuse.ValueChanged += Abuse_ValueChanged;

        }

        private void Abuse_ValueChanged(MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
            }
            else
            {
                UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
            }
        }

        private void UpdateManager_IngameUpdate()
        {
            if (SleeperOrder_1.Sleeping)
            {
                return;
            }
            if (MyHero == null)
            {
                return;
            }
            SleeperOrder_1.Sleep(150);
            var courier = EntityManager.GetEntities<Courier>().Where(x => x.IsAlive && x.IsControllable).FirstOrDefault();
            var shop = EntityManager.GetEntities<Shop>().FirstOrDefault(x=> x.Position.Distance2D(MyHero.Position) <= 400);
            if (MyHero.Player.ReliableGold <= 2500 && MyHero.ActiveShop == ShopType.Base)
            {
                if (!FindItem(MyHero, AbilityId.item_travel_boots))
                {
                    Player.Buy(MyHero, AbilityId.item_boots); // не работает
                    Player.Buy(MyHero, AbilityId.item_recipe_travel_boots); // не работает
                }
                courier.Follow(MyHero);
                if (courier.Position.Distance2D(MyHero.Position) <= 200)
                {
                    MyHero.Give(MyHero.Inventory.TownPortalScroll, courier);
                    Player.Sell(MyHero, MyHero.Inventory.Item1); // не работает
                }
            }
            
            
            


        }
        public static bool FindItem(Unit unit, AbilityId abilityId)
        {
            var Item = unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);

            bool BoolItem;

            if (Item != null)
            {
                BoolItem = true;
            }
            else
            {
                BoolItem = false;
            }

            return BoolItem;
        }
    }
}
