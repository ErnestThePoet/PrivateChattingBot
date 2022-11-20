using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateChattingBot
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class GroupMember
    {
        public string name { get; set; }

        [JsonProperty]
        public string cardName { get; set; }

        [JsonProperty]
        public long qqId { get; set; }
    }
}
