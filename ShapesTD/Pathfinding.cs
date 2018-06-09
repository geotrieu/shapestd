/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: Pathfinding Class
 * Purpose: To move the enemies along the dynamic path.
 ****************************************************/
using System;
using System.Drawing;

namespace ShapesTD
{
    public class Pathfinding
    {
        private static int[] scanx = new int[4] { 1, 0, -1, 0 };
        private static int[] scany = new int[4] {0, 1, 0, -1};
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: MovePath Function
        * Purpose: Every tick, this function is called to move each
        *          enemy along the dynamic path.
        * Inputs: None
        * Returns: None
        ****************************************************/
        public static void MovePath()
        {
            main:
            foreach (BaseEnemy be in Form1.enemies)
            {
                /* Last Direction:
                * 0: Right
                * 1: Down
                * 2: Left
                * 3: Up */
                
                //Check if Enemy is frozen
                if (be.GetFrozenTicks() > 0)
                {
                    be.SetFrozenTicks(be.GetFrozenTicks() - 1);
                    continue;
                }
                
                for (int i = 0; i < 4; i++)
                {
                    int locX = be.GetLocation().X + 16; 
                    int locY = be.GetLocation().Y + 16;
                    if (Form1.levelMap[(locX - (16 * scanx[be.GetLastDir()]) - ((be.GetLastDir() == 2) ? 1 : 0)) / 32 + scanx[i],
                            (locY - (16 * scany[be.GetLastDir()]) - ((be.GetLastDir() == 3) ? 1 : 0)) / 32 + scany[i]] == '1')
                    {
                        if (!be.IsVisited((locX - (16 * scanx[be.GetLastDir()]) - ((be.GetLastDir() == 2) ? 1 : 0)) / 32 + scanx[i],
                            (locY - (16 * scany[be.GetLastDir()]) - ((be.GetLastDir() == 3) ? 1 : 0)) / 32 + scany[i]))
                        {
                            int moveSpeed = 0;
                            if (Form1.levelMap[
                                    (locX - (16 * scanx[be.GetLastDir()]) + (scanx[i] * be.GetSpeed()) -
                                     ((be.GetLastDir() == 2) ? 1 : 0)) / 32 + scanx[i],
                                    (locY - (16 * scany[be.GetLastDir()]) + (scany[i] * be.GetSpeed()) -
                                     ((be.GetLastDir() == 3) ? 1 : 0)) / 32 + scany[i]] == '1')
                            {
                                //Can use regular Speed
                                moveSpeed = be.GetSpeed();
                            }
                            else
                            {
                                //Need to reduce Speed
                                if (scanx[i] != 0)
                                {
                                    //Left/Right
                                    moveSpeed = (locX / 32 + ((i == 0) ? 1 : 0)) * 32 - locX - (scanx[i] * 16);
                                }
                                else
                                {
                                    //Up/Down
                                    moveSpeed = (locY / 32 + ((i == 1) ? 1 : 0)) * 32 - locY - (scany[i] * 16);
                                }
                            }

                            moveSpeed = Math.Abs(moveSpeed);
                            Point newLoc = new Point(be.GetLocation().X  + (moveSpeed * scanx[i]), be.GetLocation().Y + (moveSpeed * scany[i]));
                            be.AddVisited((locX - (16 * scanx[be.GetLastDir()])) / 32, (locY - (16 * scany[be.GetLastDir()])) / 32);
                            be.SetLocation(newLoc);
                            be.SetLastDir(i);
                            be.pixelsTraversed += moveSpeed;
                            break;
                        }
                    } else if (Form1.levelMap[
                                   (locX - (16 * scanx[be.GetLastDir()]) - ((be.GetLastDir() == 2) ? 1 : 0)) / 32 +
                                   scanx[i],
                                   (locY - (16 * scany[be.GetLastDir()]) - ((be.GetLastDir() == 3) ? 1 : 0)) / 32 +
                                   scany[i]] == 'E')
                    {
                        Form1.health -= be.GetDamage();
                        Form1.cash += be.GetReward();
                        be.Destroy();
                        //reduce lives here
                        goto main;
                    }
                }
            }
        }
    }
}