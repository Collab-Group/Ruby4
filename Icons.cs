using Mosa.External.x86;
using Mosa.External.x86.Drawing;

namespace MOSA1
{
    public class Icons
    {
        public static Bitmap Start = null;
        public static Bitmap Calc = null;
        public static Bitmap Paint = null;
        public static Bitmap Shutdown = null;
        public static Bitmap Snake = null;
        public static Bitmap Terminal = null;
        public static Bitmap Notepad = null;

        public static void Initialize()
        {
            Start = new Bitmap(ResourceManager.GetObject("Start.bmp"));
            Calc = new Bitmap(ResourceManager.GetObject("Calc.bmp"));
            Paint = new Bitmap(ResourceManager.GetObject("Paint.bmp"));
            Shutdown = new Bitmap(ResourceManager.GetObject("Shutdown.bmp"));
            Snake = new Bitmap(ResourceManager.GetObject("Snake.bmp"));
            Terminal = new Bitmap(ResourceManager.GetObject("Terminal.bmp"));
        }
    }
}
