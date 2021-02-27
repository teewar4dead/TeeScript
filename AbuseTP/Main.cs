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
        private bool check = false;
        private readonly Sleeper SleeperOrder_1 = new Sleeper();
        private MenuHoldKey Abuse { get; set; }
        private MenuSwitcher OnOff { get; set; }
        private MenuSelector Select { get; set; }
        private static readonly Hero MyHero = EntityManager.LocalHero;
        protected override void OnActivate()
        {

            var RootMenu = MenuManager.CreateRootMenu("Tee.AbuseTP");
            OnOff = RootMenu.CreateSwitcher("Enable");
            Select = RootMenu.CreateSelector("Mode", new string[] { "Drop", "Stack" });
            RootMenu.CreateText("Buy TP (bag), need 2500 gold");
            Abuse = RootMenu.CreateHoldKey("lol", "Abuse Key", Key.None);
            OnOff.ValueChanged += OnOff_ValueChanged;

        }

        private void OnOff_ValueChanged(MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                Abuse.ValueChanged += Abuse_ValueChanged;
            }
            else
            {
                Abuse.ValueChanged -= Abuse_ValueChanged;
            }
        }

        private void Abuse_ValueChanged(MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {

            if (e.Value)
            {
                if (MyHero.Player.UnreliableGold >= 2500 && MyHero.ActiveShop == ShopType.Base && !FindItem(MyHero, AbilityId.item_travel_boots))
                {

                    UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
                }

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
            var shop = EntityManager.GetEntities<Shop>().FirstOrDefault(x => x.Position.Distance2D(MyHero.Position) <= 400);

            {
                if (!FindItem(MyHero, AbilityId.item_travel_boots))
                {
                    Player.Buy(MyHero, AbilityId.item_boots); // не работает
                    Player.Buy(MyHero, AbilityId.item_recipe_travel_boots); // не работает
                }



                if (Select.Value == "Drop")
                {
                    foreach (var item in MyHero.Inventory.Items)
                    {
                        if (item.Id == AbilityId.item_travel_boots)
                        {
                            Player.Sell(MyHero, item);
                        }

                    }
                    MyHero.Drop(MyHero.Inventory.TownPortalScroll, MyHero.Position);
                }
                if (Select.Value == "Stack")
                {
                    courier.Move(MyHero.Position);
                    if (courier.Position.Distance2D(MyHero.Position) <= 200)
                    {
                        foreach (var item in MyHero.Inventory.Items)
                        {
                            if (item.Id == AbilityId.item_travel_boots)
                            {
                                Player.Sell(MyHero, item);
                            }

                        }

                        MyHero.Give(MyHero.Inventory.TownPortalScroll, courier);

                    }
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
