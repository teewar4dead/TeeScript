using Divine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Divine.SDK.Extensions;
using SharpDX;

namespace Tee.ShadowFiend
{
    class Helper
    {
        public static bool ManaCheckItemAndHero(float manaCost, float manaPool)
        {
            if (manaPool - manaCost > 0)
                return true;
            return false;
        }

        public static bool FindItemBool(Unit unit, AbilityId abilityId)
        {
            var Item = unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);

            bool BoolItem;

            if (Item != null)
            {
                BoolItem = true;
            }
            else
            {
                BoolItem = false;
            }

            return BoolItem;
        }
        public static Item FindItem(Unit unit, AbilityId abilityId)
        {
            var Item = unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);
            return Item;

        }
        public static Item FindItemMain(Unit unit, AbilityId abilityId)
        {
            var Item = unit.Inventory.GetItems((ItemSlot)0, (ItemSlot)5).FirstOrDefault(x => x.Id == abilityId);
            return Item;

        }
        public static Modifier FindModifier(Unit unit, string ModifierName)
        {
            var Modifier = unit.ModifierStatus.Modifiers.FirstOrDefault(x => x.Name == ModifierName);
            return Modifier;
        }

        public static bool CanBeCasted(Ability ability, Unit MyHero)
        {
            bool Value;
            if(ability.Cooldown == 0 && ManaCheckItemAndHero(ability.ManaCost, MyHero.Mana) && MyHero.IsAlive && (!UnitExtensions.IsStunned(MyHero) || !UnitExtensions.IsMuted(MyHero)) && ability.Level > 0)
            {
               Value = true;
            }
            else
            {
                Value = false;
            }
            return Value;
        }
        public static bool CanBeCasted(AbilityId abilityId, Unit MyHero)
        {
            bool Value;
            Item item = FindItemMain(MyHero, abilityId);
            if(item != null)
            {
                if (item.Cooldown == 0 && ManaCheckItemAndHero(item.ManaCost, MyHero.Mana) && item.IsValid && FindItemMain(MyHero, item.Id) != null)
                {

                    Value = true;
                }
                else
                {
                    Value = false;
                }
            }
            else
            {
                Value = false;
            }
            
            return Value;
        }

        private static bool CantMove(Unit hero)
        {
            if (hero == null)
                return false;
            if (hero.UnitState == UnitState.Hexed
                || hero.UnitState == UnitState.Rooted
                || hero.UnitState == UnitState.Stunned
                || hero.HasModifier("modifier_legion_commander_duel")
                || hero.HasModifier("modifier_axe_berserkers_call")
                || hero.HasModifier("modifier_faceless_void_chronosphere_freeze")
                || hero.HasModifier("modifier_bashed"))
                return true;
            return false;
        }
        public static Vector3 GetPredictedPosition(Unit Hero, float delay)
        {
            if (Hero == null)
                return default;
            Vector3 pos = Hero.Position;
            if (CantMove(Hero) || !Hero.IsMoving || delay == 0) return pos;
            float speed = Hero.MovementSpeed;
            return pos + (Vector3)SharpDXExtensions.FromPolarCoordinates(1f, Hero.RotationRad) * speed * delay;
        }
    }

}
