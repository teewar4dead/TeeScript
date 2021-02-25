using Divine;
using Divine.SDK.Extensions;


namespace TeeLoneDruid.Bear.Items
{
    class item_nullifier
    {
        public item_nullifier(Unit unit, Unit target)
        {
            var item = AbilityId.item_nullifier;



            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Position.Distance2D(target.Position) <= 550)
            {
                itemsHelper.CastItemTarget(unit, target, item);
            }
        }
    }
}
