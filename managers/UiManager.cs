using PrivateChattingBot.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private void SetChatTargetListTitle(int count)
        {
            context.lblChatTargetList.Content =
                $"{UiResManager.FindString("ChatTargetListTitle")} ({count})";
        }

        private void SetFinishedChatTargetListTitle(int count)
        {
            context.lblFinishedTargetList.Content =
                $"{UiResManager.FindString("FinishedChatTargetListTitle")} ({count})";
        }

        private string GetLines(
            List<ChatTarget> chatTargets, Func<ChatTarget, string> extractor)
        {
            string names = "";
            foreach (var chatTarget in chatTargets)
            {
                names += extractor(chatTarget) + "\r\n";
            }
            return names;
        }

        private string GetNamesString(List<string> names)
        {
            string nameString = "";
            for (int i = 0; i < names.Count - 1; i++)
            {
                nameString += names[i] + ",";
            }

            if (names.Count > 0)
            {
                nameString += names.Last();
            }

            return nameString;
        }

        private void SetChatTargetList(List<ChatTarget> chatTargets)
        {
            context.tbChatTargetList.Text = GetLines(
                chatTargets,
                x => x.name);
        }

        private void SetFinishedChatTargetList(List<ChatTarget> finishedChatTargets)
        {
            context.tbFinishedTargetList.Text = GetLines(
                finishedChatTargets,
                x => $"{x.name} - {x.chatTime.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void UpdatePasteOnlyTitle()
        {
            if (ConfigManager.PasteOnly)
            {
                context.window.Title =
                    $"{UiResManager.FindString("AppWindowTitle")} - " +
                    $"[{UiResManager.FindString("PasteOnlyTitle")}]";
            }
            else
            {
                context.window.Title = UiResManager.FindString("AppWindowTitle");
            }
        }

        public void UpdateTwoListsAndTitles(
            List<ChatTarget> chatTargets,
            List<ChatTarget> finishedChatTargets)
        {
            SetChatTargetListTitle(chatTargets.Count);
            SetFinishedChatTargetListTitle(finishedChatTargets.Count);
            SetChatTargetList(chatTargets);
            SetFinishedChatTargetList(finishedChatTargets);
        }

        public void ShowUnchattedNamesDialog(List<string> names)
        {
            if (names.Count > 0)
            {
                MessageBox.Show(
                    $"私聊结束，未完成与以下目标的私聊：\n{GetNamesString(names)}",
                    "私聊结束",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        public void SetRunningState(string name)
        {
            context.lblAppState.Background = new SolidColorBrush(
                Color.FromArgb(72, 0xD3, 0xFF, 0xD2));
            context.lblAppState.Foreground = new SolidColorBrush(Colors.Green);
            context.lblAppState.Content =
                $"{UiResManager.FindString("RunningStateText")} - {name}";
        }

        public void SetStopState()
        {
            context.lblAppState.Background = new SolidColorBrush(
                Color.FromArgb(72, 0xD2, 0xE2, 0xFF));
            context.lblAppState.Foreground = new SolidColorBrush(Colors.Blue);
            context.lblAppState.Content = UiResManager.FindString("StopStateText");
        }
    }
}
