using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Game;

namespace Tee.ShadowFiend
{
    class TargetSelector
    {
        public static Unit ClosestToMouse(Unit MyHero)
        {

            return EntityManager
                .GetEntities<Hero>()
                .Where(x => !x.IsAlly(MyHero) && x.IsAlive && x.IsVisible && x.IsValid && !x.IsIllusion && x.Distance2D(GameManager.MousePosition) < 750)
                .OrderBy(x => x.Distance2D(GameManager.MousePosition))
                .FirstOrDefault();
        }
    }
}