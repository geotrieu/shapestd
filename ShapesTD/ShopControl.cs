using System.Drawing;

namespace ShapesTD
{
    public class ShopControl
    {
        public static Point bulletTower = new Point(32, Form1.height * 32 - 48);
        public static Point laserTower = new Point(96, Form1.height * 32 - 48);
        public static Point freezeTower = new Point(160, Form1.height * 32 - 48);
        public static Point startButton = new Point(Form1.width * 32 - 32, Form1.height * 32 - 56);
        
        public static void drawShop()
        {
            //Bullet Tower
            Form1.offscreen.DrawImage(Form1.bullettower, bulletTower);
            Form1.offscreen.DrawString("100$", Form1.defFont, new SolidBrush(Color.White), new Point(bulletTower.X - 2, Form1.height * 32 - 16));
            //Laser Tower
            Form1.offscreen.DrawImage(Form1.lasertower, laserTower);
            Form1.offscreen.DrawString("300$", Form1.defFont, new SolidBrush(Color.White), new Point(laserTower.X - 2, Form1.height * 32 - 16));
            //Freeze Tower
            Form1.offscreen.DrawImage(Form1.freezetower, freezeTower);
            Form1.offscreen.DrawString("500$", Form1.defFont, new SolidBrush(Color.White), new Point(freezeTower.X - 2, Form1.height * 32 - 16));
            
            //Start Button
            Form1.offscreen.DrawImage(Form1.start, startButton);
        }

        public static void drawCursorPickup()
        {
            if (Form1.pickedUp != null)
            {
                if (Form1.pickedUp == "bullettower")
                {
                    Form1.offscreen.DrawImage(Form1.bullettower, Form1.mouseX - 15, Form1.mouseY - 15);
                }
                if (Form1.pickedUp == "lasertower")
                {
                    Form1.offscreen.DrawImage(Form1.lasertower, Form1.mouseX - 15, Form1.mouseY - 15);
                }
                if (Form1.pickedUp == "freezetower")
                {
                    Form1.offscreen.DrawImage(Form1.freezetower, Form1.mouseX - 15, Form1.mouseY - 15);
                }
            }
        }
    }
}