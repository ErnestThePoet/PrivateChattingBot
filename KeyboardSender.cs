using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrivateChattingBot
{
    internal class KeyboardSender
    {
        [DllImport("user32.dll")]

        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

        private const int KEYEVENTF_KEYUP = 0x0002;

        public static void SendCtrlV()
        {
            keybd_event(0x11, 0, 0, IntPtr.Zero);

            keybd_event(0x56, 0, 0, IntPtr.Zero);
            keybd_event(0x56, 0, KEYEVENTF_KEYUP, IntPtr.Zero);

            keybd_event(0x11, 0, KEYEVENTF_KEYUP, IntPtr.Zero);
        }
    }
}
