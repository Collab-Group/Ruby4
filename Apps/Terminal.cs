using Mosa.External.x86;
using Mosa.External.x86.Drawing.Fonts;
using Mosa.External.x86.Driver;
using Mosa.External.x86.Networking;
using Mosa.Kernel.x86;
using Mosa.Runtime;
using Mosa.Runtime.Metadata;
using Mosa.Runtime.x86;
using System.Drawing;
using System.Reflection;

namespace MOSA1.Apps
{
    public class Terminal : Window
    {
        //public string Content = "MOSA is an open source software project that natively executes .NET applications within a virtual hypervisor or on bare metal hardware!";
        public string Content = "";
        public string aContent = "";

        public string Command = "";

        public unsafe Terminal()
        {
            Title = "Terminal";

            this.Icon = Icons.Terminal;

            this.ToolBarColor = System.Color1;
        }

        KeyCode KeyCode;

        public override void InputUpdate()
        {
            KeyCode = Console.ReadKey(false);

            if (KeyCode == KeyCode.Delete)
            {
                if (ContinuableCommand == "")
                {
                    Command = "";

                    CursorX = 0;
                    virtualGraphics.DrawFilledRectangle(System.Color1, CursorX, CursorY, Width, 16);
                    New();
                }
            }
            else if (KeyCode == KeyCode.Enter)
            {
                if (ContinuableCommand == "")
                {
                    WriteLine();
                    ProcessCommand();
                    Command = "";

                    New();
                }
            }
            else if (KeyCode == KeyCode.ESC)
            {
                ContinuableCommand = "";
                WriteLine();
                New();
            }
            else
            {
                if (ContinuableCommand == "")
                {
                    if (PS2Keyboard.IsCapsLock)
                    {
                        Write(KeyCode.KeyCodeToString());
                        Command += KeyCode.KeyCodeToString();
                    }
                    else
                    {
                        Write(KeyCode.KeyCodeToString().ToLower());
                        Command += KeyCode.KeyCodeToString().ToLower();
                    }
                }
            }
        }


        string ContinuableCommand = "";

        public int CursorX = 0;
        public int CursorY = 0;

        public void Write(string s)
        {
            if (CursorY + 16 > Height)
            {
                CursorY -= 16;
                ASM.MEMCPY(virtualGraphics.VideoMemoryCacheAddr, (uint)(virtualGraphics.VideoMemoryCacheAddr + (16 * Width * 4)), (uint)(Width * (Height - 16) * 4));
                virtualGraphics.DrawFilledRectangle(System.Color1, 0, Height - 16, Width, 16);
            }

            if (virtualGraphics == null)
            {
                return;
            }

            CursorX += virtualGraphics.DrawBitFontString(System.DefaultFontName, 0xFFF0F3F4, s, CursorX, CursorY);
            if (CursorX > Width)
            {
                CursorX = 0;
                CursorY += 16;
            }
        }

        public void WriteLine(string s)
        {
            Write(s);
            //s.Dispose();
            CursorX = 0;
            CursorY += 16;
        }

        public void WriteLine()
        {
            CursorX = 0;
            CursorY += 16;
        }

        private unsafe void ProcessCommand()
        {
            string CMD = Command.ToUpper();
            if (CMD == "WELCOME")
            {
                System.Add(new Welcome() { X = 100, Y = 350, Width = 700, Height = 350 });
                WriteLine("About passed onto Window: 'Welcome to Ruby4!'");
            }
            else if (CMD == "NOTEPAD")
            {
                WriteLine("Launched \"Notepad\"");
                System.Add(new Notepad() { X = 100, Y = 520, Width = 300, Height = 200 });
            }
            else if (CMD == "HELP")
            {
                WriteLine("About - About Ruby4");
                WriteLine("Clear - Clears terminal history");
                WriteLine("FPS - Shows refresh rate of display");
                WriteLine("Shutdown - ACPI powers off computer");
                WriteLine("Reboot - ACPI reboots the computer");
                WriteLine("Network - Network Wizard allows you to start netowrk or stop it");
                WriteLine("VBE - Graphics status");
                WriteLine("GC - Garbage Collection data(passes onto Machine Infomation and Data)");
                WriteLine("FPS - Shows refresh rate of display");
            }
            else if (CMD == "CLEAR")
            {
                CursorX = 0;
                CursorY = 0;
                virtualGraphics.DrawFilledRectangle(System.Color1, 0, 0, Width, Height);
            }
            else if (CMD == "FPS")
            {
                ContinuableCommand = "FPS";
            }
            
            else if (CMD == "SHUTDOWN")
            {
                Power.Shutdown();
            }
            else if (CMD == "REBOOT")
            {
                Power.Reboot();
            }
            else if (CMD == "NETWORK")
            {
                System.Add(new NetInit() { X = 100, Y = 520, Width = 300, Height = 200 });
            }
            else if (CMD == "VBE")
            {
                WriteLine($"Physics Base: {VBE.VBEModeInfo->PhysBase.ToString("x2")}");
                WriteLine($"Win Function Pointer: {VBE.VBEModeInfo->WinFuncPtr.ToString("x2")}");
            }
            else if (CMD == "GC")
            {
                System.Add(new SystemInfo() { X = 100, Y = 520, Width = 300, Height = 200 });

                for (int i = 0; i < 12; i++) 
                {
                    Write($"0x{GC.TotalAllocPtr.Load8(i).ToString("x2")}, ");
                }
                WriteLine();
                WriteLine($"GC Desc 0 Size:{(&GC.MemoryDescriptors[0])->Size}");
            }
            else if (CMD == "TEST")
            {
                WriteLine($"this.ReferenceCount: {this.ReferenceCount}");
                WriteLine($"System.Graphics.ReferenceCount: {System.Graphics.ReferenceCount}");
            }else if(CMD == "HELLO") 
            {
                WriteLine("Smoke weed everyday");

                
            }
            else if (CMD == "INFO")
            {
                WriteLine("System Infomation passed onto Window: 'Machine Infomation and Data'");
                System.Add(new SystemInfo() { X = 100, Y = 350, Width = 700, Height = 350 });
            }
            else
            {
                WriteLine("Unknown command");
            }
        }

        public void New()
        {
            Write(">");
        }

        public override void UIUpdate()
        {
            if (virtualGraphics == null)
            {
                virtualGraphics = new Mosa.External.x86.Drawing.VirtualGraphics(this.Width, this.Height);
                virtualGraphics.DrawFilledRectangle(System.Color1, 0, 0, Width, Height);

                CursorX = 0;
                CursorY = 0;
                WriteLine("Ruby4 Terminal");
                WriteLine("Type \"help\" for a list of useable commands. Type \"welcome\" to see the Welcome window");
                WriteLine("Version 2.2");
                WriteLine("https://wafflebell.photography/projects");
                New();
            }

            switch (ContinuableCommand)
            {
                case "FPS":
                    CursorX = 0;
                    virtualGraphics.DrawFilledRectangle(System.Color1, CursorX, CursorY, Width, 16);
                    Write("FPS:" + FPSMeter.FPS + " Press ESC To Continue");
                    break;
            }

            System.Graphics.DrawImageASM(virtualGraphics.bitmap, X, Y);

            System.Graphics.DrawBitFontString(System.terminalFont, 0xFFF0F3F4, "_", X + CursorX, Y + CursorY);
            //System.Graphics.DrawBitFontString("ArialCustomCharset24", Color.White.ToArgb(), "Message: ", 10, 30);
        }
    }
}
