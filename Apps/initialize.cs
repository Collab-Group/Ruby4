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

namespace MOSA1.Apps
{
    class Initialize : Window
    {

        public unsafe Initialize()
        {
            Title = "";
            this.ToolBarColor = 0xFFFEEE9E;

            //System.Graphics.DrawBitFontString(System.DefaultFontName, 0xFFF0F3F4, "Hello world!", 50, 50);
        }

        public override void UIUpdate()
        {
            //
            System.Graphics.DrawBitFontString(System.InitializeFont, 0x0, "Ruby4", X + 50, Y + 12);
            System.Graphics.DrawBitFontString(System.InitSub, 0x0, "Works best on Intel processors.", X + 50, Y + 20);
        }
    }
}
