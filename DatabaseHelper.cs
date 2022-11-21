using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrivateChattingBot
{
    internal class DatabaseHelper
    {
        private static List<GroupMember> members;
        public static void ReadDatabase()
        {
            string membersRawJson="";
            try
            {
                membersRawJson = File.ReadAllText(
                    AppDomain.CurrentDomain.BaseDirectory + "data/members.json");
            }
            catch
            {
                MessageBox.Show("Failed to load members.json");
                Application.Current.Shutdown();
            }

            try
            {
                members= JsonConvert.DeserializeObject
                    <List<GroupMember>>(membersRawJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show("members.json deserialization error: " + ex.Message);
                Application.Current.Shutdown();
            }
        }

        public static ChatTarget GetChatTargetByName(string name)
        {
            foreach(var i in members)
            {
                if (i.cardName.Contains(name))
                {
                    return new ChatTarget { groupMember = i,name=name };
                }
            }

            return null;
        }
    }
}
