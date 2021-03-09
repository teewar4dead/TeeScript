using Divine;
using Divine.SDK.Extensions;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class DrawRadiusRaze
    {
        public static int[] NeverR = new[] { 200, 450, 700};
        public static void DrawRadiusRazeOnDraw()
        {
            foreach (var Rad in NeverR)
            {
                var Rotation = GetSet.MyHero.RotationRad;
                var vector2Polar = SharpDXExtensions.FromPolarCoordinates(1f, Rotation);


                var Pos = GetSet.MyHero.Position + (vector2Polar.ToVector3() * Rad);
                ParticleManager.CreateOrUpdateParticle(
                    $"DrawRaze_{Rad}",
                    "materials/ensage_ui/particles/alert_range.vpcf",
                    GetSet.MyHero,
                    ParticleAttachment.AbsOrigin,
                    new ControlPoint(0, Pos),
                    new ControlPoint(1, Color.White),
                    new ControlPoint(2, 100, 255, 7));

                if (GetSet.MyHero.Spellbook.Spell1.IsInAbilityPhase && Rad == NeverR[0])
                {
                    ParticleManager.CreateOrUpdateParticle(
                    $"DrawRaze_{Rad}",
                    "materials/ensage_ui/particles/alert_range.vpcf",
                    GetSet.MyHero,
                    ParticleAttachment.AbsOrigin,
                    new ControlPoint(0, Pos),
                    new ControlPoint(1, Color.Red),
                    new ControlPoint(2, 100, 255, 7));
                }
                if (GetSet.MyHero.Spellbook.Spell2.IsInAbilityPhase && Rad == NeverR[1])
                {
                    ParticleManager.CreateOrUpdateParticle(
                    $"DrawRaze_{Rad}",
                    "materials/ensage_ui/particles/alert_range.vpcf",
                    GetSet.MyHero,
                    ParticleAttachment.AbsOrigin,
                    new ControlPoint(0, Pos),
                    new ControlPoint(1, Color.Red),
                    new ControlPoint(2, 100, 255, 7));
                }
                if (GetSet.MyHero.Spellbook.Spell3.IsInAbilityPhase && Rad == NeverR[2])
                {
                    ParticleManager.CreateOrUpdateParticle(
                    $"DrawRaze_{Rad}",
                    "materials/ensage_ui/particles/alert_range.vpcf",
                    GetSet.MyHero,
                    ParticleAttachment.AbsOrigin,
                    new ControlPoint(0, Pos),
                    new ControlPoint(1, Color.Red),
                    new ControlPoint(2, 100, 255, 7));
                }
            }
        }
    }
}
