using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using Divine.SDK.Orbwalker;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeeLoneDruid.Bear;
using TeeLoneDruid.Bear.Items;
using TeeLoneDruid.Bear.Items.Neutral;

namespace TeeLoneDruid
{
    class BearSpirit : GetEntity
    {
        private readonly Dictionary<Ability, float> CastTime = new Dictionary<Ability, float>();
        private readonly Sleeper SleeperOrder_1 = new Sleeper();
        private readonly Sleeper SleeperOrder_2 = new Sleeper();
        private bool HeroTargetCheck;

        public BearSpirit()
        {
            if(LocalDruidHero.HeroId == HeroId.npc_dota_hero_lone_druid)
            {
                UpdateManager.IngameUpdate += UpdateManager_AutoItems;
                MenuGlobal.OnOff.ValueChanged += OnOff_ValueChanged;
                InputManager.MouseKeyDown += InputManager_MouseKeyDown;
 
                
                
                if (BearConfig.SummonBear.Value && LocalDruidHero.Level == 1)
                {
                    LocalDruidHero.Spellbook.Spell1.Upgrade();
                    LocalDruidHero.Spellbook.Spell1.Cast();
                }
            }
            else
            {
                //nothing
            }
        }

        private void InputManager_MouseKeyDown(MouseEventArgs e)
        {

            
            
            if (BearConfig.AutoCombo.Value && (GetSelectDruidAndBear().Count() == 1 || GetSelectDruidAndBear().Count() == 2))
            {
                GetHeroTarget();

                try
                {
                    if(HeroTarget == null)
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



        [Obsolete]
        private void UpdateManager_AutoItems()
        {
            GetBearHero();

            if (SleeperOrder_1.Sleeping)
            {
                return;
            }
            if (BearHero == null)
            {
                return;
            }

            SleeperOrder_1.Sleep(150);


            new item_phase_boots(BearHero);

            new item_spider_legs(BearHero);

            new item_hand_of_midas(BearHero);

            new item_bullwhip(BearHero);

            new VisibleByEnemy();

            new CourierDeliver();

            RendererManager.Draw += RendererManager_Draw;
        }


        private static readonly float scaling = RendererManager.Scaling;
        private void RendererManager_Draw()
        {
            if (CourierGiveItem == true)
                RendererManager.DrawTexture(AbilityId.lone_druid_spirit_bear, new RectangleF(RendererManager.ScreenSize.X - (83 * scaling), RendererManager.ScreenSize.Y - (90 * scaling), 60 * scaling, 60 * scaling), AbilityTextureType.Round, true);

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
           


            if (HeroTarget == null || !HeroTarget.IsAlive)
            {
                GetHeroTarget();
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
                    BearHero.Follow(LocalDruidHero);
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
