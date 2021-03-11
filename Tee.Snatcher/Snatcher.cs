using Divine;
using Divine.SDK.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.Snatcher
{
    class Snatcher
    {
        public static readonly Hero MyHero = EntityManager.LocalHero;
        public Snatcher()
        {
            GlobalMenu.OnOff.ValueChanged += OnOff_ValueChanged;
        }

        private void OnOff_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
            }
            else
            {
                UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
            }
        }

        private void UpdateManager_IngameUpdate()
        {
            var Rune = EntityManager.GetEntities<Rune>().FirstOrDefault(x => x.Distance2D(MyHero.Position) <= 300 && (x.RuneType & RuneType.Bounty) == RuneType.Bounty && x.IsVisible && x.IsValid);
           if(Rune != null)
            {
                MyHero.PickUp(Rune);
            }
        }
    }
}
