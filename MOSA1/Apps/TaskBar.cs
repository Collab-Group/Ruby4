using Mosa.External.x86;
using Mosa.External.x86.Drawing.Fonts;
using Mosa.Kernel.x86;
using Mosa.Runtime;

namespace MOSA1.Apps
{
    class TaskBar : Window
    {
        public TaskBar()
        {
            this.X = 0;
            this.Y = 0;

            this.Width = Program.ScreenWidth;

            Title = "Dock";

            LiterallyWindow = true;

            Height = TaskBarHeight;
        }

        string s;

        public const int TaskBarHeight = 30;

        public override void UIUpdate()
        {
            System.Graphics.DrawFilledRectangle(0x313131, X, Y, Width, Height);
            s = CMOS.Hour.ToString() + ":" + CMOS.Minute.ToString().PadLeft(2, '0');

            System.Graphics.DrawBitFontString(System.DateAndTimeFont, 0xFFF0F3F4, s, (Width / 2) - (BitFont.Calculate(System.DateAndTimeFont, s) / 2), Y + (Height / 2 - 8));

            s.Dispose();
        }
    }
}
