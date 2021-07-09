using System.Collections.Generic;
using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.Components;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Creeps;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Game;

namespace TeeLoneDruid.Bear
{
    class GetEntity
    {
        public static bool CourierGiveItem { get; set; }
        public bool Prioritet { get; set; }
        public bool Prioritet_2 { get; set; }
        public static Unit BearHero;
        public static Hero HeroTarget;
        public static readonly Hero LocalDruidHero = EntityManager.LocalHero;
        public static Hero GetDruidHero()
        {
            return LocalDruidHero;
        }
        public static Unit GetBearHero()
        {
            BearHero = EntityManager.GetEntities<Unit>().FirstOrDefault(x => x.ClassId == ClassId.CDOTA_Unit_SpiritBear && x.IsAlive && x.IsControllable);
            return BearHero;
        }
        public static Unit GetSelectBear()
        {
            return LocalDruidHero.Player.SelectedUnits.Where(x => x.ClassId == ClassId.CDOTA_Unit_SpiritBear).FirstOrDefault();
        }
        public static Unit GetSelectDruid()
        {
           return LocalDruidHero.Player.SelectedUnits.Where(x => x.ClassId == ClassId.CDOTA_Unit_Hero_LoneDruid).FirstOrDefault();
        }
        public static IEnumerable<Unit> GetSelectDruidAndBear()
        {
            return LocalDruidHero.Player.SelectedUnits.Where(x => x.ClassId == ClassId.CDOTA_Unit_SpiritBear || x.ClassId == ClassId.CDOTA_Unit_Hero_LoneDruid);
        }
        public static Courier GetCourier()
        {
            return EntityManager.GetEntities<Courier>().Where(x => x.IsValid && x.IsAlive && x.IsControllable && x.ClassId == ClassId.CDOTA_Unit_Courier).FirstOrDefault();
        }
        public static Hero GetHeroTarget()
        {

            HeroTarget = EntityManager.GetEntities<Hero>().Where(x => !x.IsAlly(EntityManager.LocalHero)
                   && x.IsAlive
                   && x.IsVisible
                   && x.IsValid
                   && !x.IsIllusion
                     && x.Distance2D(GameManager.MousePosition) < 800).OrderBy(x => x.Distance2D(GameManager.MousePosition)).FirstOrDefault();
            return HeroTarget;
        }
        public static Creep GetCreepForMidasItem(Unit unitRangeFind)
        {
            return EntityManager.GetEntities<Creep>().FirstOrDefault(x => x.Distance2D(unitRangeFind.Position) <= 600 && x.IsSpawned && x.IsAlive && x.IsValid && x.IsVisible && !x.IsAncient && (!x.IsAlly(GetEntity.LocalDruidHero) || x.IsNeutral || x.IsSummoned));
        }

    }
}