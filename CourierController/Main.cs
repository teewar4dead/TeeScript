using Divine;
using Divine.Service;

namespace CourierController
{
    public class Main : Bootstrapper
    {
        protected override void OnActivate() => new GlobalMenu();
    }
}
