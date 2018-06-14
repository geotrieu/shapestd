/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: WaveHandler
 * Purpose: The WaveHandler class contains the wave structure,
 *          and also functions used to handle each wave
 *          in the "levelWaves.dat" file.
 ****************************************************/
using System.Collections;
using System.IO;

namespace ShapesTD.resources
{
    public class WaveHandler
    {
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-05
        * Title: Wave
        * Purpose: The wave struct is used to contain the
        *          properties of each wave. Contains all the
        *          enemies and their quantities in each wave,
        *          and also how sparse they should be spawned at.
        ****************************************************/
        public struct Wave
        {
            private ArrayList enemies;
            private ArrayList quantities;
            private int waveInterval;

            public Wave(ArrayList enemies, ArrayList quantities, int waveInterval)
            {
                this.enemies = enemies;
                this.quantities = quantities;
                this.waveInterval = waveInterval;
            }

            public int GetWaveInterval()
            {
                return waveInterval;
            }

            public string GetNextEnemy()
            {
                string enemy = (string) enemies[0];
                enemies.RemoveAt(0);
                return enemy;
            }

            public int GetNextQuantity()
            {
                int quantity = (int) quantities[0];
                quantities.RemoveAt(0);
                return quantity;
            }

            /*****************************************************
            * Name: George Trieu
            * Date: 2018-06-08
            * Title: Wave.IsFinished
            * Purpose: Method used to check if the wave is finished.
            *          A wave is defined finished if all the enemies
            *          have been pushed out. Therefore, this method
            *          checks if there is no more enemies and quantities
            *          remain in their respective ArrayLists.
            * Inputs: none
            * Returns: A boolean value of whether the wave is finished.
            ****************************************************/
            public bool IsFinished()
            {
                if (enemies.Count.Equals(0) && quantities.Count.Equals(0))
                {
                    return true;
                } else if (!enemies.Count.Equals(0) && !quantities.Count.Equals(0))
                {
                    return false;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Error: WaveHandler.wave.isFinished()");
                }

                return false;
            }
        }
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: LoadWaveData
        * Purpose: To read the data from the levelWaves.dat file,
        *          and create Wave strcts and store them in an
        *          ArrayList called "waves" in the Form1 class.
        * Inputs: none
        * Returns: none
        ****************************************************/
        public static void LoadWaveData()
        {
            int totalWaves = 0;
            try
            {
                using (StreamReader sr = File.OpenText("../../resources/levelWaves.dat"))
                {
                    while (sr.Peek() >= 0)
                    {
                        char[] chrArray = sr.ReadLine().ToCharArray();
                        if (chrArray[0] != '*')
                        {
                            totalWaves++;
                            ArrayList enemies = new ArrayList();
                            ArrayList quantities = new ArrayList();
                            string waveInterval = null;
                            string tempEnemy = null;
                            string tempQuantity = null;
                            int stage = 0;
                            //Not a comment
                            for (int i = 0; i < chrArray.Length; i++)
                            {
                                if (chrArray[i] == '!' || stage == 2)
                                {
                                    if (chrArray[i] == '!')
                                    {
                                        //CharArray Currently on !
                                        stage = 2;
                                    }
                                    else
                                    {
                                        waveInterval += chrArray[i].ToString();
                                    }
                                }
                                else if (chrArray[i] != ' ' && stage == 0)
                                {
                                    tempEnemy += chrArray[i].ToString();
                                }
                                else if (chrArray[i] != ' ' && stage == 1)
                                {
                                    tempQuantity += chrArray[i].ToString();
                                }
                                else if (stage % 2 == 1)
                                {
                                    //More enemies
                                    stage = 0;
                                    enemies.Add(tempEnemy);
                                    quantities.Add(int.Parse(tempQuantity));
                                    tempEnemy = null;
                                    tempQuantity = null;
                                }
                                else
                                {
                                    stage++;
                                }
                            }
                            //No more enemies
                            Form1.waves.Add(new Wave(enemies, quantities, int.Parse(waveInterval)));
                        }
                    }
                }
                Form1.totalWaves = totalWaves - 1;
            }
            catch (System.IO.FileLoadException)
            {
                System.Windows.Forms.MessageBox.Show("Error: FileLoadException on WaveHandler.loadWaveData()");
            }
        }

        private static int interval = 0;
        private static int waveInterval = 1;
        private static string currentEnemy = null;
        private static int remainingQuantity = 0;
        private static int intervalBetweenWaves = 600;
        private static int currIntervalBetweenWaves = 0;
        private static bool betweenWaves;
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: WaveTick
        * Purpose: Called every tick the timer is running.
        *          Has a interval and waveInterval values to
        *          only run every x amount of ticks. Used to
        *          spawn enemies according to their waves, by
        *          obtaining data from the ArrayList of waves,
        *          and calling methods within the wave struct.
        * Inputs: none
        * Returns: none
        ****************************************************/
        public static void WaveTick()
        {
            if (Form1.gameStarted)
            {
                if (interval == waveInterval)
                {
                    if (remainingQuantity > 0)
                    {
                        switch (currentEnemy)
                        {
                            case "bluecircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.bluecircle, Form1.startPos, 15));
                                break;
                            case "greencircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.greencircle, Form1.startPos, 30, 2, 2, 15));
                                break;
                            case "yellowcircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.yellowcircle, Form1.startPos, 45, 3, 3, 20));
                                break;
                            case "orangecircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.orangecircle, Form1.startPos, 60, 3, 3, 25));
                                break;
                            case "redcircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.redcircle, Form1.startPos, 75, 3, 3, 30));
                                break;
                            case "purplecircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.purplecircle, Form1.startPos, 90, 4, 3, 35));
                                break;
                            case "whitecircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.whitecircle, Form1.startPos, 2000, 1, 10, 100));
                                break;
                            case "bluetri":
                                Form1.enemies.Add(new BaseEnemy(Form1.bluetri, Form1.startPos, 60, 1, 3, 50));
                                break;
                            case "greentri":
                                Form1.enemies.Add(new BaseEnemy(Form1.greentri, Form1.startPos, 120, 2, 3, 60));
                                break;
                            case "yellowtri":
                                Form1.enemies.Add(new BaseEnemy(Form1.yellowtri, Form1.startPos, 180, 3, 3, 70));
                                break;
                            case "orangetri":
                                Form1.enemies.Add(new BaseEnemy(Form1.orangetri, Form1.startPos, 240, 4, 4, 80));
                                break;
                            case "redtri":
                                Form1.enemies.Add(new BaseEnemy(Form1.redtri, Form1.startPos, 300, 4, 4, 90));
                                break;
                            case "purpletri":
                                Form1.enemies.Add(new BaseEnemy(Form1.purpletri, Form1.startPos, 360, 5, 4, 100));
                                break;
                            case "whitetri":
                                Form1.enemies.Add(new BaseEnemy(Form1.whitetri, Form1.startPos, 4000, 1, 25, 1000));
                                break;
                            case "bluerect":
                                Form1.enemies.Add(new BaseEnemy(Form1.bluerect, Form1.startPos, 240, 2, 4, 150));
                                break;
                            case "greenrect":
                                Form1.enemies.Add(new BaseEnemy(Form1.greenrect, Form1.startPos, 580, 3, 5, 300));
                                break;
                            case "yellowrect":
                                Form1.enemies.Add(new BaseEnemy(Form1.yellowrect, Form1.startPos, 820, 4, 5, 400));
                                break;
                            case "orangerect":
                                Form1.enemies.Add(new BaseEnemy(Form1.orangerect, Form1.startPos, 1060, 4, 5, 500));
                                break;
                            case "redrect":
                                Form1.enemies.Add(new BaseEnemy(Form1.redrect, Form1.startPos, 1300, 5, 6, 600));
                                break;
                            case "purplerect":
                                Form1.enemies.Add(new BaseEnemy(Form1.purplerect, Form1.startPos, 1540, 6, 6, 750));
                                break;
                            case "whiterect":
                                Form1.enemies.Add(new BaseEnemy(Form1.whiterect, Form1.startPos, 8000, 1, 99, 5000));
                                break;
                            case "bluepent":
                                Form1.enemies.Add(new BaseEnemy(Form1.bluepent, Form1.startPos, 1060, 3, 5, 1000));
                                break;
                            case "greenpent":
                                Form1.enemies.Add(new BaseEnemy(Form1.greenpent, Form1.startPos, 1500, 3, 6, 1250));
                                break;
                            case "yellowpent":
                                Form1.enemies.Add(new BaseEnemy(Form1.yellowpent, Form1.startPos, 2000, 4, 8, 2500));
                                break;
                            case "orangepent":
                                Form1.enemies.Add(new BaseEnemy(Form1.orangepent, Form1.startPos, 3000, 5, 10, 3500));
                                break;
                            case "redpent":
                                Form1.enemies.Add(new BaseEnemy(Form1.redpent, Form1.startPos, 6000, 6, 20, 4500));
                                break;
                            case "purplepent":
                                Form1.enemies.Add(new BaseEnemy(Form1.purplepent, Form1.startPos, 10000, 7, 25, 6000));
                                break;
                            case "whitepent":
                                Form1.enemies.Add(new BaseEnemy(Form1.whitepent, Form1.startPos, 50000, 1, 100, 10000));
                                break;
                            case "bluehex":
                                Form1.enemies.Add(new BaseEnemy(Form1.bluehex, Form1.startPos, 3500, 3, 10, 1000));
                                break;
                            case "greenhex":
                                Form1.enemies.Add(new BaseEnemy(Form1.greenhex, Form1.startPos, 7000, 5, 15, 1250));
                                break;
                            case "yellowhex":
                                Form1.enemies.Add(new BaseEnemy(Form1.yellowhex, Form1.startPos, 8000, 7, 20, 2500));
                                break;
                            case "orangehex":
                                Form1.enemies.Add(new BaseEnemy(Form1.orangehex, Form1.startPos, 9000, 9, 25, 3500));
                                break;
                            case "redhex":
                                Form1.enemies.Add(new BaseEnemy(Form1.redhex, Form1.startPos, 10000, 11, 35, 4500));
                                break;
                            case "purplehex":
                                Form1.enemies.Add(new BaseEnemy(Form1.purplehex, Form1.startPos, 12000, 13, 40, 6000));
                                break;
                            case "whitehex":
                                Form1.enemies.Add(new BaseEnemy(Form1.whitehex, Form1.startPos, 100000, 2, 200, 10000));
                                break;
                            default:
                                System.Windows.Forms.MessageBox.Show("Error: Type received at Form1.timerTick Wave Handler, is invalid.");
                                break;
                        }
                        remainingQuantity--;
                    }
                    else
                    {
                        if (((WaveHandler.Wave) Form1.waves[Form1.wave]).IsFinished())
                        {
                            Form1.wave++;
                            betweenWaves = true;
                            if (!Form1.isFast)
                            {
                                Form1.gameStarted = false;    
                            }
                        }
                        //Get next enemy type and quantity
                        waveInterval = ((Wave) Form1.waves[Form1.wave]).GetWaveInterval();
                        currentEnemy = ((Wave) Form1.waves[Form1.wave]).GetNextEnemy();
                        remainingQuantity = ((Wave) Form1.waves[Form1.wave]).GetNextQuantity();
                    }

                    interval = 0;
                }
                else
                {
                    interval++;
                }
            }
        }
    }
}