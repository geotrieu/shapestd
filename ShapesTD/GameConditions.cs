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

        public static void CheckHealth(Form1 f1)
        {
            if (Form1.health <= 0)
            {
                //Game Over
                f1.timer1.Enabled = false;
                MessageBox.Show("Game Over!");
                Application.Exit();
            }
        }
    }
}