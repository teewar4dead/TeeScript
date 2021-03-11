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
        public static bool ActivateRaze = true;

        public static void AbyssalComboUpdate()
        {
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


            if (GetSet.Abyssal == null || !GetSet.Abyssal.IsValid)
            {
                GetSet.Abyssal = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_abyssal_blade);
                return;
            }

            if (GetSet.Ultimate.IsInAbilityPhase)
            {
                return;
            }


            MainCombo.DynamicItemCastForLinka(AbilityId.item_ethereal_blade);



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
                var Sheepstick = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_sheepstick);
                var Orchid = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_orchid);
                var item_bloodthorn = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_bloodthorn);

                if (Ethereal != null && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == Ethereal.Id).Value && Helper.CanBeCasted(Ethereal, GetSet.MyHero))
                {
                    Ethereal.Cast(GetSet.Target);
                }
                else if (Orchid != null && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == Orchid.Id).Value && Helper.CanBeCasted(Orchid, GetSet.MyHero))
                {
                    Orchid.Cast(GetSet.Target);
                }
                else if (item_bloodthorn != null && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_bloodthorn.Id).Value && Helper.CanBeCasted(item_bloodthorn, GetSet.MyHero))
                {
                    item_bloodthorn.Cast(GetSet.Target);
                }
                else
                {
                    GetSet.Ultimate.Cast();
                }
            }

        }
    }
}
