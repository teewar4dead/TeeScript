using Divine;
using SharpDX;

namespace CustomGame
{
    class ParticleManager
    {
        private static Particle targetParticleEffect;
        public ParticleManager()
        {
        }
        public static void DrawTargetParticleEffect(Unit target, Color color)
        {
            if (targetParticleEffect?.IsValid != true)
            {
                targetParticleEffect = Divine.ParticleManager.CreateParticle(@"materials\ensage_ui\particles\target.vpcf", ParticleAttachment.AbsOriginFollow, target);
                targetParticleEffect.SetControlPoint(6, new Vector3(255));
            }

            targetParticleEffect.SetControlPoint(2, UpdateGameInfo.MyHero.Position);
            targetParticleEffect.SetControlPoint(5, new Vector3(color.R, color.G, color.B));
            targetParticleEffect.SetControlPoint(7, target.Position);
        }
    }
}
