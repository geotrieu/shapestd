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
        public FreezeTower(int locX, int locY): base(img, locX, locY, type, shootRate, damage, radius, cost, sp)
        {
            int tileX = locX / 32;
            int tileY = locY / 32;
            this.loc = new Point(tileX * 32, tileY * 32);
        }
        
        public override void checkEnemies()
        {
            if (cycle >= shootRate)
            {
                foreach (BaseEnemy be in Form1.enemies)
                {
                    bool collision = false;
                    int xDiff = Math.Abs(loc.X + 15 - be.getLocation().X);
                    int yDiff = Math.Abs(loc.Y + 15 - be.getLocation().Y);
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        if (be.getFrozenTicks() <= 0)
                        {
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            be.setFrozenTicks(100);
                            break;
                        }

                        collision = true;
                    }
                    xDiff = Math.Abs(loc.X + 15 - (be.getLocation().X + 31));
                    yDiff = Math.Abs(loc.Y + 15 - be.getLocation().Y);
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        if (be.getFrozenTicks() <= 0)
                        {
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            be.setFrozenTicks(100);
                            break;
                        }
                        collision = true;
                    }
                    xDiff = Math.Abs(loc.X + 15 - (be.getLocation().X + 31));
                    yDiff = Math.Abs(loc.Y + 15 - (be.getLocation().Y + 31));
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        if (be.getFrozenTicks() <= 0)
                        {
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            be.setFrozenTicks(300);
                            break;
                        }
                        collision = true;
                    }
                    xDiff = Math.Abs(loc.X + 15 - be.getLocation().X);
                    yDiff = Math.Abs(loc.Y + 15 - (be.getLocation().Y + 31));
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        if (be.getFrozenTicks() <= 0)
                        {
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            be.setFrozenTicks(100);
                            break;
                        }
                        collision = true;
                    }

                    if (!collision)
                    {
                        //else there is no collision
                        //Checks if the enemy has left the radius
                        if (Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Remove(BasePair.FindBasePair(Form1.shootingAt, this, be));
                        }
                    }
                }

                cycle = 0;
            }
            else
            {
                cycle++;
            }
        }
    }
}