/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: HeartTower
 * Purpose: The Heart Tower Class inherits the BaseTower
 *          class, to provide enhanced functionality
 *          based on the BaseTower class
 ****************************************************/
using System;
using System.Drawing;
using System.Media;

namespace ShapesTD
{
    public class HeartTower: BaseTower
    {
        private static Image img = Form1.hearttower;
        private Point loc;
        private static int shootRate = 30;
        private static int damage = 10;
        private static int radius = 80;
        private static int cost = 3500;
        private int cycle = 0;
        private static string type = "heart";
        
        public HeartTower(int locX, int locY): base(img, locX, locY, type, shootRate, damage, radius, cost)
        {
            int tileX = locX / 32;
            int tileY = locY / 32;
            loc = new Point(tileX * 32, tileY * 32);
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: CheckEnemies
        * Purpose: Method used to check collisions and act on them
        * Inputs: none
        * Returns: none
        ****************************************************/
        public override void CheckEnemies()
        {
            foreach (BaseEnemy be in Form1.enemies)
            {
                int xDiff = Math.Abs(loc.X + 15 - be.GetLocation().X);
                int yDiff = Math.Abs(loc.Y + 15 - be.GetLocation().Y);
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        if (be.GetHealth() <= 0)
                        {
                            Form1.cash += be.GetReward();
                            Form1.health += 5;
                            be.Destroy();
                        }
                        break;
                    }
                }

                xDiff = Math.Abs(loc.X + 15 - (be.GetLocation().X + 31));
                yDiff = Math.Abs(loc.Y + 15 - be.GetLocation().Y);
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        if (be.GetHealth() <= 0)
                        {
                            Form1.cash += be.GetReward();
                            Form1.health += 5;
                            be.Destroy();
                        }
                        break;
                    }
                }

                xDiff = Math.Abs(loc.X + 15 - (be.GetLocation().X + 31));
                yDiff = Math.Abs(loc.Y + 15 - (be.GetLocation().Y + 31));
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        if (be.GetHealth() <= 0)
                        {
                            Form1.cash += be.GetReward();
                            Form1.health += 5;
                            be.Destroy();
                        }
                        break;
                    }
                }

                xDiff = Math.Abs(loc.X + 15 - be.GetLocation().X);
                yDiff = Math.Abs(loc.Y + 15 - (be.GetLocation().Y + 31));
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        if (be.GetHealth() <= 0)
                        {
                            Form1.cash += be.GetReward();
                            Form1.health += 5;
                            be.Destroy();
                        }
                        break;
                    }
                }

                //else there is no collision
            }

            if (cycle >= shootRate)
            {
                cycle = 0;
            }
            else
            {
                cycle++;
            }
        }
    }
}