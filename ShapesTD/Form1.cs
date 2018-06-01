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
        public static Image bluerect = Image.FromFile("../../resources/bluerect.png");
        public static Image yellowtow = Image.FromFile("../../resources/yellowtow.png");
        public static Image homebase = Image.FromFile("../../resources/homebase.png");
        public static Image heart = Image.FromFile("../../resources/heart.png");
        public static Image coin = Image.FromFile("../../resources/coin.png");
        public static Image waveImg = Image.FromFile("../../resources/wave.png");
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
        public static int cash = 200;
        public static int wave = 0;
        public static int totalWaves = 0;

        //Backend Objects
        private MouseHandler mh;
        
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(width * 32, height * 32);
            this.Show();

            //Initialize Mouse Pointer
            //mh = new MouseHandler(PointToClient(Cursor.Position));
            
            if (!InitLevel(1))
            {
                Console.WriteLine("Level 1 failed to initialize.");
            }

            //TEMP Create a tower
            towers.Add(new BaseTower(yellowtow, new Point(96, 64)));
            towers.Add(new BaseTower(yellowtow, new Point(128, 192)));
            towers.Add(new BaseTower(yellowtow, new Point(256, 64)));
            towers.Add(new BaseTower(yellowtow, new Point(512 - 32, 64)));
            towers.Add(new BaseTower(bluerect, new Point(512 - 64, 256), 4, 3, 110));
            
            //Load Waves
            WaveHandler.loadWaveData();
            
            //create the onscreen graphics
            dc = this.CreateGraphics();

            // create a blank bitmap
            curBitmap = new Bitmap(width * 32, height * 32);

            //Create a temporary Graphics object from the bitmap
            offscreen = Graphics.FromImage(curBitmap);

        }
        
        //this code copies offscreen to onscreen
        private void timer1_Tick(object sender, EventArgs e)
        {
            Pathfinding.MovePath();
            GameConditions.checkEnemies();
            if (Form1.health <= 0)
            {
                //Game Over
                timer1.Enabled = false;
                MessageBox.Show("Game Over!");
                Application.Exit();
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
    }
}
