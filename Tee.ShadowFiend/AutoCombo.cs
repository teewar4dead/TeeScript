using Divine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class AutoCombo
    {
        public static void AutoComboUpdate()
        {
            if (GetSet.ArcaneBlink == null || !GetSet.ArcaneBlink.IsValid)
            {
                GetSet.ArcaneBlink = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_arcane_blink);
            }
            if (GetSet.Eul == null || !GetSet.Eul.IsValid)
            {
                GetSet.Eul = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_cyclone);
            }
            if (GetSet.Abyssal == null || !GetSet.Abyssal.IsValid)
            {
                GetSet.Abyssal = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_abyssal_blade);
            }


            if (GetSet.Abyssal != null && Helper.CanBeCasted(AbilityId.item_abyssal_blade, GetSet.MyHero))
            {
                UpdateManager.IngameUpdate += AbyssalCombo.AbyssalComboUpdate;
            }
            else if (GetSet.Eul != null && Helper.CanBeCasted(AbilityId.item_cyclone, GetSet.MyHero))
            {
                UpdateManager.IngameUpdate += EulCombo.EulComboUpdate;
            }
            else if (GetSet.ArcaneBlink != null && Helper.CanBeCasted(AbilityId.item_arcane_blink, GetSet.MyHero))
            {
                ArcaneBlinkCombo.ActivateBool = true;
                UpdateManager.IngameUpdate += ArcaneBlinkCombo.ArcaneBlinkComboUpdate;
            }



        }
    }
}
