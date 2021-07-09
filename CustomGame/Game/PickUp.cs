using Divine.Menu.Items;
using Divine.Update;

namespace CustomGame.Game
{
    class PickUp
    {
        public static MenuHoldKey HoldKey;
        public PickUp(Menu menu)
        {
            HoldKey = menu.CreateHoldKey("PickUp items", System.Windows.Input.Key.None);
            UpdateManager.GameUpdate += () =>
            {
                if (UpdateGameInfo.PhysicalItem != null 
                && HoldKey 
                && UpdateGameInfo.MyHero != null 
                && UpdateGameInfo.MyHero.IsAlive) { UpdateGameInfo.MyHero.PickUp(UpdateGameInfo.PhysicalItem); }
            };
        }

    }
}
