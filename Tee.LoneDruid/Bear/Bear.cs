using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using Divine.SDK.Orbwalker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeeLoneDruid.Bear.Items;

namespace TeeLoneDruid
{
    class BearSpirit
    {
        private readonly Sleeper SleeperOrder_1 = new Sleeper();
        private readonly Sleeper SleeperOrder_2 = new Sleeper();
        public static readonly Hero LoneHero = EntityManager.LocalHero;
        public static Unit BearHero;
        private Hero HeroTarget;
        public BearSpirit()
        {
            if(LoneHero.HeroId == HeroId.npc_dota_hero_lone_druid)
            {
                UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
                MenuGlobal.OnOff.ValueChanged += OnOff_ValueChanged;
                

                if (BearConfig.SummonBear.Value && LoneHero.Level == 1)
                {
                    LoneHero.Spellbook.Spell1.Upgrade();
                    LoneHero.Spellbook.Spell1.Cast();
                }
            }
            else
            {
                //nothing
            }
        }

        private void UpdateManager_IngameUpdate()
        {
            if (SleeperOrder_1.Sleeping)
            {
                return;
            }

            SleeperOrder_1.Sleep(100);
            BearHero = EntityManager.GetEntities<Unit>().FirstOrDefault(x => x.ClassId == ClassId.CDOTA_Unit_SpiritBear && x.IsAlive && x.IsControllable);

            new item_phase_boots(BearHero);

            new item_spider_legs(BearHero);

            new item_hand_of_midas(BearHero);



           
        }

        
        private void BearComboUpdate()
        {

            if (SleeperOrder_2.Sleeping)
            {
                return;
            }

            SleeperOrder_2.Sleep(100);
            if (BearHero == null)
            {
                Console.WriteLine("Нет Медведя");
                return;
            }
            if (!BearHero.IsAlive)
            {
                Console.WriteLine("Мертв");
                return;
            }
            
            

            if (HeroTarget == null)
            {
                Console.WriteLine("Нет Цели");
                HeroTarget = EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(EntityManager.LocalHero)
             && x.IsAlive
             && x.IsVisible
             && x.IsValid
             && !x.IsIllusion
             && x.Distance2D(GameManager.MousePosition) < 800).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();
            }
            if (BearConfig.HitRun.Value)
            {
                BearHero.Attack(HeroTarget);
            }
            else
            {
                BearHero.Attack(HeroTarget);
            }

            new item_orchid(BearHero, HeroTarget);

            new item_bloodthorn(BearHero, HeroTarget);

            new item_mjollnir(BearHero, HeroTarget);

            new item_mask_of_madness(BearHero, HeroTarget);

            new item_abyssal_blade(BearHero, HeroTarget);

            new item_nullifier(BearHero, HeroTarget);

            new item_medallion_of_courage(BearHero, HeroTarget);

            new item_solar_crest(BearHero, HeroTarget);

        }

        private void BearCombo_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += BearComboUpdate;
            }
            else
            {
                HeroTarget = null;
                UpdateManager.IngameUpdate -= BearComboUpdate;
            }
        }




        private void OnOff_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                BearConfig.BearCombo.ValueChanged += BearCombo_ValueChanged;
            }
            else
            {
                BearConfig.BearCombo.ValueChanged -= BearCombo_ValueChanged;
            }
        }
    }
}
