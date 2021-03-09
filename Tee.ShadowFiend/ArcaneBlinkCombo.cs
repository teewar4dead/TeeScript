using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using Divine.SDK.Prediction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class ArcaneBlinkCombo
    {
        public static Sleeper SleeperLinka = new Sleeper();
        public static Sleeper BlinkSleeper = new Sleeper();
        public static Sleeper MoveSleeper = new Sleeper();
        public static Sleeper UltimateSleeper = new Sleeper();
        private static Sleeper OrdSleeper = new Sleeper();
        public static bool ActivateBool;
        private ArcaneBlinkCombo()
        {

        }
        public static void ArcaneBlinkComboUpdate()
        {

            var PingGame = GameManager.Ping / 1000;
            if (GameManager.IsPaused)
            {
                return;
            }
            var ModifierArcaneBlink = Helper.FindModifier(GetSet.MyHero, "modifier_item_arcane_blink_buff");
            if (!Helper.CanBeCasted(GetSet.Ultimate, GetSet.MyHero))
            {
                return;
            }
            if (GetSet.Ultimate.IsInAbilityPhase)
            {
                return;
            }
            if (GetSet.Target == null || !GetSet.Target.IsValid || !GetSet.Target.IsAlive)
            {

                GetSet.Target = TargetSelector.ClosestToMouse(GetSet.MyHero);
                return;
            }

            if (GetSet.ArcaneBlink == null || !GetSet.ArcaneBlink.IsValid)
            {
                GetSet.ArcaneBlink = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_arcane_blink);
                return;
            }

            if ((GetSet.ArcaneBlink != null && Helper.CanBeCasted(GetSet.ArcaneBlink, GetSet.MyHero) && Helper.CanBeCasted(GetSet.ArcaneBlink, GetSet.MyHero) && !BlinkSleeper.Sleeping) && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.ArcaneBlink.AbilitySpecialData.FirstOrDefault(x => x.Name == "blink_range").Value + AbilityExtensions.GetCastRange(GetSet.ArcaneBlink))
            {

                if (GetSet.ArcaneBlink.Cast(Helper.GetPredictedPosition(GetSet.Target, (GetSet.Ultimate.CastPoint / 100) * 60 + PingGame)))
                {
                    BlinkSleeper.Sleep(600);
                }

            }
            if (!UltimateSleeper.Sleeping)
            {
               
                DynamicCombo.DynamicItemCastForLinka(Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target), AbilityId.item_ethereal_blade);

                OrdSleeper.Sleep(100);
                GetSet.Ultimate.Cast();




                UltimateSleeper.Sleep(100);
            }

        }
    }
}


