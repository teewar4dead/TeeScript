using Divine;
using Divine.SDK.Extensions;


namespace TeeLoneDruid.Bear.Items
{
    class item_medallion_of_courage
    {
        public item_medallion_of_courage(Unit unit, Unit target)
        {
            var item = AbilityId.item_medallion_of_courage;



            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Position.Distance2D(target.Position) <= 600)
            {
                itemsHelper.CastItemTarget(unit, target, item);
            }
        }
    }
}
