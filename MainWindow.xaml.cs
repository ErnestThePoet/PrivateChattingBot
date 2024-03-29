﻿using PrivateChattingBot.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrivateChattingBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KeyboardListener keyboardListener;
        private ChatPerformer chatPerformer;
        private UiManager uiManager;


        public MainWindow()
        {
            InitializeComponent();

            UiResManager.SetContext(this);
            ConfigManager.Load();
            DatabaseManager.Load();

            keyboardListener = new KeyboardListener();
            uiManager = new UiManager(this);
            chatPerformer = new ChatPerformer(uiManager);

            uiManager.UpdatePasteOnlyTitle();

            keyboardListener.OnKeyPressed += OnKeyPressed;
            keyboardListener.HookKeyboard();

            uiManager.SetStopState();
        }

        private (List<ChatTarget> chatTargets, List<string> discardedNames)
            GetChatTargets(string rawCardNames)
        {
            List<ChatTarget> chatTargets = new List<ChatTarget>();
            List<string> discardedNames = new List<string>();

            var splited = rawCardNames.Split(
                new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in splited)
            {
                string purifiedLine = line.Replace(" ", "").Replace("\t", "");

                if (purifiedLine != "")
                {
                    var chatTarget = DatabaseManager.GetChatTargetByName(purifiedLine);
                    if (chatTarget != null)
                    {
                        chatTargets.Add(chatTarget);
                    }
                    else
                    {
                        discardedNames.Add(purifiedLine);
                    }
                }
            }

            return (chatTargets, discardedNames);
        }

        private void OnKeyPressed(object sender, KeyPressedArgs e)
        {
            switch (e.KeyPressed)
            {
                case Key.CapsLock:
                    if (chatPerformer.GetIsStarted())
                    {
                        chatPerformer.Stop(true);
                    }
                    else
                    {
                        (var chatTargets, var discardedNames) = 
                            GetChatTargets(tbChatTargetList.Text);
                        chatPerformer.SetChatTargets(chatTargets, discardedNames);
                        chatPerformer.DoChats(tbMessageToSend.Text);
                    }

                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            keyboardListener.UnHookKeyboard();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!chatPerformer.GetIsStarted())
            {
                new SettingsWindow(() =>
                {
                    uiManager.UpdatePasteOnlyTitle();
                }).ShowDialog();
            }
        }
    }
}
