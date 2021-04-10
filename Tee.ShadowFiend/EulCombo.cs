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
    class EulCombo
    {
        public static Sleeper EulSleeper = new Sleeper();
        public static Sleeper BlinkSleeper = new Sleeper();
        public static Sleeper MoveSleeper = new Sleeper();
        public static Sleeper UltimateSleeper = new Sleeper();
        public static bool ActivateRaze = true;
        private static bool MoveMouse;

        public static void EulComboUpdate()
        {
            var PingGame = GameManager.Ping / 1000;

            if (MoveMouse)
            {
                if (GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) >= 50)
                {
                    GetSet.MyHero.Move(GetSet.Target.Position);
                }
                MoveMouse = false;
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
                if (GlobalMenu.UltGameChat.Value)
                {
                    GameManager.ExecuteCommand("Say ?");
                }
                return;
            }
            if ((GetSet.Blink != null && Helper.CanBeCasted(GetSet.Blink, GetSet.MyHero) && Helper.CanBeCasted(GetSet.Eul, GetSet.MyHero) && !BlinkSleeper.Sleeping) && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.Blink.AbilitySpecialData.FirstOrDefault(x => x.Name == "blink_range").Value + AbilityExtensions.GetCastRange(GetSet.Blink))
            {
                BlinkSleeper.Sleep(250);
                if (GetSet.Blink.Cast(GetSet.Target.Position))
                {
                    GetSet.MyHero.Move(GetSet.Target.Position);
                }
            }



            if ((GetSet.ArcaneBlink != null && Helper.CanBeCasted(GetSet.ArcaneBlink, GetSet.MyHero) && Helper.CanBeCasted(GetSet.Eul, GetSet.MyHero) && !BlinkSleeper.Sleeping) && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.ArcaneBlink.AbilitySpecialData.FirstOrDefault(x => x.Name == "blink_range").Value + AbilityExtensions.GetCastRange(GetSet.ArcaneBlink))
            {
                BlinkSleeper.Sleep(250);
                if (GetSet.ArcaneBlink.Cast(GetSet.Target.Position))
                {
                    GetSet.MyHero.Move(GetSet.Target.Position);
                }
            }

            var ModifierCyclone = Helper.FindModifier(GetSet.Target, "modifier_eul_cyclone");

            if (GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.MyHero.Speed && Helper.CanBeCasted(GetSet.Eul, GetSet.MyHero) && !EulSleeper.Sleeping)
            {
                if (Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target))
                {
                    MainCombo.DynamicItemCastForLinka(AbilityId.item_cyclone);
                }
                else
                {
                    EulSleeper.Sleep(250);
                    if (GetSet.Eul.Cast(GetSet.Target))
                    {
                        GetSet.MyHero.Move(GetSet.Target.Position);
                    }

                    var PhaseBoots = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_phase_boots);
                    var item_bkb = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_black_king_bar);
                    if (PhaseBoots != null && Helper.CanBeCasted(PhaseBoots, GetSet.MyHero))
                    {
                        PhaseBoots.Cast();
                    }
                    if (item_bkb != null && Helper.CanBeCasted(GetSet.item_bkb, GetSet.MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == GetSet.item_bkb.Id).Value && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= 600)
                    {
                        GetSet.item_bkb.Cast();
                    }
                    if (GlobalMenu.PauseGame.Value)
                    {
                        GameManager.ExecuteCommand("dota_pause");
                    }
                }

            }
          
            var ModifierArcaneBlink = Helper.FindModifier(GetSet.MyHero, "modifier_item_arcane_blink_buff");
            var CastTime = ModifierArcaneBlink != null ? (GetSet.Ultimate.CastPoint / 100) * 50 : GetSet.Ultimate.CastPoint;
            if (ModifierCyclone != null && GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= 150 && ModifierCyclone.RemainingTime <= (CastTime + PingGame) && !UltimateSleeper.Sleeping)
            {

                UltimateSleeper.Sleep(250);
                if (GetSet.Ultimate.Cast())
                {
                   
                }
            }
        }
    }
}
