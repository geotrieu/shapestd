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
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: CheckEnemies
        * Purpose: For every tower, check if an enemy is in its
        *          range, and act upon it if so.
        * Inputs: none
        * Returns: nothing
        ****************************************************/
        public static void CheckEnemies()
        {
            foreach (BaseTower bt in Form1.towers)
            {
                bt.CheckEnemies();
            }
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: CheckHealth
        * Purpose: Checks the health of the player, if they
        *          have lost the game or not. If they have lost,
        *          display a custom MessageBox.
        * Inputs: none
        * Returns: nothing
        ****************************************************/
        public static void CheckHealth()
        {
            if (Form1.health <= 0)
            {
                //Game Over
                MessageBox.DisplayOneOptionMessage("Game Over!", "Wave: " + (Form1.wave + 1), "Exit");
            }
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: CheckWin
        * Purpose: Check if they have won the game. If so,
        *          display a custom Message Box.
        * Inputs: none
        * Returns: nothing
        ****************************************************/
        public static void CheckWin()
        {
            if (Form1.wave > Form1.totalWaves - 1)
            {
                MessageBox.DisplayOneOptionMessage("You Win!", "Money: $" + Form1.cash, "Exit");
            }
        }
    }
}