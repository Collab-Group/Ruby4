// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.External.x86;
using Mosa.External.x86.Driver;
using Mosa.Kernel.x86;
using Mosa.Runtime.Plug;
using System.Runtime.InteropServices;

namespace MOSA1
{
    public static unsafe class Program
    {
        //Screen Resolution
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 800;

        public static void Main() { }

        //Screen Resolution
        [VBERequire(1280, 800, 32)]
        [Resource(
            "Calc.bmp",
            "Paint.bmp",
            "Shutdown.bmp",
            "Snake.bmp",
            "Start.bmp",
            "Terminal.bmp",
            "Wallpaper.bmp",
            "Cursor.bmp"
            )]
        [Plug("Mosa.Runtime.StartUp::KMain")]
        [UnmanagedCallersOnly(EntryPoint = "KMain", CallingConvention = CallingConvention.StdCall)]
        public static void KMain()
        {
            IDT.OnInterrupt += IDT_OnInterrupt;

            System system = new System();

            for (; ; )
            {
                system.Run();
            }
        }

        private static void IDT_OnInterrupt(uint irq, uint error)
        {
            switch (irq)
            {
                case 0x2C:
                    PS2Mouse.OnInterrupt();
                    break;
                case 0x21:
                    PS2Keyboard.OnInterrupt();
                    break;
            }
        }
    }
}
