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

namespace test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Weekday weekday = Weekday.Winter;

        public MainWindow()
        {
            InitializeComponent();
            button.Text = weekday.ToString();
        }

        private void press_Click(object sender, RoutedEventArgs e)
        {
            weekday++;
            if ((int)weekday == 4)
                weekday = (Weekday)0;
            button.Text = weekday.ToString();
        }
    }
}
