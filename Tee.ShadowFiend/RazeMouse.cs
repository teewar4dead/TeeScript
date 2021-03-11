using Divine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class RazeMouse
    {
        public static void RazeMouseUpdate(OrderAddingEventArgs e)
        {
            Ability order = e.Order.Ability;
            if (GetSet.MyHero.ClassId != ClassId.CDOTA_Unit_Hero_Nevermore || e.IsCustom) 
            { 
                return; 
            }
            if (e.Order.Type == OrderType.Cast &&  (order.Id == AbilityId.nevermore_shadowraze1 || order.Id == AbilityId.nevermore_shadowraze2 || order.Id == AbilityId.nevermore_shadowraze3))
            {
                GetSet.MyHero.MoveToDirection(GameManager.MousePosition);
                order.Cast();
                e.Process = true;
            }
        }
    }
}
