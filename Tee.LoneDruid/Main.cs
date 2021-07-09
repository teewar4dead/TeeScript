using Divine.Service;

namespace TeeLoneDruid
{
    public class Main : Bootstrapper
    {
        protected override void OnActivate()
        {
            new MenuGlobal();
            new BearSpirit();
        }
    }
}