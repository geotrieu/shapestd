using System.Drawing;
using System.Windows.Forms;

namespace ShapesTD
{
    public class DrawGraphics
    {
        public static void drawMap()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 12; y++)
                {
                    if (Form1.levelMap[x, y] == '0')
                    {
                        Form1.offscreen.DrawImage(Form1.grass, new Point(x * 32, y * 32));
                    }
                    else if (Form1.levelMap[x, y] == '1' || Form1.levelMap[x, y] == 'S')
                    {
                        Form1.offscreen.DrawImage(Form1.dirt, new Point(x * 32, y * 32));
                    }
                    else if (Form1.levelMap[x, y] == 'E')
                    {
                        Form1.offscreen.DrawImage(Form1.homebase, new Point(x * 32, y * 32));
                    }
                }
            }
        }

        public static void drawHealth()
        {
            Form1.offscreen.DrawImage(Form1.heart, new Point(Form1.width * 32 - 64, 8));
            Form1.offscreen.DrawString(Form1.health.ToString(), Form1.defFont, new SolidBrush(Color.Black), new Point(Form1.width * 32 - 45, 8));
        }

        public static void drawCash()
        {
            Form1.offscreen.DrawImage(Form1.coin, new Point(Form1.width * 32 - 64, 40));
            Form1.offscreen.DrawString("$" + Form1.cash.ToString(), Form1.defFont, new SolidBrush(Color.Black), new Point(Form1.width * 32 - 45, 40));
        }
        
        public static void drawWave()
        {
            Form1.offscreen.DrawImage(Form1.waveImg, new Point(Form1.width * 32 - 64, 72));
            Form1.offscreen.DrawString("#" + (Form1.wave + 1).ToString(), Form1.defFont, new SolidBrush(Color.Black), new Point(Form1.width * 32 - 45, 72));
        }

        public static void drawEveryTick()
        {
            //Clear Screen
            Form1.offscreen.Clear(Color.White);

            //Draw Level
            drawMap();
            //Draw Overlays
            drawHealth();
            drawCash();
            drawWave();

            foreach (BaseEnemy be in Form1.enemies)
            {
                be.drawEnemy(ref Form1.offscreen);
            }

            foreach (BaseTower bt in Form1.towers)
            {
                bt.drawRadius(ref Form1.offscreen);
                bt.drawTower(ref Form1.offscreen);
            }

            Form1.dc.DrawImage(Form1.curBitmap, 0, 0);
        }
    }
}