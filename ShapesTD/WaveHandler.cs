using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace ShapesTD.resources
{
    public class WaveHandler
    {
        public struct wave
        {
            private ArrayList enemies;
            private ArrayList quantities;
            private int waveInterval;

            public wave(ArrayList enemies, ArrayList quantities, int waveInterval)
            {
                this.enemies = enemies;
                this.quantities = quantities;
                this.waveInterval = waveInterval;
            }

            public int getWaveInterval()
            {
                return waveInterval;
            }

            public string getNextEnemy()
            {
                string enemy = (string) enemies[0];
                enemies.RemoveAt(0);
                return enemy;
            }

            public int getNextQuantity()
            {
                int quantity = (int) quantities[0];
                quantities.RemoveAt(0);
                return quantity;
            }

            public bool isFinished()
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
                    MessageBox.Show("Error: WaveHandler.wave.isFinished()");
                }

                return false;
            }
        }
        public static void loadWaveData()
        {
            int totalWaves = 0;
            try
            {
                using (StreamReader sr = File.OpenText("../../resources/levelWaves.dat"))
                {
                    while (sr.Peek() >= 0)
                    {
                        char[] chrArray = sr.ReadLine().ToCharArray();
                        totalWaves++;
                        if (chrArray[0] != '*')
                        {
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
                            Form1.waves.Add(new wave(enemies, quantities, int.Parse(waveInterval)));
                        }
                    }
                }
                Form1.totalWaves = totalWaves;
            }
            catch (System.IO.FileLoadException)
            {
                MessageBox.Show("Error: FileLoadException on WaveHandler.loadWaveData()");
            }
        }

        private static int interval = 0;
        private static int waveInterval = 1;
        private static string currentEnemy = null;
        private static int remainingQuantity = 0;
        private static int intervalBetweenWaves = 200;
        private static int currIntervalBetweenWaves = 0;
        private static bool betweenWaves;
        
        public static void waveTick()
        {
            if (!betweenWaves && Form1.gameStarted)
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
                                Form1.enemies.Add(new BaseEnemy(Form1.greencircle, Form1.startPos, 30, 2, 2));
                                break;
                            case "yellowcircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.yellowcircle, Form1.startPos, 45, 3, 3));
                                break;
                            case "orangecircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.orangecircle, Form1.startPos, 60, 3, 3));
                                break;
                            case "redcircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.redcircle, Form1.startPos, 75, 3, 3));
                                break;
                            case "purplecircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.purplecircle, Form1.startPos, 90, 4, 3));
                                break;
                            case "whitecircle":
                                Form1.enemies.Add(new BaseEnemy(Form1.whitecircle, Form1.startPos, 2000, 1, 10));
                                break;
                            case "bluetri":
                                Form1.enemies.Add(new BaseEnemy(Form1.bluetri, Form1.startPos, 60, 1, 3));
                                break;
                            case "greentri":
                                Form1.enemies.Add(new BaseEnemy(Form1.greentri, Form1.startPos, 120, 2, 3));
                                break;
                            case "yellowtri":
                                Form1.enemies.Add(new BaseEnemy(Form1.yellowtri, Form1.startPos, 180, 3, 3));
                                break;
                            case "orangetri":
                                Form1.enemies.Add(new BaseEnemy(Form1.orangetri, Form1.startPos, 240, 4, 4));
                                break;
                            case "redtri":
                                Form1.enemies.Add(new BaseEnemy(Form1.redtri, Form1.startPos, 300, 4, 4));
                                break;
                            case "purpletri":
                                Form1.enemies.Add(new BaseEnemy(Form1.purpletri, Form1.startPos, 360, 5, 4));
                                break;
                            case "whitetri":
                                Form1.enemies.Add(new BaseEnemy(Form1.whitetri, Form1.startPos, 4000, 1, 25));
                                break;
                            case "bluerect":
                                Form1.enemies.Add(new BaseEnemy(Form1.bluerect, Form1.startPos, 240, 2, 4));
                                break;
                            case "greenrect":
                                Form1.enemies.Add(new BaseEnemy(Form1.greenrect, Form1.startPos, 580, 3, 4));
                                break;
                            case "yellowrect":
                                Form1.enemies.Add(new BaseEnemy(Form1.yellowrect, Form1.startPos, 820, 4, 4));
                                break;
                            case "orangerect":
                                Form1.enemies.Add(new BaseEnemy(Form1.orangerect, Form1.startPos, 1060, 4, 5));
                                break;
                            case "redrect":
                                Form1.enemies.Add(new BaseEnemy(Form1.redrect, Form1.startPos, 1300, 5, 5));
                                break;
                            case "purplerect":
                                Form1.enemies.Add(new BaseEnemy(Form1.purplerect, Form1.startPos, 1540, 6, 6));
                                break;
                            case "whiterect":
                                Form1.enemies.Add(new BaseEnemy(Form1.whiterect, Form1.startPos, 8000, 1, 50));
                                break;
                            default:
                                MessageBox.Show("Error: Type received at Form1.timerTick Wave Handler, is invalid.");
                                break;
                        }
                        remainingQuantity--;
                    }
                    else
                    {
                        if (((WaveHandler.wave) Form1.waves[Form1.wave]).isFinished())
                        {
                            //MessageBox.Show("Wave " + wave);
                            Form1.wave++;
                            betweenWaves = true;
                        }
                        //Get next enemy type and quantity
                        waveInterval = ((WaveHandler.wave) Form1.waves[Form1.wave]).getWaveInterval();
                        currentEnemy = ((WaveHandler.wave) Form1.waves[Form1.wave]).getNextEnemy();
                        remainingQuantity = ((WaveHandler.wave) Form1.waves[Form1.wave]).getNextQuantity();
                        DrawGraphics.drawPopUpWave();
                    }

                    interval = 0;
                }
                else
                {
                    interval++;
                }
            }
            else
            {
                //Between Waves
                if (currIntervalBetweenWaves != intervalBetweenWaves)
                {
                    currIntervalBetweenWaves++;
                }
                else
                {
                    currIntervalBetweenWaves = 0;
                    betweenWaves = false;
                }
            }
        }
    }
}