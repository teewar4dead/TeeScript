using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using Divine.SDK.Managers.Log;
using SharpDX;
using System;
using System.Linq;

namespace CourierController
{
    class Base
    {
        private void CourierOnOff(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e) { if (e.Value) { UpdateManager.IngameUpdate += UpdateGame; } else { UpdateManager.IngameUpdate -= UpdateGame; } }
        public Base()
        {
            GlobalMenu.CourierOnOff.ValueChanged += CourierOnOff;
        }
        private Sleeper sleeper = new Sleeper();
        private Sleeper sleeperMoveItem = new Sleeper();
        private void UpdateGame()
        {
            var MyHero = EntityManager.LocalHero;
            var MyCourier = EntityManager.GetEntities<Courier>().Where(x => x.IsAlive && x.IsControllable && x.Owner == MyHero.Owner).FirstOrDefault();
            if ((MyCourier == null || MyHero == null) && sleeper.Sleeping) return;
            if (!MyCourier.IsAlive) return;
            try
            {
                var DistanceFountain = 3000;
                var SpellCourierBurst = MyCourier.Spellbook.GetSpellById(AbilityId.courier_burst);
                var SpellCourierHome = MyCourier.Spellbook.GetSpellById(AbilityId.courier_return_stash_items);
                var SpellCourierShield = MyCourier.Spellbook.GetSpellById(AbilityId.courier_shield);
                var ObserverRadiant = GameManager.ItemStockInfos.FirstOrDefault(x => x.AbilityId == AbilityId.item_ward_observer && x.Team == Team.Radiant);
                var ObserverDire = GameManager.ItemStockInfos.FirstOrDefault(x => x.AbilityId == AbilityId.item_ward_observer && x.Team == Team.Dire);
                var CourierCheckFreeSlots = MyCourier.Inventory.GetFreeSlots(ItemSlot.MainSlot_1, ItemSlot.BackPack_3).Count() == 0;
                var SelectedCourier = MyHero.Player.SelectedUnits.Where(x => x.Handle == MyCourier.Handle).FirstOrDefault();
                var allheroEnemyRadiusCourier = EntityManager.GetEntities<Hero>().Where(x => x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero) && x.Distance2D(MyCourier.Position) < 2000).FirstOrDefault();
                var allheroEnemyRadiusFontanDire = EntityManager.GetEntities<Hero>().Where(x => x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero) && x.Distance2D(new Vector3(6263, 5764, 256)) < DistanceFountain).FirstOrDefault();
                var allheroEnemyRadiusFontanRadiant = EntityManager.GetEntities<Hero>().Where(x => x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero) && x.Distance2D(new Vector3(-6339, -5807, 256)) < DistanceFountain).FirstOrDefault();
                if (GlobalMenu.CourierBuyWard && MyCourier.ActiveShop == ShopType.Base && ObserverDire.StockCount != 0)
                {
                    var Item = MyCourier.Inventory.Items.FirstOrDefault(x => x.Id == AbilityId.item_ward_observer);

                    if (MyHero.Team == Team.Radiant && ObserverRadiant.StockCount != 0)
                    {
                        if (CourierCheckFreeSlots && Item != null)
                        {
                            Player.Buy(MyCourier, AbilityId.item_ward_observer);
                        }

                        else if (!CourierCheckFreeSlots)
                        {
                            Player.Buy(MyCourier, AbilityId.item_ward_observer);
                        }

                    }
                    else if (MyHero.Team == Team.Dire && ObserverDire.StockCount != 0)
                    {
                        if (CourierCheckFreeSlots && Item != null)
                        {
                            Player.Buy(MyCourier, AbilityId.item_ward_observer);
                        }

                        else if (!CourierCheckFreeSlots)
                        {
                            Player.Buy(MyCourier, AbilityId.item_ward_observer);
                        }
                    }


                }
                if (GlobalMenu.CourierFast.Value == GlobalMenu.selectorRun[1] && MyCourier.State == CourierState.Deliver && MyCourier.ActiveShop == ShopType.Base && SpellCourierBurst.Cooldown == 0 && MyCourier.Level >= 10)
                {
                    SpellCourierBurst.Cast();
                }

                if (GlobalMenu.CourierFast.Value == GlobalMenu.selectorRun[2] && (MyCourier.State == CourierState.Deliver || MyCourier.State == CourierState.BackToBase) && SpellCourierBurst.Cooldown == 0 && MyCourier.Level >= 10)
                {
                    SpellCourierBurst.Cast();
                }

                if (GlobalMenu.CourierFontan && (MyCourier.State == CourierState.AtBase && MyCourier.State != CourierState.Move && MyCourier.State != CourierState.Deliver) && SelectedCourier == null && MyCourier.ActiveShop == ShopType.Base)
                {
                    if (MyHero.Team == Team.Radiant && allheroEnemyRadiusFontanRadiant == null)
                    {
                        MyCourier.Move(new Vector3(-6339, -5807, 256));
                    }

                    else if (MyHero.Team == Team.Dire && allheroEnemyRadiusFontanDire == null)
                    {
                        MyCourier.Move(new Vector3(6263, 5764, 256));

                    }
                }

                else if (GlobalMenu.CourierFontan && (MyCourier.State != CourierState.Move && MyCourier.State != CourierState.Deliver) && SelectedCourier == null && MyCourier.ActiveShop == ShopType.Base)
                {

                    if (MyHero.Team == Team.Radiant && allheroEnemyRadiusFontanRadiant != null)
                    {
                        MyCourier.Move(new Vector3(-6928, -6528, 384));
                    }

                    else if (MyHero.Team == Team.Dire && allheroEnemyRadiusFontanDire != null)
                    {
                        MyCourier.Move(new Vector3(7008, 6387, 392));
                    }


                }


                if (GlobalMenu.CourierShield && allheroEnemyRadiusCourier != null && SpellCourierShield.Cooldown == 0 && MyCourier.Level >= 20)
                {
                    if ((allheroEnemyRadiusCourier.NetworkActivity == NetworkActivity.Attack || allheroEnemyRadiusCourier.NetworkActivity == NetworkActivity.Attack2) && MyCourier.Distance2D(allheroEnemyRadiusCourier.Position) <= allheroEnemyRadiusCourier.AttackRange)
                        SpellCourierShield.Cast();

                }
            }
            catch (Exception e)
            {
                LogManager.Error(e + " | " + "Error 2");
            }

            if (GlobalMenu.CourierMoveSlot && MyCourier.Distance2D(MyHero.Position) <= 1000 && MyCourier.State == CourierState.Deliver && !sleeperMoveItem.Sleeping && MyHero.IsAlive)
            {
                try
                {
                    var ObserverCourier = MyCourier.Inventory.Items.FirstOrDefault(x => x.Id == AbilityId.item_ward_observer);
                    var TPCourier = MyCourier.Inventory.Items.FirstOrDefault(x => x.Id == AbilityId.item_tpscroll);
                    var ItemAllnoRecipeNoNeutrals = MyCourier.Inventory.Items.Where(x => x.KeyValue.GetKeyValue("ItemIsNeutralDrop").GetBooleon() == false && !x.IsRecipe);
                    var MoveItems = MyHero.Inventory.MainItems.OrderBy(x => x.Cost).FirstOrDefault(x => x.IsSellable && x.Cost <= 450 && x.IsValid);


                    if (MyCourier.Inventory.MainItems.Count() == 1 && TPCourier != null || (MyCourier.Inventory.MainItems.Count() == 1 && ObserverCourier != null))
                    {
                        //nothing
                    }
                    else if 
                        (ItemAllnoRecipeNoNeutrals.Count() != 0
                        && MyHero.Inventory.FreeMainSlots.Count() == 0 
                        && MyHero.Inventory.FreeBackpackSlots.Count() != 0 
                        && (MyHero.Inventory.FreeBackpackSlots.Count() == 3 || MyHero.Inventory.FreeBackpackSlots.Count() == 2 || MyHero.Inventory.FreeBackpackSlots.Count() == 1) 
                        && MoveItems != null 
                        && !sleeperMoveItem.Sleeping)
                    {
                        MyHero.Inventory.Move(MoveItems, MyHero.Inventory.FreeBackpackSlots.FirstOrDefault());
                        sleeperMoveItem.Sleep(10000);
                    }
                }
                catch (Exception e)
                {
                    LogManager.Error(e + " | " + "Error 1");

                }
                
            }
            sleeper.Sleep(100);
        }
    }
}
