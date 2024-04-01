using Mosa.External.x86.Drawing;
using Mosa.External.x86.Drawing.Fonts;
using Mosa.External.x86.Driver;
using Mosa.Runtime;
using MOSA1.Apps;
using System;

namespace MOSA1
{
    public class Window
    {
        public int Index = 0;

        public int X;
        public int Y;
        public int Width;
        public int Height;

        private static bool Move = false;
        private int OffsetX;
        private int OffsetY;

        public string Title = "Developer SDK Sample";
        public static int BarHeight = 20;

        public bool Visible = true;
        public bool LiterallyWindow = false;

        public int X_Dock = -1;
        public int Y_Dock = -1;

        public VirtualGraphics virtualGraphics;

        public bool HandleMouse = false;

        public Bitmap Icon = null;

        public uint ToolBarColor = 0xFFF0F3F4;

        public bool Actived
        {
            get
            {
                return Index == System.Windows.Count - 1;
            }
        }

        public Window()
        {
            this.X_Dock = -1;
            this.Y_Dock = -1;
        }

        public void Active()
        {
            System.MoveToEnd(this);
        }

        public void Update()
        {
            if (!Visible)
            {
                return;
            }

            if (!LiterallyWindow)
            {
                if (PS2Mouse.MouseStatus == Mosa.External.x86.Driver.Input.MouseStatus.Left)
                {
                    if (PS2Mouse.X > X && PS2Mouse.X < X + Width && PS2Mouse.Y > Y - BarHeight && PS2Mouse.Y < Y && !Move && !HandleMouse)
                    {
                        //Hide Button
                        if (PS2Mouse.X > X + Width - 15 && this.Actived && !LiterallyWindow)
                        {
                            System.Terminal.WriteLine("Close Button Clicked.");
                            System.Windows.Remove(this);
                            this.Dispose();
                            return;
                        }

                        Move = true;

                        OffsetX = PS2Mouse.X - X;
                        OffsetY = PS2Mouse.Y - Y;

                        //
                        System.MoveToEnd(this);
                    }
                }
                else
                {
                    Move = false;
                }

                if (Move && Actived)
                {
                    this.X = Math.Clamp(PS2Mouse.X - OffsetX, 0, Program.ScreenWidth - Width);
                    this.Y = Math.Clamp(PS2Mouse.Y - OffsetY, BarHeight + TaskBar.TaskBarHeight, Program.ScreenHeight - Height);
                }

                //Bar
                //System.Color3

                if (Actived)
                {
                    System.Graphics.DrawFilledRectangle(System.Color2, X + 0, (Y - BarHeight) + 5, Width, BarHeight - 5);
                    System.Graphics.DrawFilledRoundedRectangle(System.Color2, X + 0, (Y - BarHeight) + 0, Width, BarHeight, 5);

                    System.Graphics.DrawFilledRoundedRectangle(System.Color2, X + 0, Y + Height - 5, Width, BarHeight - 5, 5);
                    System.Graphics.DrawFilledRoundedRectangle(ToolBarColor, X + 1, Y + Height - 5, Width - 2, BarHeight - 5 - 1, 5);
                }
                else
                {
                    System.Graphics.DrawFilledRectangle(System.Color3, X + 0, (Y - BarHeight) + 5, Width, BarHeight - 5);
                    System.Graphics.DrawFilledRoundedRectangle(System.Color3, X + 0, (Y - BarHeight) + 0, Width, BarHeight, 5);

                    System.Graphics.DrawFilledRoundedRectangle(System.Color3, X + 0, Y + Height - 5, Width, BarHeight - 5, 5);
                    System.Graphics.DrawFilledRoundedRectangle(ToolBarColor, X + 1, Y + Height - 5, Width - 2, BarHeight - 5 - 1, 5);
                }
                System.Graphics.DrawBitFontString(System.DefaultFontName, 0xFFF0F3F4, Title, X + (BarHeight / 2) - (16 / 2), Y - BarHeight + (BarHeight / 2) - (16 / 2));

                //Hide
                //System.Graphics.DrawFilledRectangle(0x313131, X + Width - BarHeight, Y - BarHeight, BarHeight, BarHeight);
            }
            /*
            else
            {
                InputUpdate();
            }
            */


            const int Rad = 5;

            if (!LiterallyWindow)
            {
                System.Graphics.DrawFilledRectangle(0xFFF0F3F4, X, Y, Width, Height);

                System.Graphics.DrawFilledCircle(0xFFFF0000, X + Width - Rad * 2, Y - BarHeight + Rad * 2, Rad);
            }

            if (Actived || this.LiterallyWindow)
            {
                InputUpdate();
            }

            UIUpdate();

            if (!LiterallyWindow)
            {
                //System.Graphics.DrawRectangle(System.Color2, X, Y, Width, Height, 1);
                System.Graphics.DrawLine(Actived ? System.Color2 : System.Color3, X, Y, X, Y + Height);
                System.Graphics.DrawLine(Actived ? System.Color2 : System.Color3, X + Width - 1, Y, X + Width - 1, Y + Height);
            }
        }

        public virtual void UIUpdate()
        {
        }

        public virtual void InputUpdate()
        {
        }

        public void Exit()
        {
            System.Windows.Remove(this);
        }
    }
}
