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
    class Welcome : Window
    {

        public unsafe Welcome()
        {
            Title = "Welcome to Ruby4";
            this.ToolBarColor = 0xFFFEEE9E;

            //System.Graphics.DrawBitFontString(System.DefaultFontName, 0xFFF0F3F4, "Hello world!", 50, 50);
        }

        public override void UIUpdate()
        {

            System.Graphics.DrawBitFontString(System.BigSansSerif32, 0x0, "Ruby 4, by WaffleBell and Jack Gannon", X + 50, Y + 12);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "Ruby4 is a hobbyist operating system, using certain elements of MOSA with my ideas and code", X + 32, Y + 100);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "built on top of it. Ruby4 encourages exploration and experimentation with its user friendly.", X + 32, Y + 115);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "source code, with it's kernel and applications being written purely in C#.", X + 32, Y + 130);

            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "Things aren't quite perfect, and they probably never will be in this OS, but starring the repo", X + 32, Y + 160);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "always helps :)  -  Spare change please!", X + 32, Y + 175);
        }
    }
}
