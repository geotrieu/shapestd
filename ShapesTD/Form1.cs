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
        public static Image grass = Image.FromFile("../../resources/grass.png");
        public static Image dirt = Image.FromFile("../../resources/dirt.png");
        public static Image steel = Image.FromFile("../../resources/steel.png");
        public static Image bluecircle = Image.FromFile("../../resources/bluecircle.png");
        public static Image greencircle = Image.FromFile("../../resources/greencircle.png");
        public static Image yellowcircle = Image.FromFile("../../resources/yellowcircle.png");
        public static Image orangecircle = Image.FromFile("../../resources/orangecircle.png");
        public static Image redcircle = Image.FromFile("../../resources/redcircle.png");
        public static Image purplecircle = Image.FromFile("../../resources/purplecircle.png");
        public static Image whitecircle = Image.FromFile("../../resources/whitecircle.png");
        public static Image bluetri = Image.FromFile("../../resources/bluetri.png");
        public static Image greentri = Image.FromFile("../../resources/greentri.png");
        public static Image yellowtri = Image.FromFile("../../resources/yellowtri.png");
        public static Image orangetri = Image.FromFile("../../resources/orangetri.png");
        public static Image redtri = Image.FromFile("../../resources/redtri.png");
        public static Image purpletri = Image.FromFile("../../resources/purpletri.png");
        public static Image whitetri = Image.FromFile("../../resources/whitetri.png");
        public static Image bluerect = Image.FromFile("../../resources/bluerect.png");
        public static Image greenrect = Image.FromFile("../../resources/greenrect.png");
        public static Image yellowrect = Image.FromFile("../../resources/yellowrect.png");
        public static Image orangerect = Image.FromFile("../../resources/orangerect.png");
        public static Image redrect = Image.FromFile("../../resources/redrect.png");
        public static Image purplerect = Image.FromFile("../../resources/purplerect.png");
        public static Image whiterect = Image.FromFile("../../resources/whiterect.png");
        public static Image bullettower = Image.FromFile("../../resources/bullettower.png");
        public static Image lasertower = Image.FromFile("../../resources/lasertower.png");
        public static Image freezetower = Image.FromFile("../../resources/freezetower.png");
        public static Image homebase = Image.FromFile("../../resources/homebase.png");
        public static Image heart = Image.FromFile("../../resources/heart.png");
        public static Image coin = Image.FromFile("../../resources/coin.png");
        public static Image waveImg = Image.FromFile("../../resources/wave.png");
        public static Image start = Image.FromFile("../../resources/play.png");
        public static Font defFont = new Font(FontFamily.GenericMonospace, 10);
        public static Font bigFont = new Font(FontFamily.GenericMonospace, 100);

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
        public static int cash = 20000;
        public static int wave = 0;
        public static int totalWaves = 0;
        public static bool gameStarted = false;
        public static ArrayList shootingAt = new ArrayList();
        
        //Mouse Variables
        public static int mouseX = 0;
        public static int mouseY = 0;
        
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(width * 32, height * 32);
            this.Show();
            
            if (!InitLevel(1))
            {
                Console.WriteLine("Level 1 failed to initialize.");
            }

            //TEMP Create a tower
            /*towers.Add(new BaseTower(yellowtow, 96, 64));
            towers.Add(new BaseTower(yellowtow, 128, 192));
            towers.Add(new BaseTower(yellowtow, 256, 64));
            towers.Add(new BaseTower(yellowtow, 512 - 32, 64));
            towers.Add(new BaseTower(bluerect, 512 - 64, 256, 4, 3, 100));*/
            
            //Load Waves
            WaveHandler.loadWaveData();
            
            //create the onscreen graphics
            dc = this.CreateGraphics();

            // create a blank bitmap
            curBitmap = new Bitmap(width * 32, height * 32);

            //Create a temporary Graphics object from the bitmap
            offscreen = Graphics.FromImage(curBitmap);

        }
        
        private int sortEnemiesInterval = 1;
        private int currentTick = 0;
        //this code copies offscreen to onscreen
        private void timer1_Tick(object sender, EventArgs e)
        {
            Pathfinding.MovePath();
            GameConditions.checkEnemies();
            GameConditions.checkHealth(this);
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
                WaveHandler.waveTick();
            DrawGraphics.drawEveryTick();
        }

        public bool InitLevel(int level)
        {
            try
            {
                using (StreamReader sr = File.OpenText("../../resources/level" + level + ".dat"))
                {
                    int i = 0;
                    while (sr.Peek() >= 0)
                    {
                        string str = sr.ReadLine();
                        int j = 0;
                        foreach (char c in str.ToCharArray())
                        {
                            levelMap[j, i] = c;
                            j++;
                        }

                        i++;
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
            //Click Controllers
            if (mouseX >= ShopControl.bulletTower.X && mouseX <= (ShopControl.bulletTower.X + 31))
            {
                if (mouseY >= ShopControl.bulletTower.Y && mouseY <= (ShopControl.bulletTower.Y + 31))
                {
                    if (pickedUp == null)
                    {
                        if (cash >= 100)
                        {
                            pickedUp = "bullettower";
                            return;
                        }
                    }
                }
            }
            if (mouseX >= ShopControl.laserTower.X && mouseX <= (ShopControl.laserTower.X + 31))
            {
                if (mouseY >= ShopControl.laserTower.Y && mouseY <= (ShopControl.laserTower.Y + 31))
                {
                    if (pickedUp == null)
                    {
                        if (cash >= 300)
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
                        if (cash >= 300)
                        {
                            pickedUp = "freezetower";
                            return;
                        }
                    }
                }
            }
            if (mouseX >= ShopControl.startButton.X && mouseX <= (ShopControl.startButton.X + 15))
            {
                if (mouseY >= ShopControl.startButton.Y && mouseY <= (ShopControl.startButton.Y + 15))
                {
                    if (gameStarted == false)
                    {
                        gameStarted = true;
                    }
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
                    if (bt.getLocation().X == (tileX * 32))
                    {
                        if (bt.getLocation().Y == (tileY * 32))
                        {
                            valid = false;
                            break;
                        }
                    }
                }

                if (valid)
                {
                    BaseTower bt = null;
                    if (pickedUp == "bullettower")
                    {
                        bt = new BaseTower(bullettower, tileX * 32, tileY * 32);
                        towers.Add(bt);
                    }
                    else if (pickedUp == "lasertower")
                    {
                        bt = new BaseTower(lasertower, tileX * 32, tileY * 32, 1, 1, 30, 300, "laser");
                        towers.Add(bt);
                    }
                    else if (pickedUp == "freezetower")
                    {
                        bt = new FreezeTower(tileX * 32, tileY * 32);
                        towers.Add(bt);
                    }

                    pickedUp = null;
                    cash -= bt.getCost();
                }
            }
        }
    }
}
