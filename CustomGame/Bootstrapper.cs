using Divine.Service;

namespace CustomGame
{
    public class Main: Bootstrapper
    {
        protected override void OnActivate()
        {
            new Common();
        }
    }
}