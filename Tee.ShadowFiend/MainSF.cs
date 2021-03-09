using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Divine;
using System.Threading.Tasks;
using Divine.SDK.Helpers;
using Divine.SDK.Extensions;
using SharpDX;

namespace Tee.ShadowFiend
{
    class MainSF
    {


        public MainSF()
        {
            PanelClick.SelectItem4 = true;
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
                InputManager.MouseKeyDown += PanelClick.OnMouseKeyDown;
                GlobalMenu.LinkaShow.ValueChanged += LinkaShow_ValueChanged;
                GlobalMenu.MainCombo.ValueChanged += MainCombo_ValueChanged; ;
                GlobalMenu.AutoRazeMouseKey.ValueChanged += AutoRazeMouseKey_ValueChanged;
                GlobalMenu.ShowRadiusRaze.ValueChanged += ShowRadiusRaze_ValueChanged;
                GlobalMenu.UltComboKey.ValueChanged += UltComboKey_ValueChanged;
                RendererManager.Draw += ComboMenuThisDraw;
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
                UpdateManager.IngameUpdate -= ArcaneBlinkCombo.ArcaneBlinkComboUpdate;
                RendererManager.Draw -= DrawRadiusRaze.DrawRadiusRazeOnDraw;
                DisponseDraw();
                RendererManager.Draw -= ComboMenuThisDraw;

            }

        }

        private void AutoRazeKey_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += SpellSF.SpellCombo;
            }
            else
            {
                UpdateManager.IngameUpdate -= SpellSF.SpellCombo;
            }
        }

        private void ComboMenuThisDraw()
        {
            var MovePanel = new Vector2((RendererManager.ScreenSize.X / 100) * 50 + GlobalMenu.VectorX, (RendererManager.ScreenSize.Y / 100) * 50 + GlobalMenu.VectorY);
            var scaling = RendererManager.Scaling;
            var combo = new[] { AbilityId.item_cyclone, AbilityId.item_abyssal_blade, AbilityId.item_arcane_blink };
            var rect = new RectangleF(MovePanel.X, MovePanel.Y, ((4 * 82) + 2) * scaling, 88 * scaling);
            var rectMenuBorder = new RectangleF(rect.X, rect.Y - (20 * scaling), ((4 * 82) + 2) * scaling, 20 * scaling);
            RendererManager.DrawFilledRectangle(rect, Color.Indigo, Color.Black, 1);
            RendererManager.DrawFilledRectangle(rectMenuBorder, Color.Indigo, Color.Black, 1);
            RendererManager.DrawText("Select Combo", rectMenuBorder, Color.Green, "Arial", (FontFlags.Center | FontFlags.VerticalCenter), 20 * scaling);


            Color colorBorder1 = PanelClick.SelectItem1 ? Color.Green : Color.Red;
            Color colorBorder2 = PanelClick.SelectItem2 ? Color.Green : Color.Red;
            Color colorBorder3 = PanelClick.SelectItem3 ? Color.Green : Color.Red;
            Color colorBorder4 = PanelClick.SelectItem4 ? Color.Green : Color.Red;
            var Opacity1 = PanelClick.SelectItem1 ? 1 : 0.5f;
            var Opacity2 = PanelClick.SelectItem2 ? 1 : 0.5f;
            var Opacity3 = PanelClick.SelectItem3 ? 1 : 0.5f;



            var item1 = new RectangleF((rect.X + (4 * scaling)) + 0 * (82 * scaling), rect.Y + (6 * scaling), 76 * scaling, 76 * scaling);
            var item2 = new RectangleF((rect.X + (4 * scaling)) + 1 * (82 * scaling), rect.Y + (6 * scaling), 76 * scaling, 76 * scaling);
            var item3 = new RectangleF((rect.X + (4 * scaling)) + 2 * (82 * scaling), rect.Y + (6 * scaling), 76 * scaling, 76 * scaling);
            var item4 = new RectangleF((rect.X + (4 * scaling)) + 3 * (82 * scaling), rect.Y + (6 * scaling), 76 * scaling, 76 * scaling);
            RendererManager.DrawTexture(combo[0], item1, true, Opacity1);
            RendererManager.DrawTexture(combo[1], item2, true, Opacity2);
            RendererManager.DrawTexture(combo[2], item3, true, Opacity3);
            RendererManager.DrawText("A", item4, Color.Green, "Arial", (FontFlags.Center | FontFlags.VerticalCenter), 46 * scaling);
            RendererManager.DrawRectangle(item1, colorBorder1, 1);
            RendererManager.DrawRectangle(item2, colorBorder2, 1);
            RendererManager.DrawRectangle(item3, colorBorder3, 1);
            RendererManager.DrawRectangle(item4, colorBorder4, 1);

            PanelClick.item1(new Vector2(item1.X, item1.Y), new Vector2(item1.Width, item1.Height));
            PanelClick.item2(new Vector2(item2.X, item2.Y), new Vector2(item2.Width, item2.Height));
            PanelClick.item3(new Vector2(item3.X, item3.Y), new Vector2(item3.Width, item3.Height));
            PanelClick.item4(new Vector2(item4.X, item4.Y), new Vector2(item4.Width, item4.Height));
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

        private void MainCombo_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += SpellSF.SpellCombo;

                UpdateManager.IngameUpdate += ItemSF.SFSpell;
                
            }
            else
            {
                UpdateManager.IngameUpdate -= SpellSF.SpellCombo;
                UpdateManager.IngameUpdate -= ItemSF.SFSpell;
                DynamicCombo.EndDynamicItem();
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
               

                if (PanelClick.SelectItem1)
                {
                    UpdateManager.IngameUpdate += EulCombo.EulComboUpdate;
                }
                if (PanelClick.SelectItem2)
                {
                    
                    UpdateManager.IngameUpdate += AbyssalCombo.AbyssalComboUpdate;
                }
                if (PanelClick.SelectItem3)
                {
                    ArcaneBlinkCombo.ActivateBool = true;
                    UpdateManager.IngameUpdate += ArcaneBlinkCombo.ArcaneBlinkComboUpdate;
                }
                if (PanelClick.SelectItem4)
                {
                    AutoCombo.AutoComboUpdate();
                }

            }
            else
            {
                DynamicCombo.EndDynamicItem();
                UpdateManager.IngameUpdate -= EulCombo.EulComboUpdate;
                UpdateManager.IngameUpdate -= AbyssalCombo.AbyssalComboUpdate;
                UpdateManager.IngameUpdate -= ArcaneBlinkCombo.ArcaneBlinkComboUpdate;
                GetSet.Target = null;
            }
        }

    }
}
