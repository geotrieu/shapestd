using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesTD
{
    class BaseTower
    {
        private Image img;
        private Point loc;
        private int shootRate;
        private int damage;
        private int radius;
        private int cycle = 0;

        public BaseTower(Image img, Point loc, int shootRate = 9, int damage = 1, int radius = 50)
        {
            this.img = img;
            this.loc = loc;
            this.shootRate = shootRate;
            this.damage = damage;
            this.radius = radius;
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

        public void checkEnemies()
        {
            if (cycle >= shootRate)
            {
                foreach (BaseEnemy be in Form1.enemies)
                {
                    int xDiff = Math.Abs(loc.X - be.getLocation().X);
                    int yDiff = Math.Abs(loc.Y - be.getLocation().Y);
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        be.setHealth(be.getHealth() - damage);
                        Form1.cash++;
                        break;
                    }
                    xDiff = Math.Abs(loc.X - be.getLocation().X + 31);
                    yDiff = Math.Abs(loc.Y - be.getLocation().Y);
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        be.setHealth(be.getHealth() - damage);
                        Form1.cash++;
                        break;
                    }
                    xDiff = Math.Abs(loc.X - be.getLocation().X + 31);
                    yDiff = Math.Abs(loc.Y - be.getLocation().Y + 31);
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        be.setHealth(be.getHealth() - damage);
                        Form1.cash++;
                        break;
                    }
                    xDiff = Math.Abs(loc.X - be.getLocation().X);
                    yDiff = Math.Abs(loc.Y - be.getLocation().Y + 31);
                    if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                    {
                        be.setHealth(be.getHealth() - damage);
                        Form1.cash++;
                        break;
                    }
                    //else there is no collision
                }

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
            offscreen.FillEllipse(new SolidBrush(Color.FromArgb(175, 255, 51, 0)), (loc.X + 16 - radius), (loc.Y + 16 - radius), radius * 2, radius * 2);
        }
    }
}
