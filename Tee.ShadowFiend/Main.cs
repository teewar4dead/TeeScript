using Divine;

namespace Tee.ShadowFiend
{
    public class Main:Bootstrapper
    {
        protected override void OnActivate()
        {
            GetSet.MyHero = EntityManager.LocalHero;
            if (GetSet.MyHero == null || !GetSet.MyHero.IsValid ||
                GetSet.MyHero.ClassId != ClassId.CDOTA_Unit_Hero_Nevermore)
            {
                return;
            }
            GetSet.Ultimate = GetSet.MyHero.Spellbook.Spell6;
            new GlobalMenu();
        }
    }
}
