using Divine;
using Divine.Menu;
using Divine.Menu.Items;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using Divine.SDK.Managers.Log;
using System;
using System.Collections.Generic;
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
            RootMenu.SetAbilityTexture(AbilityId.item_travel_boots);
            OnOff = RootMenu.CreateSwitcher("Enable");
            Select = RootMenu.CreateSelector("Mode", new string[] { "Drop to the ground", "Drop in courier" });
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
        }
        private void UpdateManager_IngameUpdate()
        {

            var MyCourier = EntityManager.GetEntities<Courier>().Where(x => x.IsControllable && x.Owner == MyHero.Owner).FirstOrDefault();
            if (SleeperOrder_1.Sleeping)
            {
                return;
            }
            if (MyHero == null || MyCourier == null)
            {
                return;
            }





            if (Select.Value == "Drop in courier")
            {
                if (!FindItem(MyHero, AbilityId.item_travel_boots))
                {
                    if(MyCourier.IsAlive)
                    {
                        Player.Buy(MyHero, AbilityId.item_boots);
                        Player.Buy(MyHero, AbilityId.item_recipe_travel_boots);
                    }
                    else
                    {
                        UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
                    }
                }

                MyCourier.Move(MyHero.Position);
                foreach (var item in MyHero.Inventory.Items)
                {
                    if (item.Id == AbilityId.item_travel_boots)
                    {
                        if (Player.Sell(MyHero, item) && !Abuse)
                        {
                            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
                        }
                    }

                }
                if (MyCourier.Position.Distance2D(MyHero.Position) <= 200)
                {
                    MyHero.Give(MyHero.Inventory.TownPortalScroll, MyCourier);
                }
            }
            if (Select.Value == "Drop to the ground")
            {
                if (!FindItem(MyHero, AbilityId.item_travel_boots))
                {
                    Player.Buy(MyHero, AbilityId.item_boots);
                    Player.Buy(MyHero, AbilityId.item_recipe_travel_boots);
                }
                foreach (var item in MyHero.Inventory.Items)
                {
                    if (item.Id == AbilityId.item_travel_boots)
                    {
                            if (Player.Sell(MyHero, item) && !Abuse)
                            {
                                UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
                            }
                    }

                }
                MyHero.Drop(MyHero.Inventory.TownPortalScroll, MyHero.Position);
            }

            SleeperOrder_1.Sleep(100);

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
