using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WPFandAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        private int i = 0;

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(0.1);
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            progress.Text = $"{i++}";
        }

        private async void OnButtonCountClick(object sender, RoutedEventArgs e)
        {
            buttonCount.IsEnabled = false;
            timer.Start();
            if(buttonUseAwaitAsync.IsChecked == true)
            {
                await LongtaskAsync();
            }
            else
            {
                LongTask();
            }
            buttonCount.IsEnabled = true;
        }

        private void LongTask()
        {
            Thread.Sleep(5000);
            timer.Stop();
        }

        private async Task LongtaskAsync()
        {
            await Task.Delay(5000);
            timer.Stop();
        }
    }
}
