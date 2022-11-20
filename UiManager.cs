using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PrivateChattingBot
{
    internal class UiManager
    {
        MainWindow context;
        public UiManager(MainWindow context)
        {
            this.context = context;
        }
        public void SetTargetCardNamesTitle(int count)
        {
            context.lblTargetCardNames.Content =
                $"Target Card Names ({count})";
        }

        public void SetFiniehedCardNamesTitle(int count)
        {
            context.lblFinishedCardNames.Content =
                $"Finished Card Names ({count})";
        }

        private string GetNames(List<GroupMember> groupMembers)
        {
            string cardNames = "";
            foreach (GroupMember groupMember in groupMembers)
            {
                cardNames += groupMember.name + "\r\n";
            }
            return cardNames;
        }

        public void SetTargetCardNames(List<GroupMember> groupMembers)
        {
            context.tbTargetCardNames.Text = GetNames(groupMembers);
        }

        public void SetFinishedCardNames(List<GroupMember> groupMembers)
        {
            context.tbFinishedCardNames.Text = GetNames(groupMembers);
        }

        public void SetRunningState(string name)
        {
            context.lblAppState.Foreground = new SolidColorBrush(Colors.Green);
            context.lblAppState.Content = $"RUNNING - {name}";
        }

        public void SetStopState()
        {
            context.lblAppState.Foreground = new SolidColorBrush(Colors.Blue);
            context.lblAppState.Content = "STOPPED";
        }
    }
}
