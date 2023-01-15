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
        private List<string> discardedNames;

        public ChatPerformer(UiManager uiManager)
            => (chatTargets, finishedChatTargets, this.uiManager)
                = (new List<ChatTarget>(), new List<ChatTarget>(), uiManager);

        private void RunOnUiThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                action();
            });
        }

        public bool GetIsStarted()
        {
            return isStarted;
        }

        public void SetChatTargets(List<ChatTarget> chatTargets, List<string> discardedNames)
        {
            this.chatTargets = chatTargets;
            uiManager.UpdateTwoListsAndTitles(
                chatTargets, finishedChatTargets);
            this.discardedNames = discardedNames;
        }

        public void DoChats(string rawMessages)
        {
            if (isStarted || chatTargets.Count == 0)
            {
                return;
            }

            var messages = rawMessages.Split(
                new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int currentMessageIndex = 0;

            if (messages.Length == 0)
            {
                return;
            }

            isStarted = true;

            Thread chatThread = new Thread(() =>
            {
                for (int i = 0; isStarted && i < chatTargets.Count;)
                {
                    var currentChatTarget = chatTargets[i];

                    RunOnUiThread(() =>
                    {
                        uiManager.SetRunningState(currentChatTarget.name);
                    });

                    // Click search bar
                    MouseSender.LeftClick(
                        ConfigManager.SearchBarX, ConfigManager.SearchBarY);
                    Clipboard.SetText(currentChatTarget.groupMember.qqId.ToString());
                    Thread.Sleep(ConfigManager.ShortIntervalMs);

                    // Press Ctrl+V to search for the student
                    KeyboardSender.SendCtrlV();
                    Thread.Sleep(ConfigManager.LongIntervalMs);

                    // Press enter to open chat window
                    KeyboardSender.SendEnter();
                    Clipboard.SetText(messages[currentMessageIndex]);
                    Thread.Sleep(ConfigManager.LongIntervalMs);

                    // Focus message box
                    MouseSender.LeftClick(
                        ConfigManager.MessageBoxX, ConfigManager.MessageBoxY);
                    Thread.Sleep(ConfigManager.ShortIntervalMs);

                    // Press Ctrl+A to select all previous message text
                    KeyboardSender.SendCtrlA();
                    Thread.Sleep(ConfigManager.ShortIntervalMs);

                    // Press Ctrl+V to paste message text
                    KeyboardSender.SendCtrlV();
                    Thread.Sleep(ConfigManager.ShortIntervalMs);

                    if (!ConfigManager.PasteOnly)
                    {
                        // Press Ctrl+Enter to send message
                        KeyboardSender.SendCtrlEnter();
                        Thread.Sleep(ConfigManager.ShortIntervalMs);
                    }

                    // Close chat window
                    KeyboardSender.SendAltF4();

                    currentChatTarget.chatTime = DateTime.Now;
                    finishedChatTargets.Add(currentChatTarget);
                    chatTargets.RemoveAt(i);

                    if (currentMessageIndex < messages.Length - 1)
                    {
                        currentMessageIndex++;
                    }
                    else
                    {
                        currentMessageIndex = 0;
                    }

                    RunOnUiThread(() =>
                        uiManager.UpdateTwoListsAndTitles(
                            chatTargets, finishedChatTargets)
                    );
                }

                if (chatTargets.Count == 0)
                {
                    RunOnUiThread(() => Stop(false));
                }
            });

            chatThread.SetApartmentState(ApartmentState.STA);
            chatThread.Start();
        }

        public void Stop(bool isPause)
        {
            isStarted = false;
            uiManager.SetStopState();

            if (!isPause)
            {
                List<string> unchattedNames = new List<string>(discardedNames);
                foreach (var target in chatTargets)
                {
                    unchattedNames.Add(target.name);
                }

                uiManager.ShowUnchattedNamesDialog(unchattedNames);
            }
        }
    }
}
