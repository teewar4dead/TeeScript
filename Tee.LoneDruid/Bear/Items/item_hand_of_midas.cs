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
            
            var CreepRange = EntityManager.GetEntities<Creep>().FirstOrDefault(x => x.Distance2D(unit.Position) <= 600 && x.IsSpawned && x.IsAlive && x.IsValid && x.IsVisible && !x.IsAncient && (!x.IsAlly(BearSpirit.LoneHero) || x.IsNeutral || x.IsSummoned));
           


            if (BearConfig.AbilityToggler.GetValue(item) && itemsHelper.FindItem(unit, item) )
            {
                itemsHelper.CastItemTarget(unit, CreepRange, item);
            }
        }
    }
}
