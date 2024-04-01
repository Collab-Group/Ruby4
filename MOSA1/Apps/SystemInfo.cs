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

namespace MOSA1.Apps
{
    class SystemInfo : Window
    {
        public string Content = "";
        public string aContent = "";

        public unsafe SystemInfo()
        {
            Title = "Machine Infomation and Data";

            this.ToolBarColor = 0xFFFEEE9E;

            //System.Graphics.DrawBitFontString(System.DefaultFontName, 0xFFF0F3F4, "Hello world!", 50, 50);
        }



        public override void UIUpdate()
        {
            Object obj = Intrinsic.GetObjectFromAddress(GC.TotalAllocPtr);
            TypeDefinition td = new TypeDefinition(new Pointer(obj.GetType().TypeHandle.Value));
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "Total Memory: " + Memory.GetAvailableMemory() / (1024 * 1024) + "MB", X, Y);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "CPU Vendor: ???", X, Y + 14);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "MAC Address: " + MACAddress.GetRandomAddress(), X, Y + 35);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "Total Alloc: " + GC.TotalAlloc, X, Y + 52);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "Total Reuse: " + GC.TotalReuse, X, Y + 69);
            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "GC Total Full Used: " + GC.TotalFullUsed, X, Y + 86);

            System.Graphics.DrawBitFontString(System.DefaultFontName, 0x0, "FPS:" + FPSMeter.FPS, X, Y + 103);
        }
    }
    }
