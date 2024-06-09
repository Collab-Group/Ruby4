using Mosa.External.x86.Drawing.Fonts;
using Mosa.External.x86.Driver;
using Mosa.Kernel.x86;
using Mosa.Runtime;
using Mosa.Runtime.x86;

namespace MOSA1.Apps
{
    class Notepad : Window
    {
        public string Content = "";
        public string aContent = "";

        public unsafe Notepad()
        {
            Title = "Notepad";

            this.ToolBarColor = 0xFFFEEE9E;
        }

        KeyCode KeyCode;

        public override void InputUpdate()
        {
            KeyCode = Console.ReadKey(false);


            if (KeyCode == KeyCode.Delete)
            {
                CursorX = 0;
                virtualGraphics.DrawFilledRectangle(0xFFFEEE9E, CursorX, CursorY, Width, 16);
            }
            else if (KeyCode == KeyCode.Enter)
            {
                WriteLine();
            }
            else
            {
                if (PS2Keyboard.IsCapsLock)
                {
                    Write(KeyCode.KeyCodeToString());
                }
                else
                {
                    Write(KeyCode.KeyCodeToString().ToLower());
                }
            }
        }

        public int CursorX = 0;
        public int CursorY = 0;

        public void Write(string s)
        {
            if (CursorY + 16 > Height)
            {
                CursorY -= 16;
                ASM.MEMCPY(virtualGraphics.VideoMemoryCacheAddr, (uint)(virtualGraphics.VideoMemoryCacheAddr + (16 * Width * 4)), (uint)(Width * (Height - 16) * 4));
                virtualGraphics.DrawFilledRectangle(0xFFFEEE9E, 0, Height - 16, Width, 16);
            }

            if (virtualGraphics == null) return;

            CursorX += virtualGraphics.DrawBitFontString(System.DefaultFontName, 0X0, s, CursorX, CursorY);

            if (CursorX > Width)
            {
                CursorX = 0;
                CursorY += 16;
            }
        }

        public void WriteLine(string s)
        {
            Write(s);
            s.Dispose();
            CursorX = 0;
            CursorY += 16;
        }

        public void WriteLine()
        {
            CursorX = 0;
            CursorY += 16;
        }

        public override void UIUpdate()
        {
            if (virtualGraphics == null)
            {
                virtualGraphics = new Mosa.External.x86.Drawing.VirtualGraphics(this.Width, this.Height);
                virtualGraphics.DrawFilledRectangle(0xFFFEEE9E, 0, 0, Width, Height);

                CursorX = 0;
                CursorY = 0;
            }

            System.Graphics.DrawImageASM(virtualGraphics.bitmap, X, Y);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "_", X + CursorX, Y + CursorY);
        }
    }
}
