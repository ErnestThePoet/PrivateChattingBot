using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrivateChattingBot.managers
{
    internal class UiResManager
    {
        private static FrameworkElement context;
        public static void SetContext(FrameworkElement context)
        {
            UiResManager.context = context;
        }

        public static string FindString(string key)
        {
            return context.FindResource(key).ToString();
        }
    }
}
