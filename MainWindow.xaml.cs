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
        KeyboardListener keyboardListener;
        ChatPerformer chatPerformer;
        
        public MainWindow()
        {
            InitializeComponent();

            DatabaseHelper.ReadDatabase();

            keyboardListener = new KeyboardListener();
            keyboardListener.OnKeyPressed += OnKeyPressed;
            keyboardListener.HookKeyboard();

            chatPerformer = new ChatPerformer();
        }

        private void OnKeyPressed(object sender, KeyPressedArgs e)
        {
            switch (e.KeyPressed)
            {
                case Key.Escape:
                    chatPerformer.Stop();
                    break;

                case Key.D1:
                    
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            keyboardListener.UnHookKeyboard();
        }
    }
}
