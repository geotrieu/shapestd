using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ShapesTD
{
    public class BaseTower
    {
        private Image img;
        private Point loc;
        private int shootRate;
        private int damage;
        private int radius;
        private int cycle = 0;
        private int cost;
        private string towerType;
        private SoundPlayer sound;

        public BaseTower(Image img, int locX, int locY, string towerType, int shootRate = 30, int damage = 15,
            int radius = 80, int cost = 100, SoundPlayer sp = null)
        {
            this.img = img;
            int tileX = locX / 32;
            int tileY = locY / 32;
            this.loc = new Point(tileX * 32, tileY * 32);
            this.shootRate = shootRate;
            this.damage = damage;
            this.radius = radius;
            this.cost = cost;
            this.towerType = towerType;
            if (sp == null)
            {
                sound = Form1.silentSound;
            }
            else
            {
                sound = sp;
            }
        }

        public int getCost()
        {
            return cost;
        }
        
        public Point getLocation()
        {
            return loc;
        }

        public int getShootRate()
        {
            return shootRate;
        }

        public int getRadius()
        {
            return radius;
        }

        public string GetTowerType()
        {
            return towerType;
        }

        public virtual void checkEnemies()
        {
            
            //ToDo optomize checkEnemies, check whether there is more enemies or towers, and use that to check the rest.
                foreach (BaseEnemy be in Form1.enemies)
                {
                    if (be.getHealth() <= 0)
                    {
                        Form1.cash += be.GetReward();
                        be.destroyEnemy();

                        break;
                    }
                    int xDiff = Math.Abs(loc.X + 15 - be.getLocation().X);
                    int yDiff = Math.Abs(loc.Y + 15 - be.getLocation().Y);
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        if (cycle >= shootRate)
                        {
                            be.setHealth(be.getHealth() - damage);
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            sound.Play();
                        }

                        break;
                    }
                    xDiff = Math.Abs(loc.X + 15 - (be.getLocation().X + 31));
                    yDiff = Math.Abs(loc.Y + 15 - be.getLocation().Y);
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        if (cycle >= shootRate)
                        {
                            be.setHealth(be.getHealth() - damage);
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            sound.Play();
                        }

                        break;
                    }
                    xDiff = Math.Abs(loc.X + 15 - (be.getLocation().X + 31));
                    yDiff = Math.Abs(loc.Y + 15 - (be.getLocation().Y + 31));
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        if (cycle >= shootRate)
                        {
                            be.setHealth(be.getHealth() - damage);
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            sound.Play();
                        }

                        break;
                    }
                    xDiff = Math.Abs(loc.X + 15 - be.getLocation().X);
                    yDiff = Math.Abs(loc.Y + 15 - (be.getLocation().Y + 31));
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        if (cycle >= shootRate)
                        {
                            be.setHealth(be.getHealth() - damage);
                            if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                            {
                                Form1.shootingAt.Add(new BasePair(this, be));
                            }

                            sound.Play();
                        }

                        break;
                    }
                    //else there is no collision
                    //Checks if the enemy has left the radius
                    if (Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                    {
                        Form1.shootingAt.Remove(BasePair.FindBasePair(Form1.shootingAt, this, be));
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

        public void drawTower(ref Graphics offscreen)
        {
            offscreen.DrawImage(img, loc);
        }

        public void drawRadius(ref Graphics offscreen)
        {
            offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (loc.X + 15 - radius), (loc.Y + 15 - radius), radius * 2, radius * 2);
        }
    }
}
