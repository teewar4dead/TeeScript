using Divine;
using Divine.SDK.Extensions;
using System;

namespace TeeLoneDruid.Bear.Items
{
    class item_bloodthorn
    {
        public item_bloodthorn(Unit unit, Unit target)
        {
            var item = AbilityId.item_bloodthorn;



            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Position.Distance2D(target.Position) <= 350)
            {
                itemsHelper.CastItemTarget(unit, target, item);
            }
        }
    }
}
