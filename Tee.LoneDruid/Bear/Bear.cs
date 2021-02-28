using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using Divine.SDK.Orbwalker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeeLoneDruid.Bear.Items;
using TeeLoneDruid.Bear.Items.Neutral;

namespace TeeLoneDruid
{
    class BearSpirit
    {
        private readonly Dictionary<Ability, float> CastTime = new Dictionary<Ability, float>();
        private readonly Sleeper SleeperOrder_1 = new Sleeper();
        private readonly Sleeper SleeperOrder_2 = new Sleeper();
        public static readonly Hero LoneHero = EntityManager.LocalHero;
        public static Unit BearHero;
        private Hero HeroTarget;
        private bool HeroTargetCheck;
        public BearSpirit()
        {
            if(LoneHero.HeroId == HeroId.npc_dota_hero_lone_druid)
            {
                UpdateManager.IngameUpdate += UpdateManager_AutoItems;
                MenuGlobal.OnOff.ValueChanged += OnOff_ValueChanged;
                InputManager.MouseKeyDown += InputManager_MouseKeyDown;
 
                
                
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

        private void InputManager_MouseKeyDown(MouseEventArgs e)
        {
            string select = LoneHero.Player.SelectedUnits.FirstOrDefault().ToString();
            if (BearConfig.AutoCombo.Value && select == "npc_dota_lone_druid_bear1")
            {
                HeroTarget = EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(EntityManager.LocalHero)
                   && x.IsAlive
                   && x.IsVisible
                   && x.IsValid
                   && !x.IsIllusion
                     && x.Distance2D(GameManager.MousePosition) < 800).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();

                try
                {
                    if(!HeroTarget.IsAlive)
                    {
                        UpdateManager.IngameUpdate -= BearComboUpdate;
                    }
                    if (e.MouseKey == MouseKey.Right && HeroTarget.Position.Distance2D(GameManager.MousePosition) < 140)
                    {
                        if (HeroTargetCheck)
                        {
                            UpdateManager.IngameUpdate += BearComboUpdate;
                        }

                        HeroTargetCheck = false;

                    }
                    else if(!BearConfig.BearCombo.Value)
                    {
                        HeroTarget = null;
                        UpdateManager.IngameUpdate -= BearComboUpdate;
                        HeroTargetCheck = true;
                    }
                }
                catch (Exception)
                {

                    //nothing
                }
            }
        }

        private bool find_blink(Hero hero)
        {
            var Item = hero.Inventory.Items.FirstOrDefault(x => x.Id == AbilityId.item_blink);
            bool BoolItem;

            if (Item != null)
            {
                BoolItem = true;
              
            }
            else
            {
                BoolItem = false;
            }

            return BoolItem;
        }
        

        private void UpdateManager_AutoItems()
        {
            BearHero = EntityManager.GetEntities<Unit>().FirstOrDefault(x => x.ClassId == ClassId.CDOTA_Unit_SpiritBear && x.IsAlive && x.IsControllable);
            if (SleeperOrder_1.Sleeping)
            {
                return;
            }
            if(BearHero == null)
            {
                return;
            }
            
            SleeperOrder_1.Sleep(150);
            
            
            new item_phase_boots(BearHero);

            new item_spider_legs(BearHero);

            new item_hand_of_midas(BearHero);

            new item_bullwhip(BearHero);

            if (BearConfig.VBE.Value)
            {
                if(BearHero.IsVisibleToEnemies && BearHero.IsAlive)
                {
                    ParticleManager.CreateOrUpdateParticle("VBE", "particles/items_fx/aura_shivas.vpcf", BearHero, ParticleAttachment.AbsOriginFollow, new ControlPoint(1, 255, 255, 255), new ControlPoint(2, 255));

                    
                }
                else
                {
                    ParticleManager.RemoveParticle("VBE");
                }
            }
            else
            {
                ParticleManager.RemoveParticle("VBE");
            }
        }

        
        private void BearComboUpdate()
        {

            if (SleeperOrder_2.Sleeping)
            {
                return;
            }

            SleeperOrder_2.Sleep(150);
            if (BearHero == null)
            {
                return;
            }
            if (!BearHero.IsAlive)
            {
                return;
            }
            
            

            if (HeroTarget == null)
            {
                HeroTarget = EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(EntityManager.LocalHero)
                    && x.IsAlive
                    && x.IsVisible
                    && x.IsValid
                    && !x.IsIllusion
                    && x.Distance2D(GameManager.MousePosition) < 800).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();
                BearHero.Move(GameManager.MousePosition);
            }
            if (BearConfig.HitRun.Value)
            {
                BearHero.Attack(HeroTarget);
            }
            else
            {
                BearHero.Attack(HeroTarget);
            }

            try
            {
                new item_orchid(BearHero, HeroTarget);

                new item_bloodthorn(BearHero, HeroTarget);

                new item_mjollnir(BearHero, HeroTarget);

                new item_mask_of_madness(BearHero, HeroTarget);

                new item_abyssal_blade(BearHero, HeroTarget);

                new item_nullifier(BearHero, HeroTarget);

                new item_medallion_of_courage(BearHero, HeroTarget);

                new item_solar_crest(BearHero, HeroTarget);

                new item_satanic(BearHero);

            }
            catch (Exception)
            {

                //nothing
            }


        }

        private void BearCombo_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {

                UpdateManager.IngameUpdate += BearComboUpdate;
            }
            else
            {
                if (BearHero != null)
                {
                    BearHero.Follow(LoneHero);
                }
                
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
