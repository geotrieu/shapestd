namespace ShapesTD
{
    public class SortEnemies
    {
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