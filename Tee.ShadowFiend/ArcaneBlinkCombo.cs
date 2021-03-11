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
        public static bool ActivateRaze = true;
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

            if (GetSet.Ultimate.IsInAbilityPhase)
            {
                return;
            }
            if (GetSet.Target == null || !GetSet.Target.IsValid || !GetSet.Target.IsAlive)
            {

                GetSet.Target = TargetSelector.ClosestToMouse(GetSet.MyHero);
                return;
            }
            var ModifierRequiemSlow = Helper.FindModifier(GetSet.Target, "modifier_nevermore_requiem_slow");
            if (ModifierRequiemSlow != null && ActivateRaze)
            {
                UpdateManager.IngameUpdate += MainCombo.DynamicItem;
                ActivateRaze = false;
            }
            if (!Helper.CanBeCasted(GetSet.Ultimate, GetSet.MyHero))
            {
                return;
            }
            if (GetSet.ArcaneBlink == null || !GetSet.ArcaneBlink.IsValid)
            {
                GetSet.ArcaneBlink = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_arcane_blink);
                return;
            }

            if ((GetSet.ArcaneBlink != null && Helper.CanBeCasted(GetSet.ArcaneBlink, GetSet.MyHero) && Helper.CanBeCasted(GetSet.ArcaneBlink, GetSet.MyHero) && !BlinkSleeper.Sleeping) && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.ArcaneBlink.AbilitySpecialData.FirstOrDefault(x => x.Name == "blink_range").Value + AbilityExtensions.GetCastRange(GetSet.ArcaneBlink))
            {

                if (GetSet.ArcaneBlink.Cast(Helper.GetPredictedPosition(GetSet.Target, (GetSet.Ultimate.CastPoint / 100) * 50 + PingGame)))
                {
                    BlinkSleeper.Sleep(600);
                }

            }
            var Ethereal = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_ethereal_blade);
            var Sheepstick = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_sheepstick);
            var Orchid = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_orchid);
            var item_bloodthorn = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_bloodthorn);

          

            if (!UltimateSleeper.Sleeping)
            {


                MainCombo.DynamicItemCastForLinka(AbilityId.item_ethereal_blade);

                OrdSleeper.Sleep(100);
                if (ModifierArcaneBlink != null && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= 500)
                {
                    if (Ethereal != null && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == Ethereal.Id).Value && Helper.CanBeCasted(Ethereal, GetSet.MyHero))
                    {
                        OrdSleeper.Sleep(10);
                        Ethereal.Cast(GetSet.Target);
                    }
                    else if (Sheepstick != null && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == Sheepstick.Id).Value && Helper.CanBeCasted(Sheepstick, GetSet.MyHero))
                    {
                        OrdSleeper.Sleep(10);
                        Sheepstick.Cast(GetSet.Target);
                    }
                    else if (Orchid != null && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == Orchid.Id).Value && Helper.CanBeCasted(Orchid, GetSet.MyHero))
                    {
                        OrdSleeper.Sleep(10);
                        Orchid.Cast(GetSet.Target);
                    }
                    else if (item_bloodthorn != null && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_bloodthorn.Id).Value && Helper.CanBeCasted(item_bloodthorn, GetSet.MyHero))
                    {
                        OrdSleeper.Sleep(10);
                        item_bloodthorn.Cast(GetSet.Target);
                    }
                    else
                    {
                        GetSet.Ultimate.Cast();
                    }
                   
                }





                UltimateSleeper.Sleep(150);
            }

        }
    }
}


