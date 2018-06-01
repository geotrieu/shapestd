using System.Drawing;
using System.Windows.Forms;

namespace ShapesTD
{
    public class MouseHandler
    {
        private static Point pos;

        public MouseHandler(ref Point p)
        {
            pos = p;
        }
        
        public static int getX()
        {
            return pos.X;
        }
        
        public static int getY()
        {
            return pos.Y;
        }
        
    }
}