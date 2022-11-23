using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PrivateChattingBot.managers;

namespace PrivateChattingBot
{
    internal class DatabaseManager
    {
        private static List<GroupMember> members;

        private const string JSON_FILE_PATH = "data/members.json";

        public static void Load()
        {
            string rawJson="";
            try
            {
                rawJson = File.ReadAllText(
                    AppDomain.CurrentDomain.BaseDirectory + JSON_FILE_PATH);
            }
            catch
            {
                MessageBox.Show(
                    $"{UiResManager.FindString("FailedToLoadText")} " +
                    $"{JSON_FILE_PATH}",
                    UiResManager.FindString("ErrorTitle"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            try
            {
                members= JsonConvert.DeserializeObject
                    <List<GroupMember>>(rawJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{JSON_FILE_PATH} " +
                    $"{UiResManager.FindString("DeserializationErrorText")}: " +
                    $"{ex.Message}",
                    UiResManager.FindString("ErrorTitle"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
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
