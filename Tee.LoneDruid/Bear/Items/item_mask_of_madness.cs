using Divine;
using Divine.SDK.Extensions;


namespace TeeLoneDruid.Bear.Items
{
    class item_mask_of_madness
    {
        public item_mask_of_madness(Unit unit, Unit target)
        {
            var item = AbilityId.item_mask_of_madness;




            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Position.Distance2D(target.Position) <= 600)
            {
                itemsHelper.CastItem(unit, item);
            }
        }
    }
}
