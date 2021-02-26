using Divine;
using Divine.SDK.Extensions;
using System;
using System.Linq;

namespace TeeLoneDruid.Bear.Items
{
    class item_orchid
    {
        public item_orchid(Unit unit, Unit target)
        {
            var item = AbilityId.item_orchid;




            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Position.Distance2D(target.Position) <= 350)
            {
                itemsHelper.CastItemTarget(unit, target, item);
            }
        }
    }
}
