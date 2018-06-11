using System.Drawing;
using System.Runtime.CompilerServices;

namespace ShapesTD
{
    public class MessageBox
    {
        public static bool messageActive;
        public static string mainMessage;
        public static string subMessage;
        public static string optionMessage;
        public static void DisplayOneOptionMessage(string mainMessage, string subMessage, string optionMessage)
        {
            messageActive = true;
            MessageBox.mainMessage = mainMessage;
            MessageBox.subMessage = subMessage;
            MessageBox.optionMessage = optionMessage;
        }

        public static void DrawMessageBox()
        {
            Form1.offscreen.FillRectangle(new SolidBrush(Color.Gold), 48, 48, Form1.width * 32 - 96,
                Form1.height * 32 - 160);
            Form1.offscreen.FillRectangle(new SolidBrush(Color.SpringGreen), 56, 56, Form1.width * 32 - 112,
                Form1.height * 32 - 176);
            Form1.offscreen.DrawString(MessageBox.mainMessage, Form1.main, new SolidBrush(Color.Crimson),
                (Form1.width * 32 - 112) / 2 + 56 -
                (Form1.offscreen.MeasureString(MessageBox.mainMessage, Form1.main).Width / 2), 64);
            Form1.offscreen.DrawString(MessageBox.subMessage, Form1.sub, new SolidBrush(Color.Crimson),
                (Form1.width * 32 - 112) / 2 + 56 -
                (Form1.offscreen.MeasureString(MessageBox.subMessage, Form1.sub).Width / 2), 128);
            Form1.offscreen.FillRectangle(new SolidBrush(Color.DarkGoldenrod),
                (Form1.width * 32 - 112) / 2 + 56 -
                (Form1.offscreen.MeasureString(MessageBox.optionMessage, Form1.option).Width / 2) - 8, 248,
                Form1.offscreen.MeasureString(MessageBox.optionMessage, Form1.option).Width + 16,
                Form1.offscreen.MeasureString(MessageBox.optionMessage, Form1.option).Height + 16);
            Form1.offscreen.DrawString(MessageBox.optionMessage, Form1.option, new SolidBrush(Color.Blue),
                (Form1.width * 32 - 112) / 2 + 56 -
                (Form1.offscreen.MeasureString(MessageBox.optionMessage, Form1.option).Width / 2), 256);   
        }
    }
}