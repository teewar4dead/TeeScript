using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
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
            if (MyCourier == null || MyHero == null || sleeper.Sleeping) return;
            var SpellCourierBurst = MyCourier.Spellbook.GetSpellById(AbilityId.courier_burst);
            var SpellCourierHome = MyCourier.Spellbook.GetSpellById(AbilityId.courier_return_stash_items);
            var SpellCourierShield = MyCourier.Spellbook.GetSpellById(AbilityId.courier_shield);
            var ObserverRadiant = GameManager.ItemStockInfos.FirstOrDefault(x => x.AbilityId == AbilityId.item_ward_observer && x.Team == Team.Radiant);
            var ObserverDire = GameManager.ItemStockInfos.FirstOrDefault(x => x.AbilityId == AbilityId.item_ward_observer && x.Team == Team.Dire);
            var ObserverCourier = MyCourier.Inventory.Items.FirstOrDefault(x => x.Id == AbilityId.item_ward_observer);
            var TPCourier =  MyCourier.Inventory.Items.FirstOrDefault(x => x.Id == AbilityId.item_tpscroll);
            var CourierCheckFreeSlots = MyCourier.Inventory.GetFreeSlots(ItemSlot.MainSlot_1, ItemSlot.BackPack_3).Count() == 0;
            var MyHeroCheckFreeBackPackSlots = MyHero.Inventory.GetFreeSlots(ItemSlot.BackPack_1, ItemSlot.BackPack_3).Count() == 0;
            var MyHeroCheckFreeBackMainSlots = MyHero.Inventory.GetFreeSlots(ItemSlot.MainSlot_1, ItemSlot.MainSlot_6).Count() == 0;
            var SelectedCourier = MyHero.Player.SelectedUnits.Where(x => x.Handle == MyCourier.Handle).FirstOrDefault();
            Console.WriteLine(MyCourier.Position);
            var allheroEnemyRadiusCourier = EntityManager.GetEntities<Hero>().Where(x => x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero) && x.Distance2D(MyCourier.Position) < 2000).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();
            var allheroEnemyRadiusFontanDire = EntityManager.GetEntities<Hero>().Where(x => x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero) && x.Distance2D(new Vector3(6263, 5764, 256)) < 1500).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();
            var allheroEnemyRadiusFontanRadiant = EntityManager.GetEntities<Hero>().Where(x => x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero) && x.Distance2D(new Vector3(-6339, -5807, 256)) < 1500).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();
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
            if(GlobalMenu.CourierFast.Value == GlobalMenu.selectorRun[1] && MyCourier.State == CourierState.Deliver && MyCourier.ActiveShop == ShopType.Base && SpellCourierBurst.Cooldown == 0 && SpellCourierBurst.CastPoint != 0)
            {
                SpellCourierBurst.Cast();
            }
            if (GlobalMenu.CourierFast.Value == GlobalMenu.selectorRun[2] && (MyCourier.State == CourierState.Deliver || MyCourier.State == CourierState.BackToBase) && SpellCourierBurst.Cooldown == 0 && SpellCourierBurst.CastPoint != 0)
            {
                SpellCourierBurst.Cast();
            }
            if(GlobalMenu.CourierFontan && (MyCourier.State == CourierState.AtBase && MyCourier.State != CourierState.Move && MyCourier.State != CourierState.Deliver) && SelectedCourier == null && MyCourier.ActiveShop == ShopType.Base)
            {
                if(MyHero.Team == Team.Radiant && allheroEnemyRadiusFontanRadiant == null)
                {
                    MyCourier.Move(new Vector3(-6339, -5807, 256));
                }
                else if(MyHero.Team == Team.Dire && allheroEnemyRadiusFontanDire == null)
                {
                    MyCourier.Move(new Vector3(6263 ,5764, 256));
                    
                }
            }
            else if (GlobalMenu.CourierFontan && ( MyCourier.State != CourierState.Move && MyCourier.State != CourierState.Deliver) && SelectedCourier == null && MyCourier.ActiveShop == ShopType.Base)
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
            if (GlobalMenu.CourierMoveSlot && !MyHeroCheckFreeBackPackSlots && MyCourier.Distance2D(MyHero.Position) <= 1000 && MyCourier.State == CourierState.Deliver && !sleeperMoveItem.Sleeping)
            {
                if(MyCourier.Inventory.MainItems.Count() == 1 && TPCourier != null || (MyCourier.Inventory.MainItems.Count() == 1 && ObserverCourier != null))
                {
                    //nothing
                }
                else if(MyHero.Inventory.MainItems.Count() == 6)
                {
                    MyHero.Inventory.Move(MyHero.Inventory.MainItems.OrderBy(x => x.Cost <= 450 ).FirstOrDefault(x => x.IsSellable), MyHero.Inventory.FreeBackpackSlots.FirstOrDefault());
                }
                sleeperMoveItem.Sleep(5000);
            }
            if(GlobalMenu.CourierShield  && allheroEnemyRadiusCourier!= null && SpellCourierShield.Cooldown == 0 && MyCourier.Level >= 20)
            {
                if((allheroEnemyRadiusCourier.NetworkActivity == NetworkActivity.Attack || allheroEnemyRadiusCourier.NetworkActivity == NetworkActivity.Attack2)&& MyCourier.Distance2D(allheroEnemyRadiusCourier.Position) <= allheroEnemyRadiusCourier.AttackRange)
                SpellCourierShield.Cast();
            }
            sleeper.Sleep(100);
        }
    }
}
