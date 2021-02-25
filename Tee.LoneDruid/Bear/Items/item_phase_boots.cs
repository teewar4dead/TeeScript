using Divine;
using System;
using System.Linq;

namespace TeeLoneDruid.Bear.Items
{
    class item_phase_boots
    {
        public  item_phase_boots(Unit unit)
        {
            var item = AbilityId.item_phase_boots;


          
            

            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item)&& unit.IsMoving)
            {
                
                itemsHelper.CastItem(unit, item);
            }
        }
    }
}
