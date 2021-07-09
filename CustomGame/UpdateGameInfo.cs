using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.PhysicalItems;
using Divine.Entity.Entities.Units;
using Divine.Extensions;
using Divine.Game;
using Divine.Update;

namespace CustomGame
{
    class UpdateGameInfo
    {
        public static Unit MyHero { get; private set; }
        public static PhysicalItem PhysicalItem { get; private set; }
        public static string GameName { get; private set; }
        #region RandomDefense
        public static Unit Belka { get; private set; }
        public static Unit GoldRosha { get; private set; }
        public static PhysicalItem Key { get; private set; }
        public static PhysicalItem ItemSoul { get; private set; }
        #endregion

        public UpdateGameInfo()
        {
            GameName = GameManager.LevelName;

            UpdateManager.GameUpdate += () =>
            {
                MyHero = EntityManager.LocalHero;
                PhysicalItem = EntityManager.GetEntities<PhysicalItem>().OrderBy(x => MyHero.Position.Distance(x.Position)).Where(x => x.Distance2D(MyHero.Position) <= 300).FirstOrDefault();

                //RandomDefense
                Key = EntityManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Item.Name == "item_key");
                Belka = EntityManager.GetEntities<Unit>().FirstOrDefault(x => x.Name == "belka");
                GoldRosha = EntityManager.GetEntities<Unit>().FirstOrDefault(x => x.Name == "npc_treasure_chest");
                ItemSoul = EntityManager.GetEntities<PhysicalItem>().Where(x => x.Distance2D(MyHero.Position) <= 300).FirstOrDefault(x => x.Item.Name == "item_forest_soul"|| x.Item.Name == "item_swamp_soul" || x.Item.Name == "item_village_soul" || x.Item.Name == "item_mines_soul" || x.Item.Name == "item_dust_soul" || x.Item.Name == "item_snow_soul" || x.Item.Name == "item_key");

            };
        }
    }
}
