using System.Collections.Generic;
using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Creeps;
using Divine.Extensions;
using Divine.Game;

namespace TeeLoneDruid.Bear.Items
{
    class ItemsDinamic
    {
        private static bool ManaCheck(float manaCost, float manaPool)
        {
            if (manaPool - manaCost > 0)
                return true;
            return false;
        }
        public static Creep GetCreepForMidasItem(Unit unitRangeFind)
        {
            return EntityManager.GetEntities<Creep>().FirstOrDefault(x => x.Distance2D(unitRangeFind.Position) <= 600 && x.IsSpawned && x.IsAlive && x.IsValid && x.IsVisible && !x.IsAncient && (!x.IsAlly(GetEntity.LocalDruidHero) || x.IsNeutral || x.IsSummoned));
        }
        public static void CastDinamicItem(Unit hero, Unit target, Dictionary<AbilityId, bool> DictionaryItemsAndBool)
        {

            foreach (var item in hero.Inventory.Items)
            {
                foreach (var ID in DictionaryItemsAndBool)
                {

                    if (item.Id == ID.Key && item.Cooldown == 0 && item != null && item.Level > 0 && item.Cooldown == 0 && ManaCheck(item.ManaCost, hero.Mana) && ID.Value == true && hero.IsAlive && hero != null)
                    {
                        if (item.Id == AbilityId.item_bullwhip && item.Cooldown == 0 && hero.Position.Distance2D(target.Position) <= 700 && target != null)
                        {
                            item.Cast(target);
                        }
                        if (item.Id == AbilityId.item_bullwhip && item.Cooldown == 0 && hero.Position.Distance2D(target.Position) >= 700 && target != null)
                        {
                            item.Cast(hero);
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.UnitTarget) == AbilityBehavior.UnitTarget && (item.TargetTeamType & TargetTeamType.Enemy) == TargetTeamType.Enemy && hero.Position.Distance2D(target.Position) <= 600 && item.Cooldown == 0 && target != null && item.Id != AbilityId.item_hand_of_midas)
                        {
                            item.Cast(target);
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.UnitTarget) == AbilityBehavior.UnitTarget && (item.TargetTeamType & TargetTeamType.Allied) == TargetTeamType.Allied && hero.Position.Distance2D(target.Position) <= 600 && item.Cooldown == 0 && hero != null && hero.IsAlive)
                        {
                            item.Cast(hero);
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.NoTarget) == AbilityBehavior.NoTarget && hero.Position.Distance2D(target.Position) <= 600 && item.Cooldown == 0)
                        {
                            item.Cast();
                        }


                    }
                }

            }
        }
        public static void AutoCastItem(Unit hero, Dictionary<AbilityId, bool> DictionaryItemsAndBool)
        {
            foreach (var item in hero.Inventory.Items)
            {
                foreach (var ID in DictionaryItemsAndBool)
                {
                    if (item.Id == ID.Key && item.Cooldown == 0 && item != null && item.Level > 0 && item.Cooldown == 0 && ManaCheck(item.ManaCost, hero.Mana) && ID.Value == true)
                    {
                        if ((item.AbilityBehavior & AbilityBehavior.UnitTarget) == AbilityBehavior.UnitTarget && (item.TargetTeamType & TargetTeamType.Enemy) == TargetTeamType.Enemy && item.Id == AbilityId.item_hand_of_midas)
                        {
                            item.Cast(GetCreepForMidasItem(hero));
                        }
                        if (item.Id == AbilityId.item_bullwhip && item.Cooldown == 0 && hero.IsMoving)
                        {
                            if (item.Cast(hero))
                            {
                                hero.Move(GameManager.MousePosition);
                            }


                           
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.NoTarget) == AbilityBehavior.NoTarget && (item.Id == AbilityId.item_phase_boots || item.Id == AbilityId.item_spider_legs) && hero.IsMoving)
                        {
                            item.Cast();
                        }
                    }
                        
                }

            }
        }


    }

}
