using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace ShutdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This will invoke the shutdown operation with the time spedified in minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shutdownButton_Click(object sender, RoutedEventArgs e)
        {
            Process p1 = new Process();
            p1.StartInfo.Arguments = @"/s /f /t " + Convert.ToInt32(minutesTextBox.Text) * 60;
            p1.StartInfo.FileName = "shutdown";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p1.Start();
            MessageBox.Show(this, "System will shutdown in " + minutesTextBox.Text + " minutes");
            shutdownButton.IsEnabled = false;
            cancelShutdown.IsEnabled = true;
        }

        /// <summary>
        /// This will abort the shutdown operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelShutdown_Click(object sender, RoutedEventArgs e)
        {
            Process p1 = new Process();
            p1.StartInfo.Arguments = @"/a";
            p1.StartInfo.FileName = "shutdown";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p1.Start();
            MessageBox.Show(this, "System shutdown cancelled");
            cancelShutdown.IsEnabled = false;
            shutdownButton.IsEnabled = true;
        }

        /// <summary>
        /// We will provide this timer value to Shutdown operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minutesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(minutesTextBox.Text)) // Shutdown button will remain disabled till we get valid number. 0 is also valid to shutdown immediately
            {
                shutdownButton.IsEnabled = false;
                cancelShutdown.IsEnabled = false;
                return;
            }
            else
            {
                shutdownButton.IsEnabled = true;
                cancelShutdown.IsEnabled = false;
            }

            int time = 0;
            if (!int.TryParse( minutesTextBox.Text,out time)) // Check if it valid number or contains any non-digit data
            {
                MessageBox.Show(this, "Please enter only digits");
                minutesTextBox.Text = minutesTextBox.Text.Substring(0, minutesTextBox.Text.Length - 1); // Truncate the last character
                minutesTextBox.CaretIndex = minutesTextBox.Text.Length;
            }
        }
    }
}
