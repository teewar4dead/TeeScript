using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Creeps;
using Divine.Extensions;
using Divine.Game;
using Divine.Helpers;

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