using System.Drawing;
using System.Windows.Forms;

namespace ShapesTD
{
    public class DrawGraphics
    {
        public static void drawMap()
        {
            for (int x = 0; x < Form1.width; x++)
            {
                for (int y = 0; y < Form1.height; y++)
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
                    else if (Form1.levelMap[x, y] == 'P')
                    {
                        Form1.offscreen.DrawImage(Form1.steel, new Point(x * 32, y * 32));
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
            Form1.offscreen.DrawString("$" + Form1.cash, Form1.defFont, new SolidBrush(Color.Black), new Point(Form1.width * 32 - 45, 40));
        }
        
        public static void drawWave()
        {
            Form1.offscreen.DrawImage(Form1.waveImg, new Point(Form1.width * 32 - 64, 72));
            Form1.offscreen.DrawString("#" + (Form1.wave + 1), Form1.defFont, new SolidBrush(Color.Black), new Point(Form1.width * 32 - 45, 72));
        }

        public static void drawPopUpWave()
        {
            Form1.offscreen.DrawString("Wave " + (Form1.wave + 1), Form1.bigFont, new SolidBrush(Color.Black), new Point(Form1.width * 32 / 2 - 100, 275));
        }

        public static void drawShop()
        {
            Form1.offscreen.DrawImage(Form1.yellowtow, new Point(32, Form1.height * 32 - 48));
            Form1.offscreen.DrawString("100$", Form1.defFont, new SolidBrush(Color.White), new Point(30, Form1.height * 32 - 16));
        }

        public static void drawDebug()
        {
            Form1.offscreen.DrawString("X: " + MouseHandler.getX(), Form1.defFont, new SolidBrush(Color.White), new Point(Form1.width * 32 - 96, Form1.height * 32 - 64));
            Form1.offscreen.DrawString("Y: " + MouseHandler.getX(), Form1.defFont, new SolidBrush(Color.White), new Point(Form1.width * 32 - 96, Form1.height * 32 - 48));
        }
        
        public static void drawEveryTick()
        {
            //Clear Screen
            Form1.offscreen.Clear(Color.White);

            //Draw Level
            drawMap();
            drawShop();
            //Draw Overlays
            drawHealth();
            drawCash();
            drawWave();
            
            //Draw Debug
            drawDebug();

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