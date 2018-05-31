using System.Windows.Forms;

namespace ShapesTD
{
    public class GameConditions
    {
        public static void checkEnemies()
        {
            foreach (BaseTower bt in Form1.towers)
            {
                bt.checkEnemies();
            }
        }
    }
}