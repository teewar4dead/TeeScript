using Divine;
using Divine.SDK.Extensions;
using System;

namespace TeeLoneDruid.Bear.Items
{
    class item_mjollnir
    {
        public item_mjollnir(Unit unit, Unit target)
        {
            var item = AbilityId.item_mjollnir;



            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Position.Distance2D(target.Position) <= 350)
            {
                
                itemsHelper.CastItemTarget(unit, unit, item);
            }
        }
    }
}
