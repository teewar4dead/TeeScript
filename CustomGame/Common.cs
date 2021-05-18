using CustomGame.Game;
using Divine;
using Divine.Menu;
using Divine.Menu.Items;
using System.Reflection;

namespace CustomGame
{
    class Common
    {
        public RootMenu RootMenu {get; private set;}
        public Common()
        {
            RootMenu = MenuManager.CreateRootMenu("Custom Game");
            RootMenu.SetAbilityTexture(Divine.AbilityId.roshan_bash);

            //initialization Class
            new RandomDefense(RootMenu);
            new PickUp(RootMenu);
            //initialization Image
            RendererManager.LoadTexture("RandomDefense.Belka", Assembly.GetExecutingAssembly().GetManifestResourceStream(@"CustomGame.img.s1200.png"));
            RendererManager.LoadTexture("RandomDefense.Key", Assembly.GetExecutingAssembly().GetManifestResourceStream(@"CustomGame.img.key.png"));
            RendererManager.LoadTexture("RandomDefense.RoshanGold", Assembly.GetExecutingAssembly().GetManifestResourceStream(@"CustomGame.img.Roshan_model_Gold.png"));
        }
    }
}
