using Divine.Numerics;
using Divine.Particle;
using Divine.Particle.Components;

namespace TeeLoneDruid.Bear
{
    class VisibleByEnemy : GetEntity
    {
        public VisibleByEnemy()
        {
            if (BearConfig.VBE.Value)
            {
                if (BearHero.IsVisibleToEnemies && BearHero.IsAlive)
                {
                    ParticleManager.CreateOrUpdateParticle("VBE", "particles/items_fx/aura_shivas.vpcf", BearHero, ParticleAttachment.AbsOriginFollow, new ControlPoint(1, 255, 255, 255), new ControlPoint(2, 255));
                }
                else
                {
                    ParticleManager.RemoveParticle("VBE");
                }
            }
            else
            {
                ParticleManager.RemoveParticle("VBE");
            }
        }
    }
}
