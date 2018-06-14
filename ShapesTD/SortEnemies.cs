/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: SortEnemies
 * Purpose: Every x amount of ticks, the enemies are reordered
 *          from based on where they are on the path. The closer
 *          to the exit they are, the earlier in the ArrayList
 *          they appear. Uses Bubble Sort.
 ****************************************************/
namespace ShapesTD
{
    public class SortEnemies
    {
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: ReorderEnemies
        * Purpose: Uses bubble sort to sort the enemies based
        *          on how far along they are on the map.
        * Inputs: none
        * Returns: none
        ****************************************************/
        public static void ReorderEnemies()
        {
            BaseEnemy temp;
            for (int i = 0; i < Form1.enemies.Count - 1; i++)
            {
                for (int j = 0; j < Form1.enemies.Count - 1; j++)
                {
                    if (((BaseEnemy) Form1.enemies[j]).pixelsTraversed <
                        ((BaseEnemy) Form1.enemies[j + 1]).pixelsTraversed)
                    {
                        temp = ((BaseEnemy)Form1.enemies[j + 1]);
                        Form1.enemies[j + 1] = Form1.enemies[j];
                        Form1.enemies[j] = temp;
                    }
                }
            }
        }
    }
}