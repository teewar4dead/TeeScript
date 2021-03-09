using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Divine.SDK.Orbwalker;

namespace Tee.ShadowFiend
{

    class ItemSF
    {
        private static Sleeper OrdSleeper = new Sleeper();
        private static bool MoveHero;
        public static void SFSpell()
        {
            
            if (OrdSleeper.Sleeping)
            {
                return;
            }

            if (!GetSet.MyHero.IsAlive || GetSet.MyHero == null || GetSet.MyHero.IsStunned())
            {
                return;
            }
            if (MoveHero)
            {
                GetSet.MyHero.Move(GameManager.MousePosition);
                MoveHero = false;
            }
            if (GetSet.Target == null || !GetSet.Target.IsValid || !GetSet.Target.IsAlive)
            {
                MoveHero = true;
                GetSet.Target = TargetSelector.ClosestToMouse(GetSet.MyHero);
                return;
            }
           

          

            OrdSleeper.Sleep(300);

            DynamicCombo.DynamicItem();










        }
        public static Creep GetCreepForMidasItem(Unit unitRangeFind)
        {
            return EntityManager.GetEntities<Creep>().FirstOrDefault(x => x.Distance2D(unitRangeFind.Position) <= 600 && x.IsSpawned && x.IsAlive && x.IsValid && x.IsVisible && !x.IsAncient && (!x.IsAlly(GetSet.MyHero) || x.IsNeutral || x.IsSummoned));
        }

    }
}
