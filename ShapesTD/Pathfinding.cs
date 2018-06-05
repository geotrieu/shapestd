using System;
using System.Drawing;

namespace ShapesTD
{
    public class Pathfinding
    {
        private static int[] scanx = new int[4] { 1, 0, -1, 0 };
        private static int[] scany = new int[4] {0, 1, 0, -1};
        
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
                if (be.getFrozenTicks() > 0)
                {
                    be.setFrozenTicks(be.getFrozenTicks() - 1);
                    continue;
                }
                
                for (int i = 0; i < 4; i++)
                {
                    int locX = be.getLocation().X + 16; 
                    int locY = be.getLocation().Y + 16;
                    if (Form1.levelMap[(locX - (16 * scanx[be.getLastDir()]) - ((be.getLastDir() == 2) ? 1 : 0)) / 32 + scanx[i],
                            (locY - (16 * scany[be.getLastDir()]) - ((be.getLastDir() == 3) ? 1 : 0)) / 32 + scany[i]] == '1')
                    {
                        if (!be.isVisited((locX - (16 * scanx[be.getLastDir()]) - ((be.getLastDir() == 2) ? 1 : 0)) / 32 + scanx[i],
                            (locY - (16 * scany[be.getLastDir()]) - ((be.getLastDir() == 3) ? 1 : 0)) / 32 + scany[i]))
                        {
                            int moveSpeed = 0;
                            if (Form1.levelMap[
                                    (locX - (16 * scanx[be.getLastDir()]) + (scanx[i] * be.getSpeed()) -
                                     ((be.getLastDir() == 2) ? 1 : 0)) / 32 + scanx[i],
                                    (locY - (16 * scany[be.getLastDir()]) + (scany[i] * be.getSpeed()) -
                                     ((be.getLastDir() == 3) ? 1 : 0)) / 32 + scany[i]] == '1')
                            {
                                //Can use regular Speed
                                moveSpeed = be.getSpeed();
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
                            Point newLoc = new Point(be.getLocation().X  + (moveSpeed * scanx[i]), be.getLocation().Y + (moveSpeed * scany[i]));
                            be.addVisited((locX - (16 * scanx[be.getLastDir()])) / 32, (locY - (16 * scany[be.getLastDir()])) / 32);
                            be.setLocation(newLoc);
                            be.setLastDir(i);
                            be.pixelsTraversed += moveSpeed;
                            break;
                        }
                    } else if (Form1.levelMap[
                                   (locX - (16 * scanx[be.getLastDir()]) - ((be.getLastDir() == 2) ? 1 : 0)) / 32 +
                                   scanx[i],
                                   (locY - (16 * scany[be.getLastDir()]) - ((be.getLastDir() == 3) ? 1 : 0)) / 32 +
                                   scany[i]] == 'E')
                    {
                        Form1.health -= be.getDamage();
                        Form1.cash += be.GetReward();
                        be.destroyEnemy();
                        //reduce lives here
                        goto main;
                    }
                }
            }
        }
    }
}