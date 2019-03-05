using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FlipACoin
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> stringList = new ObservableCollection<string>();
        private DispatcherTimer btnTimer = new DispatcherTimer();
        private DispatcherTimer RandomTimer = new DispatcherTimer();
        private Random random = new Random();
        private int countDown = 5;
        public MainWindow()
        {
            InitializeComponent();
            listBox.ItemsSource = stringList;
            btnTimer.Interval = TimeSpan.FromSeconds(1);
            btnTimer.Tick += BtnTimer_Tick;

            RandomTimer.Interval = TimeSpan.FromMilliseconds(20);
            RandomTimer.Tick += RandomTimer_Tick;
        }

        private void RandomTimer_Tick(object sender, EventArgs e)
        {
            var index = random.Next(0, stringList.Count);
            result.Text = stringList[index];
        }

        private void BtnTimer_Tick(object sender, EventArgs e)
        {
            switch (countDown)
            {
                case 5:
                    btn.Content = "Emmmmm....";
                    break;
                case 4:
                    btn.Content = "最终结果会是什么呢...？";
                    break;
                case 3:
                    btn.Content = "很快了很快了...";
                    break;
                case 2:
                    btn.Content = "噢噢结果出来了...！";
                    break;
                case 1:
                    btn.Content = "竟然是！！！";
                    break;
                case 0:
                    btnTimer.Stop();
                    RandomTimer.Stop();
                    btn.FontSize = 20;
                    btn.Content = "就是这个啦~！不能反悔了噢！";
                    break;

            }
            countDown -= 1;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && !string.IsNullOrWhiteSpace(add.Text))
            {
                if (stringList.Contains(add.Text))
                {
                    MessageBox.Show("喂喂喂，不能作弊噢！");
                    return;
                }
                stringList.Add(add.Text);
                add.Clear();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (stringList.Count < 2)
            {
                MessageBox.Show("额，最起码列表里面要有 2 个选项我才能帮你做决定哇。");
                return;
            }
            grid.IsEnabled = false;
            RandomTimer.Start();
            btnTimer.Start();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedIndex != -1)
            {
                stringList.RemoveAt(listBox.SelectedIndex);
            }
        }
    }
}
