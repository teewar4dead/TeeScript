using Divine;
using Divine.SDK.Extensions;


namespace TeeLoneDruid.Bear.Items
{
    class item_abyssal_blade
    {
        public item_abyssal_blade(Unit unit, Unit target)
        {
            var item = AbilityId.item_abyssal_blade;



            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Position.Distance2D(target.Position) <= 550)
            {
                itemsHelper.CastItemTarget(unit, target, item);
            }
        }
    }
}
