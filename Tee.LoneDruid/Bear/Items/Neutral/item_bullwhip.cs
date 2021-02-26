using Divine;
using Divine.SDK.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeeLoneDruid.Bear.Items.Neutral
{
    class item_bullwhip
    {
        public item_bullwhip(Unit unit)
        {
            var item = AbilityId.item_bullwhip;



            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) && unit.IsMoving)
            {

                itemsHelper.CastItemTarget(unit, unit, item);
            }
        }
    }
}
