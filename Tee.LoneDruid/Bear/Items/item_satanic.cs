using Divine;


namespace TeeLoneDruid.Bear.Items
{
    class item_satanic
    {

        public item_satanic(Unit unit)
        {
            var item = AbilityId.item_satanic;
            var BearHpPrc = (unit.MaximumHealth / 100) * 25;

            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.Health <= BearHpPrc)
            {

                itemsHelper.CastItem(unit, item);
            }
        }

    }
}
