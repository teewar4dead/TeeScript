using Divine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
