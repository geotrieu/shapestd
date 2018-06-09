using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.Runtime.CompilerServices;
using ShapesTD.resources;

namespace ShapesTD
{
    public partial class Form1 : Form
    {
        //Graphics
        public static Graphics dc;
        public static Bitmap curBitmap;
        public static Graphics offscreen;
        public static readonly Image grass = Image.FromFile("../../resources/grass.png");
        public static readonly Image dirt = Image.FromFile("../../resources/dirt.png");
        public static readonly Image steel = Image.FromFile("../../resources/steel.png");
        public static readonly Image bluecircle = Image.FromFile("../../resources/bluecircle.png");
        public static readonly Image greencircle = Image.FromFile("../../resources/greencircle.png");
        public static readonly Image yellowcircle = Image.FromFile("../../resources/yellowcircle.png");
        public static readonly Image orangecircle = Image.FromFile("../../resources/orangecircle.png");
        public static readonly Image redcircle = Image.FromFile("../../resources/redcircle.png");
        public static readonly Image purplecircle = Image.FromFile("../../resources/purplecircle.png");
        public static readonly Image whitecircle = Image.FromFile("../../resources/whitecircle.png");
        public static readonly Image bluetri = Image.FromFile("../../resources/bluetri.png");
        public static readonly Image greentri = Image.FromFile("../../resources/greentri.png");
        public static readonly Image yellowtri = Image.FromFile("../../resources/yellowtri.png");
        public static readonly Image orangetri = Image.FromFile("../../resources/orangetri.png");
        public static readonly Image redtri = Image.FromFile("../../resources/redtri.png");
        public static readonly Image purpletri = Image.FromFile("../../resources/purpletri.png");
        public static readonly Image whitetri = Image.FromFile("../../resources/whitetri.png");
        public static readonly Image bluerect = Image.FromFile("../../resources/bluerect.png");
        public static readonly Image greenrect = Image.FromFile("../../resources/greenrect.png");
        public static readonly Image yellowrect = Image.FromFile("../../resources/yellowrect.png");
        public static readonly Image orangerect = Image.FromFile("../../resources/orangerect.png");
        public static readonly Image redrect = Image.FromFile("../../resources/redrect.png");
        public static readonly Image purplerect = Image.FromFile("../../resources/purplerect.png");
        public static readonly Image whiterect = Image.FromFile("../../resources/whiterect.png");
        public static readonly Image bluepent = Image.FromFile("../../resources/bluepentagon.png");
        public static readonly Image greenpent = Image.FromFile("../../resources/greenpentagon.png");
        public static readonly Image yellowpent = Image.FromFile("../../resources/yellowpentagon.png");
        public static readonly Image orangepent = Image.FromFile("../../resources/orangepentagon.png");
        public static readonly Image redpent = Image.FromFile("../../resources/redpentagon.png");
        public static readonly Image purplepent = Image.FromFile("../../resources/purplepentagon.png");
        public static readonly Image whitepent = Image.FromFile("../../resources/whitepentagon.png");
        public static readonly Image bullettower = Image.FromFile("../../resources/bullettower.png");
        public static readonly Image lasertower = Image.FromFile("../../resources/lasertower.png");
        public static readonly Image freezetower = Image.FromFile("../../resources/freezetower.png");
        public static readonly Image cannontower = Image.FromFile("../../resources/cannontower.png");
        public static readonly Image machineguntower = Image.FromFile("../../resources/machineguntower.png");
        public static readonly Image homebase = Image.FromFile("../../resources/homebase.png");
        public static readonly Image heart = Image.FromFile("../../resources/heart.png");
        public static readonly Image coin = Image.FromFile("../../resources/coin.png");
        public static readonly Image waveImg = Image.FromFile("../../resources/wave.png");
        public static readonly Image start = Image.FromFile("../../resources/play.png");
        public static readonly Image pause = Image.FromFile("../../resources/pause.png");
        public static readonly Image fast = Image.FromFile("../../resources/fast.png");
        public static readonly Image slow = Image.FromFile("../../resources/slow.png");
        public static readonly Image sell = Image.FromFile("../../resources/sell.png");
        public static readonly Font defFont = new Font(FontFamily.GenericMonospace, 10);
        public static readonly Font bigFont = new Font(FontFamily.GenericMonospace, 100);

        //Sound
        public static SoundPlayer silentSound = new SoundPlayer("../../resources/silent.wav");
        public static SoundPlayer freezeSound = new SoundPlayer("../../resources/freezesound.wav");
        public static SoundPlayer cannonSound = new SoundPlayer("../../resources/cannonsound.wav");

        //Entities
        public static ArrayList enemies = new ArrayList();
        public static ArrayList towers = new ArrayList();

        //Level Specs
        public static int width = 20;
        public static int height = 14;
        public static char[,] levelMap = new char[width, height];
        public static Point startPos = new Point(0, 32);
        public static ArrayList waves = new ArrayList();

        //Level Variables
        public static int health = 100;
        public static int cash = 500;
        public static int wave = 0;
        public static int totalWaves = 0;
        public static bool gameStarted = false;
        public static ArrayList shootingAt = new ArrayList();
        public static bool isFast = false;
        public static bool isPaused = false;

        //Mouse Variables
        public static int mouseX = 0;
        public static int mouseY = 0;

        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(width * 32, height * 32);
            this.Show();

            Random r = new Random();
            int rLevel = r.Next(1, 5);
            
            if (!InitLevel(rLevel))
            {
                Console.WriteLine("Level " + rLevel + " failed to initialize.");
            }

            //Load Waves
            WaveHandler.LoadWaveData();

            //create the onscreen graphics
            dc = this.CreateGraphics();

            // create a blank bitmap
            curBitmap = new Bitmap(width * 32, height * 32);

            //Create a temporary Graphics object from the bitmap
            offscreen = Graphics.FromImage(curBitmap);
        }

        private const int sortEnemiesInterval = 10;

        private int currentTick = 0;

        //this code copies offscreen to onscreen
        private void timer1_Tick(object sender, EventArgs e)
        {
            Pathfinding.MovePath();
            GameConditions.CheckEnemies();
            GameConditions.CheckHealth(this);
            //Sort Enemies every sortEnemiesInterval Seconds
            if (currentTick == sortEnemiesInterval)
            {
                SortEnemies.ReorderEnemies();
                currentTick = 0;
            }
            else
            {
                currentTick++;
            }

            //check if game is finished - aka you won- ToDo
            if (wave <= totalWaves - 1)
                WaveHandler.WaveTick();
            DrawGraphics.DrawEveryTick();
        }

        private static bool InitLevel(int level)
        {
            try
            {
                using (StreamReader sr = File.OpenText("../../resources/level" + level + ".dat"))
                {
                    int y = 0;
                    while (sr.Peek() >= 0)
                    {
                        string str = sr.ReadLine();
                        int x = 0;
                        foreach (char c in str.ToCharArray())
                        {
                            if (c == 'S')
                            {
                                startPos = new Point(x * 32, y * 32);
                            }

                            levelMap[x, y] = c;
                            x++;
                        }

                        y++;
                    }
                }
            }
            catch (System.IO.FileLoadException)
            {
                return false;
            }

            return true;
        }

        public static string pickedUp = null;
        public static BaseTower selectedTower = null;

        private void Form1_MouseHover(object sender, EventArgs e)
        {
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                //Click Controllers
                if (pickedUp == null)
                {
                    if (mouseX >= ShopControl.bulletTower.X && mouseX <= (ShopControl.bulletTower.X + 31))
                    {
                        if (mouseY >= ShopControl.bulletTower.Y && mouseY <= (ShopControl.bulletTower.Y + 31))
                        {
                            if (cash >= 100)
                            {
                                pickedUp = "bullettower";
                                return;
                            }
                        }
                    }

                    if (mouseX >= ShopControl.laserTower.X && mouseX <= (ShopControl.laserTower.X + 31))
                    {
                        if (mouseY >= ShopControl.laserTower.Y && mouseY <= (ShopControl.laserTower.Y + 31))
                        {
                            if (pickedUp == null)
                            {
                                if (cash >= 500)
                                {
                                    pickedUp = "lasertower";
                                    return;
                                }
                            }
                        }
                    }

                    if (mouseX >= ShopControl.freezeTower.X && mouseX <= (ShopControl.freezeTower.X + 31))
                    {
                        if (mouseY >= ShopControl.freezeTower.Y && mouseY <= (ShopControl.freezeTower.Y + 31))
                        {
                            if (pickedUp == null)
                            {
                                if (cash >= 750)
                                {
                                    pickedUp = "freezetower";
                                    return;
                                }
                            }
                        }
                    }

                    if (mouseX >= ShopControl.cannonTower.X && mouseX <= (ShopControl.cannonTower.X + 31))
                    {
                        if (mouseY >= ShopControl.cannonTower.Y && mouseY <= (ShopControl.cannonTower.Y + 31))
                        {
                            if (pickedUp == null)
                            {
                                if (cash >= 1250)
                                {
                                    pickedUp = "cannontower";
                                    return;
                                }
                            }
                        }
                    }

                    if (mouseX >= ShopControl.machineGunTower.X && mouseX <= (ShopControl.machineGunTower.X + 31))
                    {
                        if (mouseY >= ShopControl.machineGunTower.Y && mouseY <= (ShopControl.machineGunTower.Y + 31))
                        {
                            if (pickedUp == null)
                            {
                                if (cash >= 10000)
                                {
                                    pickedUp = "machineguntower";
                                    return;
                                }
                            }
                        }
                    }

                    if (mouseX >= ShopControl.startButton.X && mouseX <= (ShopControl.startButton.X + 31))
                    {
                        if (mouseY >= ShopControl.startButton.Y && mouseY <= (ShopControl.startButton.Y + 31))
                        {
                            if (gameStarted == false)
                            {
                                gameStarted = true;
                            }
                        }
                    }
                    
                    if (mouseX >= ShopControl.pauseButton.X && mouseX <= (ShopControl.pauseButton.X + 31))
                    {
                        if (mouseY >= ShopControl.pauseButton.Y && mouseY <= (ShopControl.pauseButton.Y + 31))
                        {
                            isPaused = !isPaused;
                            if (isPaused)
                            {
                                timer1.Enabled = false;
                            } else
                            {
                                timer1.Enabled = true;
                            }
                        }
                    }
                    
                    if (mouseX >= ShopControl.fastslowButton.X && mouseX <= (ShopControl.fastslowButton.X + 31))
                    {
                        if (mouseY >= ShopControl.fastslowButton.Y && mouseY <= (ShopControl.fastslowButton.Y + 31))
                        {
                            isFast = !isFast;
                            if (isFast)
                            {
                                gameStarted = true;
                            }
                        }
                    }

                    if (selectedTower != null)
                    {
                        if (mouseX >= ShopControl.sellButton.X && mouseX <= (ShopControl.sellButton.X + 63))
                        {
                            if (mouseY >= ShopControl.sellButton.Y && mouseY <= (ShopControl.sellButton.Y + 31))
                            {
                                cash += (selectedTower.GetCost() / 2);
                                selectedTower.Destroy();
                            }
                        }
                    }

                    bool towerFound = false;
                    foreach (BaseTower bt in towers)
                    {
                        if (mouseX >= bt.GetLocation().X && mouseX <= bt.GetLocation().X + 31)
                        {
                            if (mouseY >= bt.GetLocation().Y && mouseY <= bt.GetLocation().Y + 31)
                            {
                                selectedTower = bt;
                                towerFound = true;
                                break;
                            }
                        }
                    }

                    if (!towerFound)
                    {
                        selectedTower = null;
                    }
                }

                //PickedUp Controller
                if (pickedUp != null)
                {
                    int tileX = mouseX / 32;
                    int tileY = mouseY / 32;
                    bool valid = levelMap[tileX, tileY] == '0';
                    foreach (BaseTower bt in towers)
                    {
                        if (bt.GetLocation().X == (tileX * 32))
                        {
                            if (bt.GetLocation().Y == (tileY * 32))
                            {
                                valid = false;
                                break;
                            }
                        }
                    }

                    if (valid)
                    {
                        if (pickedUp == "bullettower")
                        {
                            if (cash >= 100)
                            {
                                towers.Add(new BaseTower(bullettower, tileX * 32, tileY * 32, "bullet"));
                                cash -= 100;
                            }
                        }
                        else if (pickedUp == "lasertower")
                        {
                            if (cash >= 500)
                            {
                                towers.Add(new BaseTower(lasertower, tileX * 32, tileY * 32, "laser", 1, 1, 46, 500));
                                cash -= 500;
                            }
                        }
                        else if (pickedUp == "freezetower")
                        {
                            if (cash >= 750)
                            {
                                towers.Add(new FreezeTower(tileX * 32, tileY * 32));
                                cash -= 750;
                            }
                        }
                        else if (pickedUp == "cannontower")
                        {
                            if (cash >= 1250)
                            {
                                towers.Add(new BaseTower(cannontower, tileX * 32, tileY * 32, "cannon", 50, 150, 112, 1250,
                                    cannonSound));
                                cash -= 1250;
                            }
                        }
                        else if (pickedUp == "machineguntower")
                        {
                            if (cash >= 10000)
                            {
                                towers.Add(new BaseTower(machineguntower, tileX * 32, tileY * 32, "machinegun", 1, 25, 46, 10000));
                                cash -= 10000;
                            }
                        }
                    }
                }
            }
            else if (e.Button.Equals(MouseButtons.Right))
            {
                if (pickedUp != null)
                {
                    pickedUp = null;
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}