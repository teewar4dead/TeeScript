using Divine;
using System;

namespace TeeLoneDruid.Bear.Items
{
    class item_spider_legs
    {
        public item_spider_legs(Unit unit)
        {
            var item = AbilityId.item_spider_legs;






            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.IsMoving)
            {

                itemsHelper.CastItem(unit, item);
            }
        }
    }
}
