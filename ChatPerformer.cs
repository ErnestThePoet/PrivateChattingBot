using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateChattingBot
{
    internal class ChatPerformer
    {
        private bool isStarted = false;

        public void DoChats(string text,List<long> qqIds)
        {
            
        }

        public void Stop()
        {
            isStarted = false;
        }
    }
}
