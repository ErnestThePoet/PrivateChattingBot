using System;

namespace PrivateChattingBot
{
    internal class ChatTarget
    {
        public GroupMember groupMember { get; set; }
        public string name { get; set; }
        public DateTime chatTime { get; set; }
    }
}
