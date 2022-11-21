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

        private List<ChatTarget> chatTargets;
        private List<ChatTarget> finishedChatTargets;

        public ChatPerformer(UiManager uiManager)
        {
            chatTargets = new List<ChatTarget>();
            finishedChatTargets = new List<ChatTarget>();
            this.uiManager = uiManager;
        }

        private void RunOnUiThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                action();
            });
        }

        public void SetChatTargets(List<ChatTarget> chatTargets)
        {
            this.chatTargets = chatTargets;
            uiManager.UpdateTwoListsAndTitles(
                chatTargets, finishedChatTargets);
        }

        public void DoChats(string text)
        {
            if (isStarted|| chatTargets.Count==0)
            {
                return;
            }

            isStarted = true;

            Thread chatThread=new Thread(() =>
            {
                const int NORMAL_INTERVAL_MS = 500;
                const int SHORT_INTERVAL_MS = 100;

                for(int i=0;this.isStarted&&i< chatTargets.Count;)
                {
                    var currentChatTarget= chatTargets[i];

                    RunOnUiThread(() =>
                    {
                        uiManager.SetRunningState(currentChatTarget.name);
                    });

                    // Click search bar
                    MouseSender.LeftClick(1717, 418);
                    Clipboard.SetText(currentChatTarget.groupMember.qqId.ToString());
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

                    // Press Ctrl+Enter to send message
                    //KeyboardSender.SendCtrlEnter();
                    //Thread.Sleep(SHORT_INTERVAL_MS);

                    // Close chat window
                    KeyboardSender.SendAltF4();

                    currentChatTarget.chatTime = DateTime.Now;
                    finishedChatTargets.Add(currentChatTarget);
                    chatTargets.RemoveAt(i);

                    RunOnUiThread(() =>
                    {
                        uiManager.UpdateTwoListsAndTitles(
                            chatTargets, finishedChatTargets);
                    });
                }

                if (chatTargets.Count == 0)
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
