using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using Divine.SDK.Orbwalker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class SpellSF
    {
        public static Sleeper OrdSleeper1 = new Sleeper();
        public static Sleeper OrdSleeper2 = new Sleeper();
        public static Sleeper OrdSleeper3 = new Sleeper();
        public static Sleeper AttackSleep = new Sleeper();
        public static void SpellCombo()
        {

            if (GetSet.Target == null || !GetSet.Target.IsValid || !GetSet.Target.IsAlive)
            {
                GetSet.Target = TargetSelector.ClosestToMouse(GetSet.MyHero);
                return;
            }

            var spell1 = GetSet.MyHero.Spellbook.Spell1;
            var spell2 = GetSet.MyHero.Spellbook.Spell2;
            var spell3 = GetSet.MyHero.Spellbook.Spell3;

            var Rotation = GetSet.MyHero.RotationRad;
            var vector2Polar = SharpDXExtensions.FromPolarCoordinates(1f, Rotation);


            var Pos1 = GetSet.MyHero.Position + (vector2Polar.ToVector3() * 200);
            var Pos2 = GetSet.MyHero.Position + (vector2Polar.ToVector3() * 450);
            var Pos3 = GetSet.MyHero.Position + (vector2Polar.ToVector3() * 700);

            if (!GetSet.Target.IsAttackImmune() && !AttackSleep.Sleeping)
            {
                if (GlobalMenu.HitRun.Value)
                {
                    OrbwalkerManager.OrbwalkTo(GetSet.Target);
                }
                else
                {
                    GetSet.MyHero.Attack(GetSet.Target);
                }
                AttackSleep.Sleep(800);
            }
            if (Pos1.Distance2D(GetSet.Target.Position) <= 250 && Helper.CanBeCasted(spell1, GetSet.MyHero) && !OrdSleeper1.Sleeping)
            {
                
                spell1.Cast();
                OrdSleeper1.Sleep(670);
            }

            if (Pos2.Distance2D(GetSet.Target.Position) <= 250 && Helper.CanBeCasted(spell2, GetSet.MyHero) && !OrdSleeper2.Sleeping)
            {
                spell2.Cast();
                OrdSleeper2.Sleep(670);
            }
           
            if (Pos3.Distance2D(GetSet.Target.Position) <= 250 && Helper.CanBeCasted(spell3, GetSet.MyHero) && !OrdSleeper3.Sleeping)
            {
                spell3.Cast();
                OrdSleeper3.Sleep(670);
            }
           


            if (spell1.IsInAbilityPhase && Pos1.Distance2D(GetSet.Target.Position) >= 250)
            {
                GetSet.MyHero.Stop();
            }
            if (spell2.IsInAbilityPhase && Pos2.Distance2D(GetSet.Target.Position) >= 250)
            {
                GetSet.MyHero.Stop();
            }
            if (spell3.IsInAbilityPhase && Pos3.Distance2D(GetSet.Target.Position) >= 250)
            {
                GetSet.MyHero.Stop();
            }

        }
    }
}
