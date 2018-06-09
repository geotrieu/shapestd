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
        private Point loc;
        private bool[,] visited = new bool[Form1.width, Form1.height];
        private int lastDir;
        private int frozenTicks;
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
        public int GetDamage()
        {
            return dmg;
        }
        public int GetMaxHealth()
        {
            return maxHealth;
        }
        public int GetHealth()
        {
            return health;
        }

        public int GetSpeed()
        {
            return speed;
        }

        public Point GetLocation()
        {
            return loc;
        }
        
        public int GetLastDir()
        {
            return lastDir;
        }
        
        public bool IsVisited(int tileX, int tileY)
        {
            return (visited[tileX, tileY] ? true : false);
        }

        public int GetFrozenTicks()
        {
            return frozenTicks;
        }

        public int GetReward()
        {
            return reward;
        }

        //SET FUNCTIONS
        public void AddVisited(int tileX, int tileY)
        {
            visited[tileX, tileY] = true;
        }

        public void SetHealth(int h)
        {
            health = h;
        }
        
        public void SetLocation(Point pt)
        {
            loc = new Point(pt.X, pt.Y);
        }

        public void SetLastDir(int i)
        {
            lastDir = i;
        }

        public void Destroy()
        {
            ArrayList al = BasePair.FindBasePair(Form1.shootingAt, this);
            foreach (BasePair bp in al)
            {
                Form1.shootingAt.Remove(bp);
            }
            Form1.enemies.Remove(this);
        }

        public void SetFrozenTicks(int x)
        {
            frozenTicks = x;
        }
        
        //DRAW FUNCTIONS
        public void DrawEnemy(ref Graphics offscreen)
        {
            //Sprite
            offscreen.DrawImage(image, loc);
            //Health Bar
            offscreen.DrawRectangle(new Pen(Color.Black), loc.X + 1, loc.Y - 9, 30, 6);
            offscreen.FillRectangle(new SolidBrush(Color.Red), loc.X + 2, loc.Y - 8, 28, 4);
            offscreen.FillRectangle(new SolidBrush(Color.LimeGreen), loc.X + 2, loc.Y - 8, (int) (28 * health / maxHealth), 4);
        }
    }
}
