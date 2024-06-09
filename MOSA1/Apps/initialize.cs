using Mosa.External.x86;
using Mosa.External.x86.Drawing.Fonts;
using Mosa.External.x86.Driver;
using Mosa.Kernel.x86;
using Mosa.Runtime;
using Mosa.Runtime.x86;
using System.Drawing;
using Mosa.External.x86.Networking;
using Mosa.Runtime.Metadata;
using System.Reflection;
using Mosa.Kernel.x86.Smbios;
using System;
using System.Collections.Generic;
using Mosa.External.x86.Driver.Audio;

namespace MOSA1.Apps
{
    class Initialize : Window
    {
        ulong W = 0;
        public unsafe Initialize()
        {
            Title = "";
            this.ToolBarColor = Color.DarkGray.ToArgb();
            PCSpeaker.Beep(400, 500);

            PIT.Wait(5000);

            System.Windows.Remove(this);
            this.Dispose();


        }

        public override void UIUpdate()
        {

            if (virtualGraphics == null)
            {
                virtualGraphics = new Mosa.External.x86.Drawing.VirtualGraphics(this.Width, this.Height);
                virtualGraphics.DrawFilledRectangle(System.Color1, 0, 0, Width, Height);
            }

            System.Graphics.DrawImageASM(virtualGraphics.bitmap, X, Y);

            System.Graphics.DrawBitFontString(System.InitializeFont, Color.White.ToArgb(), "Ruby4", X + 50, Y + 17);
            System.Graphics.DrawBitFontString(System.InitSub, Color.White.ToArgb(), "Works best on Intel processors.", X + 53, Y + 40);

            //700x350
            System.Graphics.DrawBitFontString(System.InitSub, Color.White.ToArgb(), "Initializing...", X + 68, Y + 320);


            SoundBlaster16.Initialize();
            ASC16.Setup();

            
        }
        

        

        /*
        public void Run()
        {


        }
        */
    }
}
