using System.Linq;

using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Abilities.Items;
using Divine.Extensions;
using Divine.Helpers;
using Divine.Orbwalker;
using Divine.Update;

namespace Tee.ShadowFiend
{
    class MainCombo : GetSet
    {
        private static AbilityId IgnoreItemForLinka;
        private static Sleeper OrdSleeperLinka = new Sleeper();
        private static Sleeper OrdSleeperItem = new Sleeper();
        private static Sleeper OrdSleeperSpell1 = new Sleeper();
        private static Sleeper OrdSleeperSpell2 = new Sleeper();
        private static Sleeper OrdSleeperSpell3 = new Sleeper();

        public static void DynamicItemCastForLinka(AbilityId IgnoreItemForLinkes = AbilityId.dota_base_ability)
        {
            IgnoreItemForLinka = IgnoreItemForLinkes;
            LinkaUpdate();
        }

        public static void LinkaUpdate()
        {

            foreach (var IDitem in GlobalMenu.ListLinkenToggler)
            {

                if (!Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target))
                {
                    return;
                }
                if (OrdSleeperLinka.Sleeping)
                {
                    return;
                }
                if (IDitem.Key == IgnoreItemForLinka)
                {
                    continue;
                }
                if (IDitem.Value)
                {
                    Item item = Helper.FindItemMain(GetSet.MyHero, IDitem.Key);
                    if (item != null)
                    {
                        if (Helper.CanBeCasted(item, GetSet.MyHero))
                        {
                            if (Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target))
                            {
                                item.Cast(GetSet.Target);
                                OrdSleeperLinka.Sleep(400);
                            }
                        }
                    }
                }

            }
        }

        public static void EndDynamicItem()
        {
            UpdateManager.IngameUpdate -= DynamicItem;
            GetSet.Target = null;
        }


        public static void DynamicItem()
        {

            if (GetSet.Target == null || !GetSet.Target.IsValid || !GetSet.Target.IsAlive)
            {
                GetSet.Target = TargetSelector.ClosestToMouse(GetSet.MyHero);
                return;
            }

            bool RadiusToHero = GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.MyHero.AttackRange;
            if (item_arcane_ring == null || !item_arcane_ring.IsValid)
            {
                item_arcane_ring = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_arcane_ring);
            }

            if (item_bkb == null || !item_bkb.IsValid)
            {
                item_bkb = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_black_king_bar);
            }

            if (item_bloodthorn == null || !item_bloodthorn.IsValid)
            {
                item_bloodthorn = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_bloodthorn);
            }

            if (item_bullwhip == null || !item_bullwhip.IsValid)
            {
                item_bullwhip = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_bullwhip);
            }

            if (item_diffusal_blade == null || !item_diffusal_blade.IsValid)
            {
                item_diffusal_blade = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_diffusal_blade);
            }

            if (item_essence_ring == null || !item_essence_ring.IsValid)
            {
                item_essence_ring = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_essence_ring);
            }

            if (item_eternal_shroud == null || !item_eternal_shroud.IsValid)
            {
                item_eternal_shroud = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_eternal_shroud);
            }

            if (item_ethereal_blade == null || !item_ethereal_blade.IsValid)
            {
                item_ethereal_blade = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_ethereal_blade);
            }

            if (item_gungir == null || !item_gungir.IsValid)
            {
                item_gungir = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_gungir);
            }

            if (item_manta == null || !item_manta.IsValid)
            {
                item_manta = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_manta);
            }

            if (item_mjollnir == null || !item_mjollnir.IsValid)
            {
                item_mjollnir = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_mjollnir);
            }

            if (item_nullifier == null || !item_nullifier.IsValid)
            {
                item_nullifier = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_nullifier);
            }

            if (item_orchid == null || !item_orchid.IsValid)
            {
                item_orchid = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_orchid);
            }

            if (item_phase_boots == null || !item_phase_boots.IsValid)
            {
                item_phase_boots = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_phase_boots);
            }

            if (item_satanic == null || !item_satanic.IsValid)
            {
                item_satanic = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_satanic);
            }

            if (item_sheepstick == null || !item_sheepstick.IsValid)
            {
                item_sheepstick = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_sheepstick);
            }

            if (item_shivas_guard == null || !item_shivas_guard.IsValid)
            {
                item_shivas_guard = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_shivas_guard);
            }

            if (item_silver_edge == null || !item_silver_edge.IsValid)
            {
                item_silver_edge = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_silver_edge);
            }

            if (item_spider_legs == null || !item_spider_legs.IsValid)
            {
                item_spider_legs = Helper.FindItemMain(GetSet.MyHero, AbilityId.item_spider_legs);
            }





            if (item_silver_edge != null && Helper.CanBeCasted(item_silver_edge, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_silver_edge.Id).Value)
            {
                item_silver_edge.Cast();
                return;
            }
            if (Helper.FindModifier(MyHero, "modifier_item_silver_edge_windwalk") != null)
            {


                return;
            }
            LinkaUpdate();





            var spell1 = GetSet.MyHero.Spellbook.Spell1;
            var spell2 = GetSet.MyHero.Spellbook.Spell2;
            var spell3 = GetSet.MyHero.Spellbook.Spell3;

            var Rotation = GetSet.MyHero.RotationRad;
            var vector2Polar = Helper.FromPolarCoordinates(1f, Rotation);


            var Pos1 = GetSet.MyHero.Position + (vector2Polar.ToVector3() * 200);
            var Pos2 = GetSet.MyHero.Position + (vector2Polar.ToVector3() * 450);
            var Pos3 = GetSet.MyHero.Position + (vector2Polar.ToVector3() * 700);

            if (spell1.IsInAbilityPhase && Pos1.Distance2D(GetSet.Target.Position) >= 250)
            {
                GetSet.MyHero.Stop();
            }
            if (spell2.IsInAbilityPhase && Pos2.Distance2D(GetSet.Target.Position) >= 250)
            {
                GetSet.MyHero.Stop();
            }
            if (spell3.IsInAbilityPhase && Pos3.Distance2D(GetSet.Target.Position) >= 250)
            {
                GetSet.MyHero.Stop();
            }

            if (OrdSleeperItem.Sleeping)
            {
                return;
            }
            if (!GlobalMenu.AutoRazeKey)
            {
                var MyHeroHpPrc = (GetSet.MyHero.MaximumHealth / 100) * 30;

                if (item_arcane_ring != null && Helper.CanBeCasted(item_arcane_ring, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_arcane_ring.Id).Value)
                {
                    item_arcane_ring.Cast();
                }
                if (item_bloodthorn != null && Helper.CanBeCasted(item_bloodthorn, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_bloodthorn.Id).Value)
                {
                    item_bloodthorn.Cast(Target);
                }
                if (item_bullwhip != null && Helper.CanBeCasted(item_bullwhip, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_bullwhip.Id).Value)
                {
                    if (RadiusToHero)
                    {
                        item_bullwhip.Cast(Target);
                    }
                    if (RadiusToHero)
                    {
                        item_bullwhip.Cast(MyHero);
                    }

                }
                if (item_bkb != null && Helper.CanBeCasted(item_bkb, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_bkb.Id).Value && MyHero.Position.Distance2D(Target.Position) <= 600)
                {
                    item_bkb.Cast();
                }
                if (item_diffusal_blade != null && Helper.CanBeCasted(item_diffusal_blade, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_diffusal_blade.Id).Value)
                {
                    item_diffusal_blade.Cast(Target);
                }
                if (item_essence_ring != null && Helper.CanBeCasted(item_essence_ring, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_essence_ring.Id).Value)
                {
                    item_essence_ring.Cast();
                }
                if (item_eternal_shroud != null && Helper.CanBeCasted(item_eternal_shroud, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_eternal_shroud.Id).Value)
                {
                    item_eternal_shroud.Cast();
                }
                if (item_ethereal_blade != null && Helper.CanBeCasted(item_ethereal_blade, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_ethereal_blade.Id).Value)
                {
                    item_ethereal_blade.Cast(Target);
                }
                if (item_gungir != null && Helper.CanBeCasted(item_gungir, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_gungir.Id).Value)
                {
                    item_gungir.Cast(Target.Position);
                }
                if (item_invis_sword != null && Helper.CanBeCasted(item_invis_sword, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_invis_sword.Id).Value)
                {
                    item_invis_sword.Cast();
                }
                if (item_manta != null && Helper.CanBeCasted(item_manta, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_manta.Id).Value)
                {
                    item_manta.Cast();
                }
                if (item_mjollnir != null && Helper.CanBeCasted(item_mjollnir, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_mjollnir.Id).Value)
                {
                    item_mjollnir.Cast(MyHero);
                }
                if (item_nullifier != null && Helper.CanBeCasted(item_nullifier, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_nullifier.Id).Value)
                {
                    item_nullifier.Cast(Target);
                }
                if (item_orchid != null && Helper.CanBeCasted(item_orchid, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_orchid.Id).Value)
                {
                    item_orchid.Cast(Target);
                }
                if (item_phase_boots != null && Helper.CanBeCasted(item_phase_boots, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_phase_boots.Id).Value)
                {
                    item_phase_boots.Cast();
                }
                if (item_satanic != null && Helper.CanBeCasted(item_satanic, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_satanic.Id).Value && RadiusToHero && MyHero.Health <= MyHeroHpPrc)
                {
                    item_satanic.Cast();
                }
                if (item_sheepstick != null && Helper.CanBeCasted(item_sheepstick, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_sheepstick.Id).Value)
                {
                    item_sheepstick.Cast(Target);
                }
                if (item_shivas_guard != null && Helper.CanBeCasted(item_shivas_guard, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_shivas_guard.Id).Value)
                {
                    item_shivas_guard.Cast();
                }
                if (item_spider_legs != null && Helper.CanBeCasted(item_spider_legs, MyHero) && GlobalMenu.ListItemsToggler.FirstOrDefault(x => x.Key == item_spider_legs.Id).Value)
                {
                    item_spider_legs.Cast();
                }
            }




            if (Pos1.Distance2D(GetSet.Target.Position) <= 250 && Helper.CanBeCasted(spell1, GetSet.MyHero) && !OrdSleeperSpell1.Sleeping && Helper.FindModifier(GetSet.Target, "modifier_eul_cyclone") == null)
            {
                spell1.Cast();
                OrdSleeperSpell1.Sleep(600);
            }

            if (Pos2.Distance2D(GetSet.Target.Position) <= 250 && Helper.CanBeCasted(spell2, GetSet.MyHero) && !OrdSleeperSpell2.Sleeping && Helper.FindModifier(GetSet.Target, "modifier_eul_cyclone") == null)
            {
                spell2.Cast();
                OrdSleeperSpell2.Sleep(600);
            }

            if (Pos3.Distance2D(GetSet.Target.Position) <= 250 && Helper.CanBeCasted(spell3, GetSet.MyHero) && !OrdSleeperSpell3.Sleeping && Helper.FindModifier(GetSet.Target, "modifier_eul_cyclone") == null)
            {
                spell3.Cast();
                OrdSleeperSpell3.Sleep(600);

            }


            if (!Target.IsAttackImmune())
            {
                if (GlobalMenu.HitRun.Value)
                {
                    OrbwalkerManager.OrbwalkTo(Target, Target.Position);
                }
                else
                {
                    MyHero.Attack(Target);
                }

            }
            else
            {
                if (MyHero.Position.Distance2D(Target.Position) <= 550)
                {
                    MyHero.MoveToDirection(MyHero.Position.Extend(Target.Position, 1));
                }
                else
                {
                    MyHero.MoveToDirection(MyHero.Position.Extend(Target.Position, 35));
                }

            }

            OrdSleeperItem.Sleep(280);
        }
    }
}
