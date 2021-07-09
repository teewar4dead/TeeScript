using Divine.Service;

namespace Tee.Snatcher
{
    public class Main : Bootstrapper
    {
        protected override void OnActivate()
        {
            new GlobalMenu();
        }
    }
}
