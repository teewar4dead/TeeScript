using Divine;
using Divine.Entity;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Abilities.Items.Components;
using Divine.Entity.Entities.Components;
using Divine.Entity.Entities.Players;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Components;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Game;
using Divine.Helpers;
using Divine.Log;
using Divine.Numerics;
using Divine.Update;

using System;
using System.Linq;

namespace CourierController
{
    internal sealed class Base
    {
        public Base() => GlobalMenu.CourierOnOff.ValueChanged += CourierOnOff;
        private void CourierOnOff(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e) { if (e.Value) { UpdateManager.IngameUpdate += UpdateGame; } else { UpdateManager.IngameUpdate -= UpdateGame; } }

        private Sleeper sleeper = new Sleeper();
        private Sleeper sleeperMoveItem = new Sleeper();
        private uint DistanceFountain = 2500;

        private void UpdateGame()
        {
            var MyHero = EntityManager.LocalHero;
            var MyCourier = EntityManager.GetEntities<Courier>().Where(x => x.IsControllable && x.Owner == MyHero.Owner).FirstOrDefault();

            if (MyHero == null) return;
            if (MyCourier == null) return;
            if (!MyCourier.IsAlive) return;
            if (GameManager.GameMode == GameMode.Turbo || GameManager.GameMode == GameMode.Custom) return;
            if (sleeper.Sleeping) return;

            var SpellCourierBurst = MyCourier.Spellbook.GetSpellById(AbilityId.courier_burst);
            var SpellCourierHome = MyCourier.Spellbook.GetSpellById(AbilityId.courier_return_stash_items);
            var SpellCourierShield = MyCourier.Spellbook.GetSpellById(AbilityId.courier_shield);
            var ObserverAviable = GameManager.ItemStockInfos.FirstOrDefault(x => x.AbilityId == AbilityId.item_ward_observer && (MyHero.Team == Team.Radiant && x.Team == Team.Radiant) || (MyHero.Team == Team.Dire && x.Team == Team.Dire)).IsAvailable;
            var CourierCheckFreeSlots = MyCourier.Inventory.GetFreeSlots(ItemSlot.MainSlot1, ItemSlot.BackPack3).Count() == 0;
            var SelectedCourier = MyHero.Player.SelectedUnits.Where(x => x.Handle == MyCourier.Handle).FirstOrDefault();
            var allheroEnemyRadiusCourier = EntityManager.GetEntities<Hero>().Where(x => x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero) && x.Distance2D(MyCourier.Position) < 2000).FirstOrDefault();
            var EnemyRadiusFontan = EntityManager.GetEntities<Hero>().Where(x => (x.Distance2D(new Vector3(-6339, -5807, 256)) < DistanceFountain && MyHero.Team == Team.Radiant && x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero)) || (x.Distance2D(new Vector3(6263, 5764, 256)) < DistanceFountain && MyHero.Team == Team.Dire && x.IsAlive && x.IsVisible && x.IsValid && !x.IsAlly(MyHero)) && x.Name != MyHero.Name).FirstOrDefault();

            if (ObserverAviable && !CourierCheckFreeSlots && GlobalMenu.CourierBuyWard && MyCourier.ActiveShop == ShopType.Base)
            {
                Player.Buy(MyCourier, AbilityId.item_ward_observer);
            }

            if (GlobalMenu.CourierFast.Value == GlobalMenu.selectorRun[1] && MyCourier.State == CourierState.Deliver && MyCourier.ActiveShop == ShopType.Base && SpellCourierBurst.Cooldown == 0 && MyCourier.Level >= 10)
            {
                SpellCourierBurst.Cast();
            }
            else if (GlobalMenu.CourierFast.Value == GlobalMenu.selectorRun[2] && (MyCourier.State == CourierState.Deliver || MyCourier.State == CourierState.BackToBase) && SpellCourierBurst.Cooldown == 0 && MyCourier.Level >= 10)
            {
                SpellCourierBurst.Cast();
            }

            if (GlobalMenu.CourierFontan && (MyCourier.State != CourierState.Move && MyCourier.State != CourierState.Deliver) && SelectedCourier == null && MyCourier.ActiveShop == ShopType.Base)
            {

                if (EnemyRadiusFontan == null && MyCourier.State == CourierState.AtBase)
                {
                    MyCourier.Move(MyHero.Team == Team.Radiant ? new Vector3(-6339, -5807, 256) : new Vector3(6263, 5764, 256));
                }
                else if (EnemyRadiusFontan != null && MyCourier.State != CourierState.AtBase)
                {
                    MyCourier.Move(MyHero.Team == Team.Radiant ? new Vector3(-6928, -6528, 384) : new Vector3(7008, 6387, 392));
                }

            }


            if (GlobalMenu.CourierShield && allheroEnemyRadiusCourier != null && SpellCourierShield.Cooldown == 0 && MyCourier.Level >= 20)
            {
                if ((allheroEnemyRadiusCourier.NetworkActivity == NetworkActivity.Attack || allheroEnemyRadiusCourier.NetworkActivity == NetworkActivity.Attack2) && MyCourier.Distance2D(allheroEnemyRadiusCourier.Position) <= allheroEnemyRadiusCourier.AttackRange)
                    SpellCourierShield.Cast();

            }

            if (GlobalMenu.CourierMoveSlot && MyCourier.Distance2D(MyHero.Position) <= 1000 && MyCourier.State == CourierState.Deliver && !sleeperMoveItem.Sleeping && MyHero.IsAlive)
            {
                var ObserverCourier = MyCourier.Inventory.Items.FirstOrDefault(x => x.Id == AbilityId.item_ward_observer);
                var TPCourier = MyCourier.Inventory.Items.FirstOrDefault(x => x.Id == AbilityId.item_tpscroll);
                var ItemAllnoRecipeNoNeutrals = MyCourier.Inventory.Items.Where(x => x.KeyValue.GetSubKey("ItemIsNeutralDrop").GetBooleon() == false && !x.IsRecipe);
                var MoveItems = MyHero.Inventory.MainItems.OrderBy(x => x.Cost).FirstOrDefault(x => x.IsSellable && x.Cost <= 450 && x.IsValid);

                bool checkTpAndObserver =
                    MyCourier.Inventory.MainItems.Count() == 1
                    && TPCourier != null
                    || (MyCourier.Inventory.MainItems.Count() == 1 && ObserverCourier != null)
                    || (TPCourier != null && ObserverCourier != null && MyCourier.Inventory.MainItems.Count() == 2);

                bool checkitems =
                    ItemAllnoRecipeNoNeutrals.Count() != 0
                    && MyHero.Inventory.FreeMainSlots.Count() == 0
                    && MyHero.Inventory.FreeBackpackSlots.Count() != 0
                    && (MyHero.Inventory.FreeBackpackSlots.Count() == 3 || MyHero.Inventory.FreeBackpackSlots.Count() == 2 || MyHero.Inventory.FreeBackpackSlots.Count() == 1)
                    && MoveItems != null && !sleeperMoveItem.Sleeping;

                if (checkitems && !checkTpAndObserver)
                {
                    MyHero.Inventory.Move(MoveItems, MyHero.Inventory.FreeBackpackSlots.FirstOrDefault());
                    sleeperMoveItem.Sleep(10000);
                }
            }
            sleeper.Sleep(100);
        }
    }
}
