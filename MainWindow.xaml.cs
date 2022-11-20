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

            DatabaseHelper.ReadDatabase();

            keyboardListener = new KeyboardListener();
            keyboardListener.OnKeyPressed += OnKeyPressed;
            keyboardListener.HookKeyboard();

            uiManager = new UiManager(this);

            chatPerformer = new ChatPerformer(uiManager);

            uiManager.SetStopState();
        }

        private List<GroupMember> GetGroupMembers(string rawCardNames)
        {
            List<GroupMember> groupMembers = new List<GroupMember>();

            var splited = rawCardNames.Split("\r\n".ToCharArray());

            foreach (var line in splited)
            {
                if (line != "")
                {
                    var member = DatabaseHelper.GetMemberByName(line);
                    if (member != null)
                    {
                        groupMembers.Add(member);
                    }
                }
            }

            return groupMembers;
        }

        private void OnKeyPressed(object sender, KeyPressedArgs e)
        {
            switch (e.KeyPressed)
            {
                case Key.Oem3:
                    chatPerformer.Stop();
                    break;

                case Key.CapsLock:
                    chatPerformer.SetTargetMembers(
                        GetGroupMembers(tbTargetCardNames.Text));
                    chatPerformer.DoChats(tbTextToSend.Text);
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            keyboardListener.UnHookKeyboard();
        }
    }
}
