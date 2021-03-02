using Divine;
using Divine.SDK.Extensions;
using System;
using System.Linq;

namespace TeeLoneDruid.Bear.Items
{
    class item_hand_of_midas
    {
        public item_hand_of_midas(Unit unit)
        {
            var item = AbilityId.item_hand_of_midas;
            
            


            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) )
            {
                itemsHelper.CastItemTarget(unit, GetEntity.GetCreepForMidasItem(unit), item);
            }
        }
    }
}
