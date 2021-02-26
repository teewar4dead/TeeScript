

using Divine;
using Divine.SDK.Extensions;

namespace TeeLoneDruid.Bear.Items
{
    class item_solar_crest
    {
        public item_solar_crest(Unit unit, Unit target)
        {
            var item = AbilityId.item_solar_crest;



            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Position.Distance2D(target.Position) <= 600)
            {
                itemsHelper.CastItemTarget(unit, target, item);
            }
        }
    }
}
