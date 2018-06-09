/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: BaseTower
 * Purpose: The object type that control all the methods
 *          and variables of the Towers
 ****************************************************/
using System;
using System.Collections;
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

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: BaseTower Constructor
        * Purpose: Used to create a new BaseTower object
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
        
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: GetCost
        * Purpose: Method used to obtain the cost of the tower
        * Inputs: none
        * Returns: The cost of the tower (int)
        ****************************************************/
        public int GetCost()
        {
            return cost;
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: GetLocation
        * Purpose: Method used to obtain the location of the tower
        * Inputs: none
        * Returns: The location of the tower (Point)
        ****************************************************/
        public Point GetLocation()
        {
            return loc;
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: GetShootRate
        * Purpose: Method used to obtain the shoot rate of the tower
        * Inputs: none
        * Returns: The shoot rate of the tower (int)
        ****************************************************/
        public int GetShootRate()
        {
            return shootRate;
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: GetRadius
        * Purpose: Method used to obtain the radius of the tower
        * Inputs: none
        * Returns: The radius of the tower (int)
        ****************************************************/
        public int GetRadius()
        {
            return radius;
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: GetTowerType
        * Purpose: Method used to obtain the tower type
        * Inputs: none
        * Returns: The type of the tower (string)
        ****************************************************/
        public string GetTowerType()
        {
            return towerType;
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: CheckEnemies
        * Purpose: Function to detect enemy collisions, and act on them
        * Inputs: none
        * Returns: none
        ****************************************************/
        public virtual void CheckEnemies()
        {
            //ToDo optomize checkEnemies, check whether there is more enemies or towers, and use that to check the rest.
            foreach (BaseEnemy be in Form1.enemies)
            {
                bool collision = false;
                if (be.GetHealth() <= 0)
                {
                    Form1.cash += be.GetReward();
                    be.Destroy();

                    break;
                }

                int xDiff = Math.Abs(loc.X + 15 - be.GetLocation().X);
                int yDiff = Math.Abs(loc.Y + 15 - be.GetLocation().Y);
                if (Math.Pow(radius, 2) >= (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)))
                {
                    if (cycle >= shootRate)
                    {
                        be.SetHealth(be.GetHealth() - damage);
                        if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Add(new BasePair(this, be));
                        }

                        sound.Play();
                        break;
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
                        if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Add(new BasePair(this, be));
                        }

                        sound.Play();
                        break;
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
                        if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Add(new BasePair(this, be));
                        }

                        sound.Play();
                        break;
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
                        if (!Form1.shootingAt.Contains(BasePair.FindBasePair(Form1.shootingAt, this, be)))
                        {
                            Form1.shootingAt.Add(new BasePair(this, be));
                        }

                        sound.Play();
                        break;
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
        
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: DrawTower
        * Purpose: Function used to draw the graphics of the tower
        * Inputs: A reference to the Offscreen Graphics object
        * Returns: none
        ****************************************************/
        public void DrawTower(ref Graphics offscreen)
        {
            offscreen.DrawImage(img, loc);
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: Destroy
        * Purpose: Function used to destroy itself
        * Inputs: none
        * Returns: none
        ****************************************************/
        public void Destroy()
        {
            ArrayList al = BasePair.FindBasePair(Form1.shootingAt, this);
            foreach (BasePair bp in al)
            {
                Form1.shootingAt.Remove(bp);
            }
            Form1.towers.Remove(this);
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: DrawTower
        * Purpose: Function used to draw the radius of the tower
        * Inputs: A reference to the Offscreen Graphics object
        * Returns: none
        ****************************************************/
        public void DrawRadius(ref Graphics offscreen)
        {
            offscreen.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 100, 0)), (loc.X + 15 - radius),
                (loc.Y + 15 - radius), radius * 2, radius * 2);
        }
    }
}