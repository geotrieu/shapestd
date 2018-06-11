/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: GameConditions
 * Purpose: To check the conditions of the game:
 *          - Each tower checks if an enemy has entered
 *            its radius
 *          - Checks the health of the player, and display
 *            game over if health is 0 or less
 ****************************************************/
using System.Windows.Forms;

namespace ShapesTD
{
    public class GameConditions
    {
        public static void CheckEnemies()
        {
            foreach (BaseTower bt in Form1.towers)
            {
                bt.CheckEnemies();
            }
        }

        public static void CheckHealth()
        {
            if (Form1.health <= 0)
            {
                //Game Over
                MessageBox.DisplayOneOptionMessage("Game Over!", "Wave: " + (Form1.wave + 1), "Exit");
            }
        }
    }
}