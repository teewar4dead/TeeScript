using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class DynamicCombo
    {
        private static AbilityId CashItem;
        private static AbilityId IgnoreItemForLinka;
        private static Dictionary<AbilityId, bool> ListItem;
        private static bool CanBeCastedLinka;
        private static Sleeper OrdSleeperLinka = new Sleeper();
        private static Sleeper OrdSleeperItem = new Sleeper();

        public static void DynamicItemCastForLinka(bool CanBeCastedLinkes = false, AbilityId IgnoreItemForLinkes = AbilityId.dota_base_ability)
        {
            IgnoreItemForLinka = IgnoreItemForLinkes;
            CanBeCastedLinka = CanBeCastedLinkes;
            LinkaUpdate();
        }

        public static void LinkaUpdate()
        {

            foreach (var IDitem in GlobalMenu.ListLinkenToggler)
            {
                if (OrdSleeperLinka.Sleeping)
                {
                    return;
                }
                if (!Helper.CanBeCasted(AbilityId.item_sphere, GetSet.Target))
                {
                    return;
                }
               
                if(IDitem.Key == IgnoreItemForLinka)
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
                                OrdSleeperLinka.Sleep(5000);
                            }
                        }
                    }
                }
                
            }
        }

        public static void EndDynamicItem()
        {
            CashItem = default;
            ListItem = null;
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
            GetSet.Target = null;
        }
        public static void DynamicItem()
        {
           
            UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
        }

        private static void UpdateManager_IngameUpdate()
        {
            try
            {
                foreach (var ID in GlobalMenu.ListItemsToggler)
                {
                    Item item = Helper.FindItemMain(GetSet.MyHero, ID.Key);

                    if (item == null)
                    {
                        continue;
                    }

                    if (!Helper.CanBeCasted(item, GetSet.MyHero))
                    {
                        continue;
                    }
                    bool InvisSwordBool = item.Id == AbilityId.item_invis_sword;
                    bool SiverEdgeBool = item.Id == AbilityId.item_silver_edge;
                    bool EullBool = item.Id == AbilityId.item_cyclone;
                    bool EtherealBool = item.Id == AbilityId.item_ethereal_blade;
                    bool bullwhipBool = item.Id == AbilityId.item_bullwhip;
                    bool midasBool = item.Id == AbilityId.item_hand_of_midas;
                    bool SatanicBool = item.Id == AbilityId.item_satanic;
                    bool RadiusToHero = GetSet.MyHero.Position.Distance2D(GetSet.Target.Position) <= GetSet.MyHero.AttackRange;

                    var MyHeroHpPrc = (GetSet.MyHero.MaximumHealth / 100) * 30;
                    if (ID.Value)
                    {
                        if (SiverEdgeBool || InvisSwordBool)
                        {
                            item.Cast();
                            GetSet.MyHero.Attack(GetSet.Target);
                        }

                        if (EtherealBool && item.Id != CashItem && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast(GetSet.Target);
                            return;

                        }
                        if (bullwhipBool && RadiusToHero && item.Id != CashItem && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast(GetSet.Target);
                            return;

                        }
                        if (bullwhipBool && !RadiusToHero && item.Id != CashItem && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast(GetSet.MyHero);
                            return;

                        }

                        if (SatanicBool && RadiusToHero && GetSet.MyHero.Health <= MyHeroHpPrc && item.Id != CashItem && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast();
                            return;
                        }

                        if ((item.AbilityBehavior & AbilityBehavior.UnitTarget & AbilityBehavior.DontResumeAttack) == (AbilityBehavior.UnitTarget & AbilityBehavior.DontResumeAttack)
                            && (item.TargetTeamType & TargetTeamType.All) == TargetTeamType.All
                            && (item.TargetType & TargetType.Creeps & TargetType.Heroes) == (TargetType.Heroes & TargetType.Creeps)
                            && !EtherealBool
                            && !EullBool
                            && !SatanicBool
                            && !SiverEdgeBool
                            && !InvisSwordBool
                            && item.Id != CashItem
                            && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast(GetSet.Target);
                            return;
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.UnitTarget) == (AbilityBehavior.UnitTarget)
                            && (item.TargetTeamType & TargetTeamType.All) == TargetTeamType.All
                            && (item.TargetType & TargetType.Basic & TargetType.Heroes) == (TargetType.Heroes & TargetType.Basic)
                            && !EtherealBool
                            && !EullBool
                            && !SiverEdgeBool
                            && !InvisSwordBool
                            && !SatanicBool
                            && item.Id != CashItem
                            && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast(GetSet.Target);
                            return;
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.UnitTarget) == AbilityBehavior.UnitTarget
                            && (item.TargetTeamType & TargetTeamType.Enemy) == TargetTeamType.Enemy
                            && (item.TargetType & TargetType.Heroes & TargetType.Basic) == (TargetType.Heroes & TargetType.Basic)
                            && !midasBool
                            && !EtherealBool
                            && !EullBool
                            && !SiverEdgeBool
                            && !InvisSwordBool
                            && !SatanicBool
                            && item.Id != CashItem
                            && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast(GetSet.Target);
                            return;
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.UnitTarget) == AbilityBehavior.UnitTarget
                            && (item.TargetTeamType & TargetTeamType.Allied) == TargetTeamType.Allied
                            && (item.TargetType & TargetType.Heroes & TargetType.Basic) == (TargetType.Heroes & TargetType.Basic)
                            && !EtherealBool
                            && !EullBool
                            && !SiverEdgeBool
                            && !InvisSwordBool
                            && !SatanicBool
                            && item.Id != CashItem
                            && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast(GetSet.MyHero);
                            return;
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.NoTarget & AbilityBehavior.DontResumeAttack) == (AbilityBehavior.NoTarget & AbilityBehavior.DontResumeAttack)
                            && (item.TargetType & TargetType.None) == (TargetType.None)
                            && item.Id != CashItem
                            && !SiverEdgeBool
                            && !InvisSwordBool
                            && !SatanicBool
                            && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast();
                            return;
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.NoTarget & AbilityBehavior.Immediate & AbilityBehavior.IgnoreChannel) == (AbilityBehavior.NoTarget & AbilityBehavior.Immediate & AbilityBehavior.IgnoreChannel)
                            && (item.TargetType & TargetType.None) == (TargetType.None)
                            && item.Id != CashItem
                            && !SiverEdgeBool
                            && !InvisSwordBool
                            && !SatanicBool
                            && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast();
                            return;
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.NoTarget & AbilityBehavior.Immediate) == (AbilityBehavior.NoTarget & AbilityBehavior.Immediate)
                            && (item.TargetType & TargetType.None) == (TargetType.None)
                            && !SatanicBool
                            && !SiverEdgeBool
                            && !InvisSwordBool
                            && item.Id != CashItem
                            && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast();
                            return;
                        }
                        if ((item.AbilityBehavior & AbilityBehavior.Point & AbilityBehavior.AreaOfEffect) == (AbilityBehavior.Point & AbilityBehavior.AreaOfEffect)
                            && (item.TargetType & TargetType.Heroes & TargetType.Basic) == (TargetType.Heroes & TargetType.Basic)
                            && item.Id != CashItem
                            && !SiverEdgeBool
                            && !InvisSwordBool
                            && !SatanicBool
                            && !OrdSleeperItem.Sleeping)
                        {
                            OrdSleeperItem.Sleep(150);
                            item.Cast(GetSet.Target.Position);
                            return;
                        }
                        CashItem = item.Id;
                    }
                }
            }
            catch (Exception)
            {
                EndDynamicItem();
            }
            if (OrdSleeperItem.Sleeping)
            {
                return;
            }
          
            
        }
    }
}
