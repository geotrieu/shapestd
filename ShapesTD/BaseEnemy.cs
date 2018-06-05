using System;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesTD
{
    public class BaseEnemy
    {
        private Image image;
        private int maxHealth;
        private int health;
        private int speed;
        private int dmg;
        private Point loc = new Point();
        private bool[,] visited = new bool[Form1.width, Form1.height];
        private int lastDir = 0;
        private bool isFrozen = false;
        private int frozenTicks = 0;
        public int pixelsTraversed;
        private int reward;

        public BaseEnemy(Image img, Point loc, int maxHealth = 100, int speed = 1, int dmg = 1, int reward = 10)
        {
            this.image = img;
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.speed = speed;
            this.loc = loc;
            this.dmg = dmg;
            this.reward = reward;
        }

        public BaseEnemy(Image img, int x, int y, int maxHealth = 100, int speed = 1, int dmg = 1, int reward = 10)
        {
            this.image = img;
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.speed = speed;
            this.loc = new Point(x, y);
            this.dmg = dmg;
            this.reward = reward;
        }

        //GET FUNCTIONS
        public int getDamage()
        {
            return dmg;
        }
        public int getMaxHealth()
        {
            return maxHealth;
        }
        public int getHealth()
        {
            return health;
        }

        public int getSpeed()
        {
            return speed;
        }

        public Point getLocation()
        {
            return loc;
        }
        
        public int getLastDir()
        {
            return lastDir;
        }

        public bool IsFrozen()
        {
            return isFrozen;
        }
        
        public bool isVisited(int tileX, int tileY)
        {
            return (visited[tileX, tileY] ? true : false);
        }

        public int getFrozenTicks()
        {
            return frozenTicks;
        }

        public int GetReward()
        {
            return reward;
        }

        //SET FUNCTIONS
        public void addVisited(int tileX, int tileY)
        {
            visited[tileX, tileY] = true;
        }

        public void setHealth(int health)
        {
            this.health = health;
        }
        
        public void setLocation(Point pt)
        {
            loc = new Point(pt.X, pt.Y);
        }

        public void setLastDir(int i)
        {
            lastDir = i;
        }

        public void setFreeze(bool b)
        {
            isFrozen = b;
        }

        public void destroyEnemy()
        {
            ArrayList al = BasePair.FindBasePair(Form1.shootingAt, this);
            foreach (BasePair bp in al)
            {
                Form1.shootingAt.Remove(bp);
            }
            Form1.enemies.Remove(this);
        }

        public void setFrozenTicks(int x)
        {
            frozenTicks = x;
        }
        
        //DRAW FUNCTIONS
        public void drawEnemy(ref Graphics offscreen)
        {
            offscreen.DrawImage(image, loc);
            //Health Bar
            offscreen.DrawRectangle(new Pen(Color.Black), loc.X + 1, loc.Y - 9, 30, 6);
            offscreen.FillRectangle(new SolidBrush(Color.Red), loc.X + 2, loc.Y - 8, 28, 4);
            offscreen.FillRectangle(new SolidBrush(Color.LimeGreen), loc.X + 2, loc.Y - 8, (int) (28 * health / maxHealth), 4);
            //Debug
            /*
            offscreen.FillEllipse(new SolidBrush(Color.Blue), loc.X, loc.Y, 6, 6);
            offscreen.FillEllipse(new SolidBrush(Color.Blue), loc.X + 31, loc.Y, -6, 6);
            offscreen.FillEllipse(new SolidBrush(Color.Blue), loc.X + 31, loc.Y + 31, -6, -6);
            offscreen.FillEllipse(new SolidBrush(Color.Blue), loc.X, loc.Y + 31, 6, -6);
            */
        }
    }
}
