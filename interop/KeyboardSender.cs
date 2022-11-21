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

        public static void SendCtrlA()
        {
            keybd_event(0x11, 0, 0, IntPtr.Zero);

            keybd_event(0x41, 0, 0, IntPtr.Zero);
            keybd_event(0x41, 0, KEYEVENTF_KEYUP, IntPtr.Zero);

            keybd_event(0x11, 0, KEYEVENTF_KEYUP, IntPtr.Zero);
        }

        public static void SendCtrlV()
        {
            keybd_event(0x11, 0, 0, IntPtr.Zero);

            keybd_event(0x56, 0, 0, IntPtr.Zero);
            keybd_event(0x56, 0, KEYEVENTF_KEYUP, IntPtr.Zero);

            keybd_event(0x11, 0, KEYEVENTF_KEYUP, IntPtr.Zero);
        }

        public static void SendEnter()
        {
            keybd_event(0x0D, 0, 0, IntPtr.Zero);
            keybd_event(0x0D, 0, KEYEVENTF_KEYUP, IntPtr.Zero);
        }

        public static void SendCtrlEnter()
        {
            keybd_event(0x11, 0, 0, IntPtr.Zero);

            keybd_event(0x0D, 0, 0, IntPtr.Zero);
            keybd_event(0x0D, 0, KEYEVENTF_KEYUP, IntPtr.Zero);

            keybd_event(0x11, 0, KEYEVENTF_KEYUP, IntPtr.Zero);
        }

        public static void SendAltF4()
        {
            keybd_event(0x12, 0, 0, IntPtr.Zero);

            keybd_event(0x73, 0, 0, IntPtr.Zero);
            keybd_event(0x73, 0, KEYEVENTF_KEYUP, IntPtr.Zero);

            keybd_event(0x12, 0, KEYEVENTF_KEYUP, IntPtr.Zero);
        }
    }
}
