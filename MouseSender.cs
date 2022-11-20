using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace PrivateChattingBot
{
    internal class MouseSender
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftClick(int xPos, int yPos)
        {
            SetCursorPos(xPos, yPos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xPos, yPos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xPos, yPos, 0, 0);
        }
    }
}
