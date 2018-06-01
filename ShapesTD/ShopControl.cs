using System.Drawing;

namespace ShapesTD
{
    public class ShopControl
    {
        public static Point yellowTow = new Point(32, Form1.height * 32 - 48);
        public static Point startButton = new Point(Form1.width * 32 - 32, Form1.height * 32 - 56);
        
        public static void drawShop()
        {
            Form1.offscreen.DrawImage(Form1.yellowtow, yellowTow);
            Form1.offscreen.DrawString("100$", Form1.defFont, new SolidBrush(Color.White), new Point(30, Form1.height * 32 - 16));
            
            //Start Button
            Form1.offscreen.DrawImage(Form1.start, startButton);
        }

        public static void drawCursorPickup()
        {
            if (Form1.pickedUp != null)
            {
                if (Form1.pickedUp == "yellowtow")
                {
                    Form1.offscreen.DrawImage(Form1.yellowtow, Form1.mouseX - 15, Form1.mouseY - 15);
                }
            }
        }
    }
}