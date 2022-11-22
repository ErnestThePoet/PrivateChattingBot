using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateChattingBot.objects
{
    internal class BotConfig
    {
        public bool pasteOnly { get; set; }
        public int shortIntervalMs { get; set; }
        public int longIntervalMs { get; set; }

        public int searchBarX { get; set; }
        public int searchBarY { get; set; }

        public int messageBoxX { get; set; }
        public int messageBoxY { get; set; }
    }
}
