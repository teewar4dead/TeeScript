using Divine;
using Divine.Menu.Extensions;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tee.ShadowFiend
{
    class PanelClick
    {
        private static Vector2 Position1 { get; set; }
        private static Vector2 Size1 { get; set; }
        private static Vector2 Position2 { get; set; }
        private static Vector2 Size2 { get; set; }
        private static Vector2 Position3 { get; set; }
        private static Vector2 Size3 { get; set; }
        private static Vector2 Position4 { get; set; }
        private static Vector2 Size4 { get; set; }

        public static bool SelectItem1;
        public static bool SelectItem2;
        public static bool SelectItem3;
        public static bool SelectItem4;




        public static void item1(Vector2 position = default, Vector2 size = default)
        {
            Position1 = position;
            Size1 = size;
        }
        public static void item2(Vector2 position = default, Vector2 size = default)
        {
            Position2 = position;
            Size2 = size;
        }
        public static void item3(Vector2 position = default, Vector2 size = default)
        {
            Position3 = position;
            Size3 = size;
        }
        public static void item4(Vector2 position = default, Vector2 size = default)
        {
            Position4 = position;
            Size4 = size;
        }
        public static void OnMouseKeyDown(MouseEventArgs e)
        {
            if (e.MouseKey != MouseKey.Left)
            {
                return;
            }

            if (e.Position.IsUnderRectangle(new RectangleF(Position1.X - 5, Position1.Y - 5, Size1.X, Size1.Y)))
            {
                SelectItem1 = true;
                SelectItem2 = false;
                SelectItem3 = false;
                SelectItem4 = false;
            }
            if (e.Position.IsUnderRectangle(new RectangleF(Position2.X - 5, Position2.Y - 5, Size2.X, Size2.Y)))
            {
                SelectItem1 = false;
                SelectItem2 = true;
                SelectItem3 = false;
                SelectItem4 = false;
            }
            if (e.Position.IsUnderRectangle(new RectangleF(Position3.X - 5, Position3.Y - 5, Size3.X, Size3.Y)))
            {

                SelectItem1 = false;
                SelectItem2 = false;
                SelectItem3 = true;
                SelectItem4 = false;
            }
            if (e.Position.IsUnderRectangle(new RectangleF(Position4.X - 5, Position4.Y - 5, Size4.X, Size4.Y)))
            {

                SelectItem1 = false;
                SelectItem2 = false;
                SelectItem3 = false;
                SelectItem4 = true;
            }
        }
    }
}
