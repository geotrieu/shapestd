/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: FreezeTower
 * Purpose: The Freeze Tower Class inherits the BaseTower
 *          class, to provide enhanced functionality
 *          based on the BaseTower class
 ****************************************************/
using System;
using System.Drawing;
using System.Media;

namespace ShapesTD
{
    public class FreezeTower: BaseTower
    {
        private static Image img = Form1.freezetower;
        private Point loc;
        private static int shootRate = 150;
        private static int damage = 0;
        private static int radius = 80;
        private static int cost = 750;
        private int cycle = 0;
        private static string type = "freeze";
        private static SoundPlayer sp = Form1.freezeSound;
        
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: FreezeTower Constructor
        * Purpose: Used to create a new FreezeTower object that inherits its base class
        * Inputs: Image img
        *         int locX
        *         int locY
        *         string towerType
        *         (optional) int shootRate
        *         (optional) int damage
        *         (optional) int radius
        *         (optional) int cost
        *         (optional) SoundPlayer sp
        * Returns: None
        ****************************************************/
        public FreezeTower(int locX, int locY): base(img, locX, locY, type, shootRate, damage, radius, cost, sp)
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
                bool collision = false;
                int xDiff = Math.Abs(loc.X + 15 - be.GetLocation().X);
                int yDiff = Math.Abs(loc.Y + 15 - be.GetLocation().Y);
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        if (be.GetFrozenTicks() <= 0)
                        {
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            be.SetFrozenTicks(100);
                            break;
                        }
                    }

                    if (be.GetFrozenTicks() <= 0)
                    {
                        if (Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Remove(BasePair.FindBasePair(Form1.shootingAt, this, be));
                        }
                    }

                    collision = true;
                }

                xDiff = Math.Abs(loc.X + 15 - (be.GetLocation().X + 31));
                yDiff = Math.Abs(loc.Y + 15 - be.GetLocation().Y);
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        if (be.GetFrozenTicks() <= 0)
                        {
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            be.SetFrozenTicks(100);
                            break;
                        }
                    }
                    
                    if (be.GetFrozenTicks() <= 0)
                    {
                        if (Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Remove(BasePair.FindBasePair(Form1.shootingAt, this, be));
                        }
                    }

                    collision = true;
                }

                xDiff = Math.Abs(loc.X + 15 - (be.GetLocation().X + 31));
                yDiff = Math.Abs(loc.Y + 15 - (be.GetLocation().Y + 31));
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        if (be.GetFrozenTicks() <= 0)
                        {
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            be.SetFrozenTicks(100);
                            break;
                        }
                    }
                    
                    if (be.GetFrozenTicks() <= 0)
                    {
                        if (Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Remove(BasePair.FindBasePair(Form1.shootingAt, this, be));
                        }
                    }

                    collision = true;
                }

                xDiff = Math.Abs(loc.X + 15 - be.GetLocation().X);
                yDiff = Math.Abs(loc.Y + 15 - (be.GetLocation().Y + 31));
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        if (be.GetFrozenTicks() <= 0)
                        {
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            be.SetFrozenTicks(100);
                            break;
                        }
                    }
                    
                    if (be.GetFrozenTicks() <= 0)
                    {
                        if (Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Remove(BasePair.FindBasePair(Form1.shootingAt, this, be));
                        }
                    }

                    collision = true;
                }

                //else there is no collision
                //Checks if the enemy has left the radius
                if (!collision)
                {
                    if (Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                    {
                        Form1.shootingAt.Remove(BasePair.FindBasePair(Form1.shootingAt, this, be));
                    }
                }
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