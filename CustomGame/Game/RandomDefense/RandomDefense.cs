using Divine;
using Divine.Menu.Items;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using SharpDX;
using System;
using System.Linq;
using System.Reflection;

namespace CustomGame
{
    class RandomDefense : UpdateGameInfo
    {
        private static MenuSwitcher OnOff;
        private static MenuSwitcher BelkaMenu;
        private static MenuSwitcher RoshaGold;
        private static MenuSwitcher Soul;
        private static new MenuSwitcher Key;

        private readonly Sleeper sleeper = new Sleeper();

        public RandomDefense(Menu menu)
        {
            var RootMenu = menu.CreateMenu("Random Defense");
            OnOff = RootMenu.CreateSwitcher("Enable", false);
            BelkaMenu = RootMenu.CreateSwitcher("Показывать на карте белку");
            RoshaGold = RootMenu.CreateSwitcher("Показывать на карте золотого рошана");
            Key = RootMenu.CreateSwitcher("Показывать ключ на карте");
            Soul = RootMenu.CreateSwitcher("Подбирать предметы SOUL");
            if (GameManager.LevelName != "maps/new.vpk") { return; }
            OnOff.ValueChanged += OnOff_ValueChanged;
        }

        private void OnOff_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                RendererManager.Draw += RendererManager_Draw;
                UpdateManager.GameUpdate += UpdateManager_GameUpdate;
            }
            else
            {
                RendererManager.Draw -= RendererManager_Draw;
                UpdateManager.GameUpdate -= UpdateManager_GameUpdate;
            }
        }

        private void UpdateManager_GameUpdate()
        {
            if (MyHero == null) return;
            if (Soul && ItemSoul != null && MyHero.IsAlive)
            {
                MyHero.PickUp(ItemSoul);
            }
            if (sleeper.Sleeping) return;

            if (BelkaMenu && Belka != null)
            {
                ParticleManager.DrawTargetParticleEffect(Belka, Color.Green);
            }

            if (RoshaGold && GoldRosha != null)
            {
                ParticleManager.DrawTargetParticleEffect(GoldRosha, Color.Green);
            }
            sleeper.Sleep(100);
        }

        private void RendererManager_Draw()
        {
            if (BelkaMenu && Belka != null)
            {
                var pos = RendererManager.WorldToScreen(Belka.Position + new Vector3(0, 0, Belka.HealthBarOffset));
                var rect = new RectangleF(pos.X, pos.Y, 100, 100);
                if (!pos.IsZero) RendererManager.DrawTexture("RandomDefense.Belka", rect);
            }
            if (RoshaGold && GoldRosha != null)
            {
                var pos2 = RendererManager.WorldToScreen(GoldRosha.Position + new Vector3(0, 0, GoldRosha.HealthBarOffset));
                var rect2 = new RectangleF(pos2.X, pos2.Y, 100, 100);
                if (!pos2.IsZero) RendererManager.DrawTexture("RandomDefense.RoshanGold", rect2);
            }
            if (Key && UpdateGameInfo.Key != null)
            {
                var pos3 = RendererManager.WorldToScreen(UpdateGameInfo.Key.Position);
                var rect3 = new RectangleF(pos3.X, pos3.Y, 100, 100);
                if (!pos3.IsZero) RendererManager.DrawTexture("RandomDefense.Key", rect3);
            }
        }
    }
}
