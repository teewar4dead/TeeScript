using Divine;

namespace CourierController
{
    public class Main : Bootstrapper
    {
        protected override void OnActivate() => new GlobalMenu();
    }
}
