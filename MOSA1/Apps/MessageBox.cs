using Mosa.External.x86.Drawing.Fonts;

namespace MOSA1.Apps
{
    public class MessageBox : Window
    {
        public string Info = "";

        public MessageBox()
        {
            Title = "MessageBox";
        }

        public void Show(Window sender, string s)
        {
            this.X = sender.X + 50;
            this.Y = sender.Y + 50;

            Info = s;
            System.MoveToEnd(this);
            Visible = true;
        }

        public void Show(int X, int Y, string s)
        {
            this.X = X - (Width / 2);
            this.Y = Y - (Height / 2);

            Info = s;
            System.MoveToEnd(this);
            Visible = true;
        }

        public override void UIUpdate()
        {
            if (!Actived) Visible = false;
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, Info, X, Y);
        }
    }
}
