using Divine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeeLoneDruid.Bear.Items
{
    class itemsHelper
    {

        private static bool ManaCheck (float manaCost, float manaPool)
        {
            if (manaPool - manaCost > 0)
                return true;
            return false;
        }

        public static bool FindItem(Unit unit, AbilityId abilityId)
        {
            bool BoolItem = false;
            try
            {
                var Item = unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);

                if (Item != null)
                {
                    BoolItem = true;
                }
                else
                {
                    BoolItem = false;
                }

            }
            catch (Exception)
            {

               
            }

            return BoolItem;

        }
        public static void CastItem(Unit unit, AbilityId abilityId)
        {
            var Item = unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);
            if (Item != null && Item.Level > 0 && Item.Cooldown == 0 && ManaCheck(Item.ManaCost, unit.Mana))
            {
                Item.Cast();
            }
        }
        public static void CastItemTarget(Unit unit, Unit target, AbilityId abilityId)
        {
            try
            {
                var Item = unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);
                if (Item != null && Item.Level > 0 && Item.Cooldown == 0 && ManaCheck(Item.ManaCost, unit.Mana))
                {
                    Item.Cast(target);
                }
            }
            catch (Exception)
            {

                //nothing
            }
           
        }
    }
}
