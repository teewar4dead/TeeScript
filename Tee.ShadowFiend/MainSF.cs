using System;
using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Numerics;
using Divine.Order;
using Divine.Particle;
using Divine.Particle.Components;
using Divine.Renderer;
using Divine.Update;

namespace Tee.ShadowFiend
{
    class MainSF
    {
        public MainSF()
        {
            GlobalMenu.OnOff.ValueChanged += OnOff_ValueChanged;

        }

        private void DisponseDraw()
        {
            foreach (var Rad in DrawRadiusRaze.NeverR)
            {
                ParticleManager.RemoveParticle($"DrawRaze_{Rad}");
            }
            foreach (var hero in EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(GetSet.MyHero) && x.IsVisible && x.IsValid && x.IsAlive))
            {
                ParticleManager.RemoveParticle($"ShowLinka{hero.Handle}");
            }
        }

        private void OnOff_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {

            if (e.Value)
            {
                GlobalMenu.AutoRazeKey.ValueChanged += AutoRazeKey_ValueChanged;
                GlobalMenu.LinkaShow.ValueChanged += LinkaShow_ValueChanged;
                GlobalMenu.MainCombo.ValueChanged += MainCombo_ValueChanged; ;
                GlobalMenu.AutoRazeMouseKey.ValueChanged += AutoRazeMouseKey_ValueChanged;
                GlobalMenu.ShowRadiusRaze.ValueChanged += ShowRadiusRaze_ValueChanged;
                GlobalMenu.UltComboKey.ValueChanged += UltComboKey_ValueChanged;
            }
            else
            {
                GlobalMenu.AutoRazeKey.ValueChanged -= AutoRazeKey_ValueChanged;
                GlobalMenu.LinkaShow.ValueChanged -= LinkaShow_ValueChanged;
                GlobalMenu.MainCombo.ValueChanged -= MainCombo_ValueChanged; ;
                GlobalMenu.AutoRazeMouseKey.ValueChanged -= AutoRazeMouseKey_ValueChanged;
                GlobalMenu.ShowRadiusRaze.ValueChanged -= ShowRadiusRaze_ValueChanged;
                GlobalMenu.UltComboKey.ValueChanged -= UltComboKey_ValueChanged;
                OrderManager.OrderAdding -= RazeMouse.RazeMouseUpdate;
                GetSet.Target = null;
                RendererManager.Draw -= DrawRadiusRaze.DrawRadiusRazeOnDraw;
                DisponseDraw();

            }

        }

        private void AutoRazeKey_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += MainCombo.DynamicItem;
            }
            else
            {
                UpdateManager.IngameUpdate -= MainCombo.DynamicItem;
            }
        }

        private void LinkaShow_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += UpdateManager_LinkaShowDrawParticle;
            }
            else
            {
                DisponseDraw();
                UpdateManager.IngameUpdate -= UpdateManager_LinkaShowDrawParticle;
            }
        }

        private void UpdateManager_LinkaShowDrawParticle()
        {
            try
            {
                foreach (var hero in EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(GetSet.MyHero) && x.IsVisible && x.IsValid && x.IsAlive))
                {
                    var Linka = Helper.FindItemMain(hero, AbilityId.item_sphere);
                    if (Linka == null)
                    {
                        ParticleManager.RemoveParticle($"ShowLinka{hero.Handle}");
                    }
                    if (Linka != null)
                    {
                        if (Helper.CanBeCasted(Linka, hero))
                        {
                            ParticleManager.CreateOrUpdateParticle(
                       $"ShowLinka{hero.Handle}",
                       "particles/items_fx/immunity_sphere_buff.vpcf",
                       GetSet.MyHero,
                       ParticleAttachment.AbsOrigin,
                       new ControlPoint(0, hero.Position + new Vector3(0, 0, 148)),
                       new ControlPoint(1, 255),
                       new ControlPoint(2, 0, 0, 0));
                        }
                        else
                        {
                            ParticleManager.RemoveParticle($"ShowLinka{hero.Handle}");
                        }

                    }
                    else
                    {
                        ParticleManager.RemoveParticle($"ShowLinka{hero.Handle}");
                    }
                }
            }
            catch (Exception)
            {

            }
           

        }

        private void MainCombo_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {

                UpdateManager.IngameUpdate += MainCombo.DynamicItem;

            }
            else
            {
                UpdateManager.IngameUpdate -= MainCombo.DynamicItem;
                MainCombo.EndDynamicItem();
            }
        }

        private void AutoRazeMouseKey_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                OrderManager.OrderAdding += RazeMouse.RazeMouseUpdate;
            }
            else
            {
                OrderManager.OrderAdding -= RazeMouse.RazeMouseUpdate;
            }
        }

        private void ShowRadiusRaze_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                RendererManager.Draw += DrawRadiusRaze.DrawRadiusRazeOnDraw;
            }
            else
            {
                DisponseDraw();

                RendererManager.Draw -= DrawRadiusRaze.DrawRadiusRazeOnDraw;
            }
        }

        private void UltComboKey_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += EulCombo.EulComboUpdate;
            }
            else
            {

                EulCombo.ActivateRaze = true;
                MainCombo.EndDynamicItem();
                UpdateManager.IngameUpdate -= MainCombo.DynamicItem;
                UpdateManager.IngameUpdate -= EulCombo.EulComboUpdate;
                GetSet.Target = null;
            }
        }

    }
}
