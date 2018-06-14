/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: VolleyTower
 * Purpose: The Volley Tower Class inherits the BaseTower
 *          class, to provide enhanced functionality
 *          based on the BaseTower class
 ****************************************************/
using System;
using System.Drawing;

namespace ShapesTD
{
    public class VolleyTower: BaseTower
    {
        private static Image img = Form1.volleytower;
        private Point loc;
        private static int shootRate = 50;
        private static int damage = 20;
        private static int radius = 46;
        private static int cost = 5000;
        private int cycle = 0;
        private static string type = "volley";
        
        public VolleyTower(int locX, int locY): base(img, locX, locY, type, shootRate, damage, radius, cost)
        {
            int tileX = locX / 32;
            int tileY = locY / 32;
            this.loc = new Point(tileX * 32, tileY * 32);
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
                if (be.GetHealth() <= 0)
                {
                    Form1.cash += be.GetReward();
                    be.Destroy();
                    break;
                }
                bool collision = false;
                int xDiff = Math.Abs(loc.X + 15 - be.GetLocation().X);
                int yDiff = Math.Abs(loc.Y + 15 - be.GetLocation().Y);
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        continue;
                    }

                    collision = true;
                }

                xDiff = Math.Abs(loc.X + 15 - (be.GetLocation().X + 31));
                yDiff = Math.Abs(loc.Y + 15 - be.GetLocation().Y);
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        continue;
                    }

                    collision = true;
                }

                xDiff = Math.Abs(loc.X + 15 - (be.GetLocation().X + 31));
                yDiff = Math.Abs(loc.Y + 15 - (be.GetLocation().Y + 31));
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        continue;
                    }

                    collision = true;
                }

                xDiff = Math.Abs(loc.X + 15 - be.GetLocation().X);
                yDiff = Math.Abs(loc.Y + 15 - (be.GetLocation().Y + 31));
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        continue;
                    }

                    collision = true;
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