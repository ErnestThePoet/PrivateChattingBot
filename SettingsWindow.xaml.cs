using PrivateChattingBot.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrivateChattingBot
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private Action onClose;
        public SettingsWindow(Action onClose)
        {
            InitializeComponent();

            this.onClose = onClose;

            cbPasteOnly.IsChecked = ConfigManager.PasteOnly;

            tbShortIntervalMs.Text = ConfigManager.ShortIntervalMs.ToString();
            tbLongIntervalMs.Text = ConfigManager.LongIntervalMs.ToString();

            tbSearchBarX.Text = ConfigManager.SearchBarX.ToString();
            tbSearchBarY.Text = ConfigManager.SearchBarY.ToString();

            tbMessageBoxX.Text = ConfigManager.MessageBoxX.ToString();
            tbMessageBoxY.Text = ConfigManager.MessageBoxY.ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int shortIntervalMs = 0;
            int longIntervalMs = 0;

            int searchBarX = 0;
            int searchBarY = 0;

            int messageBoxX = 0;
            int messageBoxY = 0;

            if (!int.TryParse(tbShortIntervalMs.Text, out shortIntervalMs)
                ||shortIntervalMs<=0)
            {
                MessageBox.Show(
                    UiResManager.FindString("ShortIntervalMsInvalidMessage"),
                    UiResManager.FindString("ErrorTitle"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(tbLongIntervalMs.Text, out longIntervalMs)
                || longIntervalMs <= 0)
            {
                MessageBox.Show(
                    UiResManager.FindString("LongIntervalMsInvalidMessage"),
                    UiResManager.FindString("ErrorTitle"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(tbSearchBarX.Text, out searchBarX)
                || !int.TryParse(tbSearchBarY.Text, out searchBarY)
                || searchBarX <= 0
                || searchBarY<=0)
            {
                MessageBox.Show(
                    UiResManager.FindString("SearchBarClickCoordInvalidMessage"),
                    UiResManager.FindString("ErrorTitle"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(tbMessageBoxX.Text, out messageBoxX)
                || !int.TryParse(tbMessageBoxY.Text, out messageBoxY)
                || messageBoxX <= 0
                || messageBoxY <= 0)
            {
                MessageBox.Show(
                    UiResManager.FindString("MessageBoxClickCoordInvalidMessage"),
                    UiResManager.FindString("ErrorTitle"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            ConfigManager.UpdateAndSave(
                cbPasteOnly.IsChecked??false,
                shortIntervalMs,
                longIntervalMs,
                searchBarX,
                searchBarY,
                messageBoxX,
                messageBoxY);
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            onClose();
        }
    }
}
