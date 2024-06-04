using Mosa.External.x86.Drawing.Fonts;
using Mosa.External.x86.Driver;
using Mosa.External.x86.Networking;
using Mosa.Runtime;
using System.Collections.Generic;

namespace MOSA1.Apps
{
    class NetInit : Window
    {
        public NetInit()
        {
            Title = "Network Wizard";
            Btns = new List<Btn>();
            this.Icon = Icons.Calc;
        }

        public override void UIUpdate()
        {
            if (virtualGraphics == null)
            {
                virtualGraphics = new Mosa.External.x86.Drawing.VirtualGraphics(this.Width, this.Height);
                virtualGraphics.DrawFilledRectangle(0xFFF0F0F0, 0, 0, Width, Height);

                System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "Network Initialise", X, 435);
                System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "This app allows you to start network connection", X, 435);

                //7
                AddButton(170, 170, "Start network");
                AddButton(20, 170, "Stop network");

                System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "Network Initialise", X, 170);
                System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "This app allows you to start network connection", X, 200);
            }

           // System.Graphics.DrawImageASM(virtualGraphics.bitmap, X, Y);

            if (PressedButton.Name != null)
            {
                System.Graphics.DrawFilledRectangle(0xFFB2BABB, X + PressedButton.X, Y + PressedButton.Y, 90, 20);
                int i = BitFont.Calculate(System.DefaultFontName, PressedButton.Name);
                System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, PressedButton.Name, X + PressedButton.X + (60 / 2) - (i / 2), Y + PressedButton.Y + 2);
            }
        }

        bool Pressed = false;

        Btn PressedButton;

        public override void InputUpdate()
        {
            if (PS2Mouse.MouseStatus == Mosa.External.x86.Driver.Input.MouseStatus.Left)
            {
                if (!Pressed)
                {
                    for (int i = 0; i < Btns.Count; i++)
                    {
                        if (PS2Mouse.X > this.X + Btns[i].X && PS2Mouse.X < this.X + Btns[i].X + Btns[i].Width && PS2Mouse.Y > this.Y + Btns[i].Y && PS2Mouse.Y < this.Y + Btns[i].Y + Btns[i].Height)
                        {
                            System.MessageBox.Show(this, "Networked");

                            System.MessageBox.Title = "Network_Status";
                            System.MessageBox.Visible = true;
                        }
                    }
                    if(PS2Mouse.MouseStatus == Mosa.External.x86.Driver.Input.MouseStatus.Left)
                    {
                        for (int i = 0; i < Btns.Count; i++)
                    {
                        System.Graphics.DrawFilledRectangle(Btns[i].Color, X + Btns[i].X, Y + Btns[i].Y, Btns[i].Width, Btns[i].Height);
                        if (Btns[i].Name == "Start Network")
                        {
                                Ethernet.Initialize(
                                new byte[] { 192, 168, 56, 188 },
                                new byte[] { 192, 168, 56, 1 },
                                new byte[] { 255, 255, 255, 0 });
                                EthernetController.Initialize();
                                ARP.Initialize();
                                System.Terminal.WriteLine("Successfully Initialized Ethernet Controller");
                                System.Terminal.WriteLine($"Ethernet 0: {EthernetController.Controller}");
                                System.Terminal.WriteLine($"IP Address: {Ethernet.IPAddress[0]}.{Ethernet.IPAddress[1]}.{Ethernet.IPAddress[2]}.{Ethernet.IPAddress[3]}");
                            }
                    }

                    }

                    
                }
            }
            else
            {
                PressedButton.Name = null;
                Pressed = false;
            }
        }

        List<Btn> Btns;

        public struct Btn
        {
            public int X, Y, Width, Height;
            public uint Color;
            public string Name;
        }

        private void AddButton(int X, int Y, string s)
        {
            virtualGraphics.DrawFilledRectangle(0xFFD6DBDF, X, Y, 90, 20);
            int i = BitFont.Calculate(System.DefaultFontName, s);
            virtualGraphics.DrawBitFontString(System.DefaultFontName, 0x0, s, X + (60 / 2) - (i / 2), Y + 2);

            System.Terminal.New();
            System.Terminal.WriteLine($"X:{X},Y:{Y}");
            Btns.Add(new Btn()
            {
                X = X,
                Y = Y,
                Width = 60,
                Height = 20,
                Name = s
            });
        }
    }
}