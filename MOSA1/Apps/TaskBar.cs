using Mosa.External.x86;
using Mosa.External.x86.Drawing.Fonts;
using Mosa.External.x86.Driver.Audio;
using Mosa.Kernel.x86;
using Mosa.Runtime;
using System.Drawing;

namespace MOSA1.Apps
{
    class TaskBar : Window
    {
        public TaskBar()
        {
            this.X = 0;
            this.Y = 0;

            this.Width = Program.ScreenWidth;

            Title = "ruby_taskbar";

            LiterallyWindow = true;

            Height = TaskBarHeight;
        }

        string s;
        string d;

        public const int TaskBarHeight = 30;

        public override void UIUpdate()
        {
            System.Graphics.DrawFilledRectangle(0x313131, X, Y, Width, Height);
            s = CMOS.Hour.ToString() + ":" + CMOS.Minute.ToString().PadLeft(2, '0');

            //System.Graphics.DrawFilledRoundedRectangle(Color.Black.ToArgb(), 6, 2, 75, 20, 0);
            System.Graphics.DrawBitFontString(System.DateAndTimeFont, Color.White.ToArgb(), s, (Width / 2) - (BitFont.Calculate(System.DateAndTimeFont, s) / 2), Y + (Height / 2 - 8));

            s.Dispose();
        }
    }
}
