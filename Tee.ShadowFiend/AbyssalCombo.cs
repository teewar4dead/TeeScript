using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class AbyssalCombo
    {
        public static Sleeper EulSleeper = new Sleeper();
        public static Sleeper BlinkSleeper = new Sleeper();
        public static Sleeper MoveSleeper = new Sleeper();
        public static Sleeper UltimateSleeper = new Sleeper();
        private static bool MoveMouse;

        public static void AbyssalComboUpdate()
        {
            if (GetSet.Target == null || !GetSet.Target.IsValid || !GetSet.Target.IsAlive)
            {
                GetSet.Target = TargetSelector.ClosestToMouse(GetSet.MyHero);
                return;
            }

            if (!Helper.CanBeCasted(GetSet.Ultimate, GetSet.MyHero))
            {
                return;
            }


            if (GetSet.Abyssal == null || !GetSet.Abyssal.IsValid)
            {
                GetSet.Abyssal = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_abyssal_blade);
                return;
            }

            if (GetSet.Ultimate.IsInAbilityPhase)
            {
                return;
            }


                DynamicCombo.DynamicItemCastForLinka(Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target), AbilityId.item_ethereal_blade);



            if (Helper.CanBeCasted(GetSet.Abyssal, GetSet.MyHero) && !EulSleeper.Sleeping && !Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target))
            {

                EulSleeper.Sleep(250);
                GetSet.Abyssal.Cast(GetSet.Target);
            }
            var ModifierCyclone = Helper.FindModifier(GetSet.Target, "modifier_bashed");
            if (ModifierCyclone != null && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= 80 && !UltimateSleeper.Sleeping)
            {

                UltimateSleeper.Sleep(250);
                var Ethereal = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_ethereal_blade);
                if (Ethereal != null)
                {
                    Ethereal.Cast(GetSet.Target);
                    if (GetSet.Ultimate.Cast())
                    {
                        DynamicCombo.DynamicItem();
                    }
                }
                else
                {
                    if (GetSet.Ultimate.Cast())
                    {
                        DynamicCombo.DynamicItem();
                    }
                }
            }
        }
    }
}
