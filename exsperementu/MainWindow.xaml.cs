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
using System.Windows.Threading;

namespace exsperementu
{
    public partial class MainWindow : Window
    {
        string bigString = " QWERTYUIOPASDFGHJKLZXCVBNM~!@#$%^&*()_+{}|:\"<>?1234567890[],./\\`-=;'";
        string smallString = " qwertyuiopasdfghjklzxcvbnm~!@#$%^&*()_+{}|:\"<>?1234567890[],./\\`-=;'";
        DispatcherTimer timer = null;
        int time = 0;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            SliderDifficulty.IsEnabled = false;
            Caps.IsEnabled = false;
            Stop.IsEnabled = true;
            time = 0;
            timer.Start();
            lineUser.IsReadOnly = false;
            lineUser.IsEnabled = true;

            linePrograms.Text = "";
            Random rand = new Random();
            int n = Convert.ToInt32(Difficulty.Content);
            char[] a = new char[70];
            if (Caps.IsChecked == false)
            {
                for (int i = 0; i < 70; i++)
                {
                    a[i] += smallString[rand.Next(n)];
                    linePrograms.Text += a[i];
                }
            }
            else
            {
                for (int i = 0; i < 70; i++)
                {
                    a[i] += bigString[rand.Next(n)];
                    linePrograms.Text += a[i];
                }
            }
        }

        private void SliderDifficulty_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int num = 0;
            Slider s = sender as Slider;
            num = (int)s.Value;
            Difficulty.Content = num.ToString();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            SliderDifficulty.IsEnabled = true;
            Caps.IsEnabled = true;
            Stop.IsEnabled = false;
            lineUser.Text = "";
            linePrograms.Text = "";
            Fails.Content = 0;
            SpeedChar.Content = 0;
            lineUser.IsReadOnly = true;
            lineUser.IsEnabled = false;
            timer.Stop();
            time = 0;
            Fails.Content = 0;
        }

        private void lineUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            int f = 0;
            for(int i = 0;i < lineUser.Text.Length;i++)
            {
                if (lineUser.Text[i] == linePrograms.Text[i])
                {
                }
                else
                {
                   f++;
                   Fails.Content = f;
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            time++;
            Speed();
        }
        void Speed()
        {
            SpeedChar.Content = Math.Round(((double)lineUser.Text.Length / time) * 60).ToString();
        }
    }
}
