using System.Drawing;
using ShapesTD.resources;

namespace ShapesTD
{
    public class ShopControl
    {
        public static Point bulletTower = new Point(32, Form1.height * 32 - 48);
        public static Point laserTower = new Point(96, Form1.height * 32 - 48);
        public static Point freezeTower = new Point(160, Form1.height * 32 - 48);
        public static Point cannonTower = new Point(224, Form1.height * 32 - 48);
        public static Point heartTower = new Point(288, Form1.height * 32 - 48);
        public static Point volleyTower = new Point(352, Form1.height * 32 - 48);
        public static Point machineGunTower = new Point(416, Form1.height * 32 - 48);
        public static Point startButton = new Point(Form1.width * 32 - 96, Form1.height * 32 - 64);
        public static Point pauseButton = new Point(Form1.width * 32 - 64, Form1.height * 32 - 64);
        public static Point fastslowButton = new Point(Form1.width * 32 - 32, Form1.height * 32 - 64);
        public static Point sellButton = new Point(Form1.width * 32 - 64, Form1.height * 32 - 32);
        
        public static void DrawShop()
        {
            //Bullet Tower
            Form1.offscreen.DrawImage(Form1.bullettower, bulletTower);
            Form1.offscreen.DrawString("100$", Form1.defFont, new SolidBrush(Color.White), new Point(bulletTower.X - 2, Form1.height * 32 - 16));
            //Laser Tower
            Form1.offscreen.DrawImage(Form1.lasertower, laserTower);
            Form1.offscreen.DrawString("500$", Form1.defFont, new SolidBrush(Color.White), new Point(laserTower.X - 2, Form1.height * 32 - 16));
            //Freeze Tower
            Form1.offscreen.DrawImage(Form1.freezetower, freezeTower);
            Form1.offscreen.DrawString("750$", Form1.defFont, new SolidBrush(Color.White), new Point(freezeTower.X - 2, Form1.height * 32 - 16));
            //Cannon Tower
            Form1.offscreen.DrawImage(Form1.cannontower, cannonTower);
            Form1.offscreen.DrawString("1250$", Form1.defFont, new SolidBrush(Color.White), new Point(cannonTower.X - 6, Form1.height * 32 - 16));
            //Heart Tower
            Form1.offscreen.DrawImage(Form1.hearttower, heartTower);
            Form1.offscreen.DrawString("3500$", Form1.defFont, new SolidBrush(Color.White), new Point(heartTower.X - 6, Form1.height * 32 - 16));
            //Cannon Tower
            Form1.offscreen.DrawImage(Form1.volleytower, volleyTower);
            Form1.offscreen.DrawString("5000$", Form1.defFont, new SolidBrush(Color.White), new Point(volleyTower.X - 6, Form1.height * 32 - 16));
            //Machine Gun Tower
            Form1.offscreen.DrawImage(Form1.machineguntower, machineGunTower);
            Form1.offscreen.DrawString("10000$", Form1.defFont, new SolidBrush(Color.White), new Point(machineGunTower.X - 9, Form1.height * 32 - 16));
            
            //Start Button
            if (!Form1.isFast)
            {
                Form1.offscreen.DrawImage(Form1.start, startButton);
            }
            //Pause Button
            Form1.offscreen.DrawImage(Form1.pause, pauseButton);
            //Fast/Slow Button
            if (Form1.isFast)
            {
                Form1.offscreen.DrawImage(Form1.slow, fastslowButton);
            }
            else
            {
                Form1.offscreen.DrawImage(Form1.fast, fastslowButton);
            }
            //Sell Button
            if (Form1.selectedTower != null)
                Form1.offscreen.DrawImage(Form1.sell, sellButton);
        }

        public static void DrawCursorPickup()
        {
            if (Form1.pickedUp != null)
            {
                if (Form1.pickedUp == "bullettower")
                {
                    Form1.offscreen.DrawImage(Form1.bullettower, Form1.mouseX - 15, Form1.mouseY - 15);
                    Form1.offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (Form1.mouseX - 80), (Form1.mouseY - 80), 160, 160);
                }
                else if (Form1.pickedUp == "lasertower")
                {
                    Form1.offscreen.DrawImage(Form1.lasertower, Form1.mouseX - 15, Form1.mouseY - 15);
                    Form1.offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (Form1.mouseX - 46), (Form1.mouseY - 46), 92, 92);
                }
                else if (Form1.pickedUp == "freezetower")
                {
                    Form1.offscreen.DrawImage(Form1.freezetower, Form1.mouseX - 15, Form1.mouseY - 15);
                    Form1.offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (Form1.mouseX - 80), (Form1.mouseY - 80), 160, 160);
                }
                else if (Form1.pickedUp == "cannontower")
                {
                    Form1.offscreen.DrawImage(Form1.cannontower, Form1.mouseX - 15, Form1.mouseY - 15);
                    Form1.offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (Form1.mouseX - 112), (Form1.mouseY - 112), 224, 224);
                }
                else if (Form1.pickedUp == "hearttower")
                {
                    Form1.offscreen.DrawImage(Form1.hearttower, Form1.mouseX - 15, Form1.mouseY - 15);
                    Form1.offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (Form1.mouseX - 80), (Form1.mouseY - 80), 160, 160);
                }
                else if (Form1.pickedUp == "volleytower")
                {
                    Form1.offscreen.DrawImage(Form1.volleytower, Form1.mouseX - 15, Form1.mouseY - 15);
                    Form1.offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (Form1.mouseX - 46), (Form1.mouseY - 46), 92, 92);
                }
                else if (Form1.pickedUp == "machineguntower")
                {
                    Form1.offscreen.DrawImage(Form1.machineguntower, Form1.mouseX - 15, Form1.mouseY - 15);
                    Form1.offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (Form1.mouseX - 46), (Form1.mouseY - 46), 92, 92);
                }
            }
        }
    }
}