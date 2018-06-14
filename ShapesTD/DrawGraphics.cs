/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: DrawGraphics
 * Purpose: Handles all graphics on the offscreen.
 ****************************************************/

using System.Drawing;
using System.Windows.Forms;

namespace ShapesTD
{
    public class DrawGraphics
    {
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: DrawMap
        * Purpose: Obtain the map from the array, and draw the map
        *          on the offscreen.
        * Inputs: none
        * Returns: nothing
        ****************************************************/
        public static void DrawMap()
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

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: DrawHealth
        * Purpose: Draws the health as part of the overlay on the
        *          offscreen
        * Inputs: none
        * Returns: nothing
        ****************************************************/
        public static void DrawHealth()
        {
            Form1.offscreen.DrawImage(Form1.heart, new Point(Form1.width * 32 - 85, 8));
            Form1.offscreen.DrawString(Form1.health.ToString(), Form1.defFont, new SolidBrush(Color.Black),
                new Point(Form1.width * 32 - 65, 8));
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: DrawCash
        * Purpose: Draws the cash as part of the overlay on the
        *          offscreen
        * Inputs: none
        * Returns: nothing
        ****************************************************/
        public static void DrawCash()
        {
            Form1.offscreen.DrawImage(Form1.coin, new Point(Form1.width * 32 - 85, 40));
            Form1.offscreen.DrawString("$" + Form1.cash, Form1.defFont, new SolidBrush(Color.Black),
                new Point(Form1.width * 32 - 65, 40));
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: DrawWave
        * Purpose: Draws the Wave # as part of the overlay on the
        *          offscreen
        * Inputs: none
        * Returns: nothing
        ****************************************************/
        public static void DrawWave()
        {
            Form1.offscreen.DrawImage(Form1.waveImg, new Point(Form1.width * 32 - 85, 72));
            Form1.offscreen.DrawString("#" + (Form1.wave + 1), Form1.defFont, new SolidBrush(Color.Black),
                new Point(Form1.width * 32 - 65, 72));
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: DrawEveryTick
        * Purpose: Function to handle all components drawn on
        *          screen every tick.
        * Inputs: none
        * Returns: nothing
        ****************************************************/
        public static void DrawEveryTick()
        {
            //Clear Screen
            Form1.offscreen.Clear(Color.White);

            //Draw Level
            DrawMap();

            //Selected Tower/Radius Draw
            foreach (BaseTower bt in Form1.towers)
            {
                if (Form1.selectedTower == bt)
                    bt.DrawRadius();
            }
            foreach (BaseTower bt in Form1.towers)
            {
                bt.DrawTower();
            }

            //Draw Projectiles
            foreach (BasePair bp in Form1.shootingAt)
            {
                if (bp.GetTower().GetTowerType() == "laser")
                {
                    Point ptTower = new Point(bp.GetTower().GetLocation().X + 15, bp.GetTower().GetLocation().Y + 15);
                    Point ptEnemy = new Point(bp.GetEnemy().GetLocation().X + 15, bp.GetEnemy().GetLocation().Y + 15);
                    Form1.offscreen.DrawLine(new Pen(Color.Red, 3), ptTower, ptEnemy);
                }
                else if (bp.GetTower().GetTowerType() == "freeze")
                {
                    Point ptTower = new Point(bp.GetTower().GetLocation().X + 15, bp.GetTower().GetLocation().Y + 15);
                    Point ptEnemy = new Point(bp.GetEnemy().GetLocation().X + 15, bp.GetEnemy().GetLocation().Y + 15);
                    Form1.offscreen.DrawLine(new Pen(Color.Aqua, 3), ptTower, ptEnemy);
                }
                else if (bp.GetTower().GetTowerType() == "bullet")
                {
                    Point ptTower = new Point(bp.GetTower().GetLocation().X + 15, bp.GetTower().GetLocation().Y + 15);
                    Point ptEnemy = new Point(bp.GetEnemy().GetLocation().X + 15, bp.GetEnemy().GetLocation().Y + 15);
                    Form1.offscreen.DrawLine(new Pen(Color.OrangeRed, 3), ptTower, ptEnemy);
                }
                else if (bp.GetTower().GetTowerType() == "machinegun")
                {
                    Point ptTower = new Point(bp.GetTower().GetLocation().X + 15, bp.GetTower().GetLocation().Y + 15);
                    Point ptEnemy = new Point(bp.GetEnemy().GetLocation().X + 15, bp.GetEnemy().GetLocation().Y + 15);
                    Form1.offscreen.DrawLine(new Pen(Color.Pink, 3), ptTower, ptEnemy);
                }
            }
            
            //Draw the enemies on screen
            foreach (BaseEnemy be in Form1.enemies)
            {
                be.DrawEnemy();
            }

            //Draw Overlays
            DrawHealth();
            DrawCash();
            DrawWave();
            ShopControl.DrawShop();
            ShopControl.DrawCursorPickup();

            //Draw MessageBox if necessary
            if (MessageBox.messageActive)
            {
                MessageBox.DrawMessageBox();
            }

            //Push offscreen to onscreen
            Form1.dc.DrawImage(Form1.curBitmap, 0, 0);
        }
    }
}