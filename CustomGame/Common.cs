using System.Reflection;

using CustomGame.Game;

using Divine.Entity.Entities.Abilities.Components;
using Divine.Menu;
using Divine.Menu.Items;
using Divine.Renderer;

namespace CustomGame
{
    class Common
    {
        public RootMenu RootMenu {get; private set;}
        public Common()
        {
            RootMenu = MenuManager.CreateRootMenu("Custom Game");
            RootMenu.SetAbilityImage(AbilityId.roshan_bash);

            //initialization Class
            new RandomDefense(RootMenu);
            new PickUp(RootMenu);
            //initialization Image
            RendererManager.LoadImage("RandomDefense.Belka", Assembly.GetExecutingAssembly().GetManifestResourceStream(@"CustomGame.img.s1200.png"));
            RendererManager.LoadImage("RandomDefense.Key", Assembly.GetExecutingAssembly().GetManifestResourceStream(@"CustomGame.img.key.png"));
            RendererManager.LoadImage("RandomDefense.RoshanGold", Assembly.GetExecutingAssembly().GetManifestResourceStream(@"CustomGame.img.Roshan_model_Gold.png"));
        }
    }
}
