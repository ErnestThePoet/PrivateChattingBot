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

            ConfigManager.Load();
            DatabaseManager.Load();

            if (ConfigManager.PasteOnly)
            {
                window.Title += " - [Paste Only]";
            }

            keyboardListener = new KeyboardListener();
            keyboardListener.OnKeyPressed += OnKeyPressed;
            keyboardListener.HookKeyboard();

            uiManager = new UiManager(this);

            chatPerformer = new ChatPerformer(uiManager);

            uiManager.SetStopState();
        }

        private List<ChatTarget> GetChatTargets(string rawCardNames)
        {
            List<ChatTarget> chatTargets = new List<ChatTarget>();

            var splited = rawCardNames.Split("\r\n".ToCharArray());

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
                }
            }

            return chatTargets;
        }

        private void OnKeyPressed(object sender, KeyPressedArgs e)
        {
            switch (e.KeyPressed)
            {
                case Key.CapsLock:
                    if (chatPerformer.GetIsStarted())
                    {
                        chatPerformer.Stop();
                    }
                    else
                    {
                        chatPerformer.SetChatTargets(
                        GetChatTargets(tbChatTargetList.Text));
                        chatPerformer.DoChats(tbMessageToSend.Text);
                    }
                    
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            keyboardListener.UnHookKeyboard();
        }
    }
}
