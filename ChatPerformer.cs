using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PrivateChattingBot
{
    internal class ChatPerformer
    {
        private bool isStarted = false;

        private UiManager uiManager;

        private List<GroupMember> targetMembers;
        private List<GroupMember> finishedMembers;

        public ChatPerformer(UiManager uiManager)
        {
            targetMembers = new List<GroupMember>();
            finishedMembers = new List<GroupMember>();
            this.uiManager = uiManager;
        }

        public void SetTargetMembers(List<GroupMember> targetMembers)
        {
            this.targetMembers = targetMembers;
            uiManager.SetTargetCardNamesTitle(targetMembers.Count);
        }

        private void RunOnUiThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                action();
            });
        }

        public void DoChats(string text)
        {
            if (isStarted)
            {
                return;
            }

            isStarted = true;

            Thread chatThread=new Thread(() =>
            {
                const int NORMAL_INTERVAL_MS = 500;
                const int SHORT_INTERVAL_MS = 200;

                for(int i=0;this.isStarted&&i<targetMembers.Count;)
                {
                    var currentMember=targetMembers[i];

                    RunOnUiThread(() =>
                    {
                        uiManager.SetRunningState(currentMember.name);
                    });

                    // Click search bar
                    MouseSender.LeftClick(1717, 418);
                    Clipboard.SetText(currentMember.qqId.ToString());

                    Thread.Sleep(SHORT_INTERVAL_MS);

                    // Press Ctrl+V to search for the student
                    KeyboardSender.SendCtrlV();

                    Thread.Sleep(NORMAL_INTERVAL_MS);

                    // Press enter to open chat window
                    KeyboardSender.SendEnter();
                    Clipboard.SetText(text);

                    Thread.Sleep(NORMAL_INTERVAL_MS);

                    // Focus message box
                    MouseSender.LeftClick(775, 821);

                    Thread.Sleep(SHORT_INTERVAL_MS);

                    // Press Ctrl+A to select all previous message text
                    KeyboardSender.SendCtrlA();

                    Thread.Sleep(SHORT_INTERVAL_MS);

                    // Press Ctrl+V to paste message text
                    KeyboardSender.SendCtrlV();

                    Thread.Sleep(SHORT_INTERVAL_MS);

                    // Close chat window
                    KeyboardSender.SendAltF4();

                    finishedMembers.Add(currentMember);
                    targetMembers.RemoveAt(i);

                    RunOnUiThread(() =>
                    {
                        uiManager.SetTargetCardNamesTitle(targetMembers.Count);
                        uiManager.SetFiniehedCardNamesTitle(finishedMembers.Count);
                        uiManager.SetTargetCardNames(targetMembers);
                        uiManager.SetFinishedCardNames(finishedMembers);
                    });
                }

                if (targetMembers.Count == 0)
                {
                    RunOnUiThread(Stop);
                }
            });

            chatThread.SetApartmentState(ApartmentState.STA);
            chatThread.Start();
        }

        public void Stop()
        {
            isStarted = false;
            uiManager.SetStopState();
        }
    }
}
