using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.Snatcher
{
    class Snatcher
    {

        private readonly Sleeper Sleeper1 = new Sleeper();
        private readonly Sleeper Sleeper2 = new Sleeper();
        private readonly Sleeper Sleeper3 = new Sleeper();
        private readonly Sleeper Sleeper4 = new Sleeper();
        private readonly Sleeper Sleeper5 = new Sleeper();
        private readonly Sleeper Sleeper6 = new Sleeper();
        private readonly Sleeper Sleeper7 = new Sleeper();


        private readonly Sleeper Sleeper11 = new Sleeper();
        private readonly Sleeper Sleeper22 = new Sleeper();
        private readonly Sleeper Sleeper33 = new Sleeper();
        private readonly Sleeper Sleeper44 = new Sleeper();
        private readonly Sleeper Sleeper55 = new Sleeper();
        private readonly Sleeper Sleeper66 = new Sleeper();
        private readonly Sleeper Sleeper77 = new Sleeper();

        public static readonly Hero MyHero = EntityManager.LocalHero;
        public Snatcher()
        {
            GlobalMenu.OnOff.ValueChanged += OnOff_ValueChanged;
        }

        private void OnOff_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
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
            var Bounty = EntityManager.GetEntities<Rune>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && (x.RuneType & RuneType.Bounty) == RuneType.Bounty && x.IsVisible && x.IsValid);
            var Aegis = EntityManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && x.Item.Id == AbilityId.item_aegis && x.IsVisible && x.IsValid);
            var Cheese = EntityManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && x.Item.Id == AbilityId.item_cheese && x.IsVisible && x.IsValid); ;
            var Refresher = EntityManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && x.Item.Id == AbilityId.item_refresher_shard && x.IsVisible && x.IsValid);
            var Aghanim = EntityManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && x.Item.Id == AbilityId.item_ultimate_scepter_2 && x.IsVisible && x.IsValid);
            var Gem = EntityManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && x.Item.Id == AbilityId.item_gem && x.IsVisible && x.IsValid);
            var Rapier = EntityManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && x.Item.Id == AbilityId.item_rapier && x.IsVisible && x.IsValid);
            var Neutrals = EntityManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && x.Item.KeyValue.GetKeyValue("ItemIsNeutralDrop").GetBooleon() && x.IsVisible && x.IsValid);



            try
            {

            }
            catch (Exception)
            {

            }




            if (Bounty != null && GlobalMenu.ListRuneToggler["Bounty"])
            {
                MyHero.PickUp(Bounty);
            }



            if (Aegis != null && GlobalMenu.ListRuneToggler["Aegis"] && (MyHero.Inventory.FreeMainSlots.Count() != 0) && MyHero.ActiveShop != ShopType.Base && !Sleeper11.Sleeping)
            {
                MyHero.PickUp(Aegis);
                Sleeper11.Sleep(150);
            }
            else if (Aegis != null && GlobalMenu.ListRuneToggler["Aegis"] && MyHero.Inventory.FreeBackpackSlots.Count() != 0 && MyHero.ActiveShop != ShopType.Base && !Sleeper1.Sleeping)
            {

                MyHero.Inventory.Move(MyHero.Inventory.MainItems.OrderBy(x => x.Cost).FirstOrDefault(x=> x.IsSellable), MyHero.Inventory.FreeBackpackSlots.FirstOrDefault());
                Sleeper1.Sleep(1000);

            }
            else if (Aegis != null && GlobalMenu.ListRuneToggler["Aegis"] && MyHero.Inventory.GetFreeSlots(ItemSlot.BackPack_1, ItemSlot.MainSlot_6).Count() == 0 && MyHero.ActiveShop != ShopType.Base && !Sleeper1.Sleeping)
            {
                var item = MyHero.Inventory.Items.OrderBy(x => x.Cost).FirstOrDefault(x => x.Id != AbilityId.item_tpscroll && x.Cost < 449);
                if (item != null)
                {
                    
                    MyHero.Drop(item, MyHero.Position);
                }
            }




            if (Cheese != null && GlobalMenu.ListRuneToggler["Cheese"] && MyHero.Inventory.GetFreeSlots(ItemSlot.BackPack_1, ItemSlot.MainSlot_6).Count() != 0 && MyHero.ActiveShop != ShopType.Base && !Sleeper22.Sleeping)
            {

                MyHero.PickUp(Cheese);
                Sleeper22.Sleep(150);
            }





            if (Refresher != null && GlobalMenu.ListRuneToggler["Refresher"] && MyHero.Inventory.GetFreeSlots(ItemSlot.BackPack_1, ItemSlot.MainSlot_6).Count() != 0 && MyHero.ActiveShop != ShopType.Base && !Sleeper33.Sleeping)
            {
                MyHero.PickUp(Refresher);
                Sleeper33.Sleep(150);
            }




            if (Aghanim != null && GlobalMenu.ListRuneToggler["Aghanim"] && (MyHero.Inventory.FreeMainSlots.Count() != 0) && MyHero.ActiveShop != ShopType.Base && !Sleeper44.Sleeping)
            {
                MyHero.PickUp(Aghanim);
                Sleeper44.Sleep(150);
            }
            else if (Aghanim != null && GlobalMenu.ListRuneToggler["Aghanim"] && MyHero.Inventory.FreeBackpackSlots.Count() != 0 && MyHero.ActiveShop != ShopType.Base && !Sleeper2.Sleeping)
            {
                MyHero.Inventory.Move(MyHero.Inventory.MainItems.OrderBy(x => x.Cost).FirstOrDefault(x => x.IsSellable), MyHero.Inventory.GetFreeSlots(ItemSlot.BackPack_1, ItemSlot.BackPack_3).FirstOrDefault());
                Sleeper2.Sleep(1000);
            }




            if (Gem != null && GlobalMenu.ListRuneToggler["Gem"] && (MyHero.Inventory.FreeMainSlots.Count() != 0) && MyHero.ActiveShop != ShopType.Base && !Sleeper55.Sleeping)
            {
                MyHero.PickUp(Gem);
                Sleeper55.Sleep(150);
            }
            else if (Gem != null && GlobalMenu.ListRuneToggler["Gem"] && MyHero.Inventory.FreeBackpackSlots.Count() != 0 && MyHero.ActiveShop != ShopType.Base && !Sleeper3.Sleeping)
            {
                MyHero.Inventory.Move(MyHero.Inventory.MainItems.OrderBy(x => x.Cost).FirstOrDefault(x => x.IsSellable), MyHero.Inventory.GetFreeSlots(ItemSlot.BackPack_1, ItemSlot.BackPack_3).FirstOrDefault());
                Sleeper3.Sleep(1000);
            }



            if (Rapier != null && GlobalMenu.ListRuneToggler["Rapier"] && (MyHero.Inventory.FreeMainSlots.Count() != 0) && MyHero.ActiveShop != ShopType.Base && !Sleeper66.Sleeping)
            {
                MyHero.PickUp(Rapier);
                Sleeper66.Sleep(150);
            }
            else if (Rapier != null && GlobalMenu.ListRuneToggler["Rapier"] && MyHero.Inventory.FreeBackpackSlots.Count() != 0 && MyHero.ActiveShop != ShopType.Base && !Sleeper4.Sleeping)
            {

                MyHero.Inventory.Move(MyHero.Inventory.MainItems.OrderBy(x => x.Cost).FirstOrDefault(x => x.IsSellable), MyHero.Inventory.GetFreeSlots(ItemSlot.BackPack_1, ItemSlot.BackPack_3).FirstOrDefault());
                Sleeper4.Sleep(1000);
            }
            else if (Rapier != null && GlobalMenu.ListRuneToggler["Rapier"] && MyHero.Inventory.GetFreeSlots(ItemSlot.BackPack_1, ItemSlot.MainSlot_6).Count() == 0 && MyHero.ActiveShop != ShopType.Base && !Sleeper1.Sleeping)
            {
                var item = MyHero.Inventory.Items.OrderBy(x => x.Cost).FirstOrDefault(x => x.Id != AbilityId.item_tpscroll && x.Cost < 449);
                if (item != null)
                {
                    MyHero.Drop(item, MyHero.Position);
                    Sleeper1.Sleep(1000);
                }
            }


            if (Neutrals != null && GlobalMenu.ListRuneToggler["Neutrals"] && (MyHero.Inventory.FreeBackpackSlots.Count() != 0 || MyHero.Inventory.NeutralItem == null) && MyHero.ActiveShop != ShopType.Base && !Sleeper77.Sleeping)
            {

                MyHero.PickUp(Neutrals);
                Sleeper77.Sleep(150);
            }
            else if (Neutrals != null && GlobalMenu.ListRuneToggler["Neutrals"] && MyHero.Inventory.FreeMainSlots.Count() != 0 && !Sleeper5.Sleeping && MyHero.ActiveShop != ShopType.Base)
            {
                
                if (MyHero.Inventory.Move(MyHero.Inventory.BackpackItems.FirstOrDefault(x => x.IsPurchasable), MyHero.Inventory.GetFreeSlots(ItemSlot.MainSlot_1, ItemSlot.MainSlot_6).FirstOrDefault()))
                {
                }
                Sleeper5.Sleep(1000);
            }





        }
    }
}
