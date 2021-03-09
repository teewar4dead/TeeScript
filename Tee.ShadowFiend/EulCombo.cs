﻿using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class EulCombo
    {
        public static Sleeper EulSleeper = new Sleeper();
        public static Sleeper BlinkSleeper = new Sleeper();
        public static Sleeper MoveSleeper = new Sleeper();
        public static Sleeper UltimateSleeper = new Sleeper();
        private static bool MoveMouse;

        public static void EulComboUpdate()
        {
            var PingGame = GameManager.Ping / 1000;
            if (GameManager.IsPaused)
            {
                return;
            }
            if (!Helper.CanBeCasted(GetSet.Ultimate, GetSet.MyHero))
            {
                return;
            }
            if (MoveMouse)
            {
                GetSet.MyHero.Move(GameManager.MousePosition);
                MoveMouse = false;
            }

            if (GetSet.Target == null || !GetSet.Target.IsValid || !GetSet.Target.IsAlive)
            {
                GetSet.Target = TargetSelector.ClosestToMouse(GetSet.MyHero);
                return;
            }

            if (GetSet.Eul == null || !GetSet.Eul.IsValid)
            {
                MoveMouse = true;
                GetSet.Eul = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_cyclone);
                return;
            }

            if (GetSet.Blink == null || !GetSet.Blink.IsValid)
            {
                GetSet.Blink = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_blink);
            }
            if (GetSet.ArcaneBlink == null || !GetSet.ArcaneBlink.IsValid)
            {
                GetSet.ArcaneBlink = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_arcane_blink);
            }
            if (GetSet.Ultimate.IsInAbilityPhase)
            {
                return;
            }
            if ((GetSet.Blink != null && Helper.CanBeCasted(GetSet.Blink, GetSet.MyHero) && Helper.CanBeCasted(GetSet.Eul, GetSet.MyHero) && !BlinkSleeper.Sleeping) && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.Blink.AbilitySpecialData.FirstOrDefault(x => x.Name == "blink_range").Value + AbilityExtensions.GetCastRange(GetSet.Blink))
            {
                BlinkSleeper.Sleep(250);
                GetSet.Blink.Cast(GetSet.Target.Position);
            }



            if ((GetSet.ArcaneBlink != null && Helper.CanBeCasted(GetSet.ArcaneBlink, GetSet.MyHero) && Helper.CanBeCasted(GetSet.Eul, GetSet.MyHero) && !BlinkSleeper.Sleeping) && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.ArcaneBlink.AbilitySpecialData.FirstOrDefault(x => x.Name == "blink_range").Value + AbilityExtensions.GetCastRange(GetSet.ArcaneBlink))
            {
                BlinkSleeper.Sleep(250);
                GetSet.ArcaneBlink.Cast(GetSet.Target.Position);
            }


            if (GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) >= GetSet.MyHero.Speed)
            {
                GetSet.MyHero.Move(GetSet.Target.Position);
            }
            if (GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.MyHero.Speed && Helper.CanBeCasted(GetSet.Eul, GetSet.MyHero) && !EulSleeper.Sleeping)
            {
                if (Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target))
                {
                    DynamicCombo.DynamicItemCastForLinka(Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target), AbilityId.item_cyclone);
                }
                else
                {
                    EulSleeper.Sleep(250);
                    GetSet.Eul.Cast(GetSet.Target);
                    var PhaseBoots = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_phase_boots);
                    if (PhaseBoots != null)
                    {
                        PhaseBoots.Cast();
                    }
                    GetSet.MyHero.Move(GetSet.Target.Position);
                }

            }
            var ModifierCyclone = Helper.FindModifier(GetSet.Target, "modifier_eul_cyclone");
            var ModifierArcaneBlink = Helper.FindModifier(GetSet.MyHero, "modifier_item_arcane_blink_buff");
            var CastTime = ModifierArcaneBlink != null ? (GetSet.Ultimate.CastPoint / 100) * 50 : GetSet.Ultimate.CastPoint;
            if (ModifierCyclone != null && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= 80 && ModifierCyclone.RemainingTime <= (CastTime - PingGame) && !UltimateSleeper.Sleeping)
            {
               
                GetSet.MyHero.Move(GetSet.Target.Position);
                UltimateSleeper.Sleep(250);
                if (GetSet.Ultimate.Cast())
                {
                   
                }
            }
        }
    }
}