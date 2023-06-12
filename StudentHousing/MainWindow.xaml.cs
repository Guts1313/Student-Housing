using Firebase.Auth.UI;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentHousing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> MyDataItems { get; set; }
        private User user;
        private UserManager userManager = new UserManager();
        private Calendar calendar;

        public MainWindow()
        {
            InitializeComponent();
            calendar = new Calendar();
            SetTask("Monday", "Cleaning", "John Johnson");
            SetTask("Wednesday", "Groceries", "Alice Alison");
            SetTask("Thursday", "Garbage", "Bob Bobinson");
            MyDataItems = new ObservableCollection<string>();
            FirebaseUI.Instance.Client.AuthStateChanged += this.AuthStateChanged;
        }

        // checks if the user is logged in
        private void AuthStateChanged(object sender, Firebase.Auth.UserEventArgs e)
        {
            var uiDispatcher = Dispatcher;
            if (e.User == null)
            {
                uiDispatcher.Invoke(() =>
                {
                    Window loginpage = new LoginPage();
                    loginpage.Show();
                    this.Hide();
                });
            }
            else // user is created here
            {
                var userinf = e.User.Info;
                user = new User(userinf.Uid, userinf.FirstName, userinf.LastName, userinf.Email);
                userManager.AddUser(user);

                if (user.IsAdmin) showAdmibTab(); // should be called before initializing the window (or won't be possible to change)

                uiDispatcher.Invoke(() => { this.Show(); });
                showUserInfAcc();
            }
        }

        public void showUserInfAcc()
        {
            firstName.Text = user.FirstName;
            secondName.Text = user.SecondName;
            email.Text = user.Email;
        }

        // moves all the elements in the window so that an admin panel fits
        public void showAdmibTab()
        {
            Style borderMarginStyle = (Style)FindResource("BorderMargin");
            Setter marginSetter = borderMarginStyle.Setters.FirstOrDefault() as Setter;

            marginSetter.Value = new Thickness(10, 11, 10, 0);
            adminItem.Visibility = Visibility.Visible;
        }

        // shows the name of the selected tab
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedItem = e.AddedItems[0] as TabItem;
            string selectedHeader = selectedItem.Header.ToString();
            TabName.Text = selectedHeader;
        }

        // prevents default action of the listbox (it always wants to do smth when item is clicked)
        private void PreventDef(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ColorDaysWithTasks();
        }

        private void ColorDaysWithTasks()
        {
            // Check and color the days with tasks
            if (!string.IsNullOrEmpty(MondayTask.Text))
                SetDayBackground(MondayButton, MondayTask.Text);
            if (!string.IsNullOrEmpty(TuesdayTask.Text))
                SetDayBackground(TuesdayButton, TuesdayTask.Text);
            if (!string.IsNullOrEmpty(WednesdayTask.Text))
                SetDayBackground(WednesdayButton, WednesdayTask.Text);
            if (!string.IsNullOrEmpty(ThursdayTask.Text))
                SetDayBackground(ThursdayButton, ThursdayTask.Text);
            if (!string.IsNullOrEmpty(FridayTask.Text))
                SetDayBackground(FridayButton, FridayTask.Text);
            if (!string.IsNullOrEmpty(SaturdayTask.Text))
                SetDayBackground(SaturdayButton, SaturdayTask.Text);
            if (!string.IsNullOrEmpty(SundayTask.Text))
                SetDayBackground(SundayButton, SundayTask.Text);
        }

        private void SetDayBackground(Button button, string task)
        {
            // Set the background color of the button based on the task
            if (task.ToLower() == "garbage")
            {
                Color color = Color.FromArgb(125, 0, 255, 0); // RGB values for green
                button.Background = new SolidColorBrush(color);
           
            }
            else if (task.ToLower() == "cleaning")
            {
                Color color = Color.FromArgb(125, 0, 0, 200); // RGB values for blue
                button.Background = new SolidColorBrush(color);
               
            }
            else if (task.ToLower() == "groceries")
            {
                Color color = Color.FromArgb(125, 255, 255, 0); // RGB values for yellow
                button.Background = new SolidColorBrush(color);
                
            }
        }




        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Content is StackPanel stackPanel)
            {
                // Check if the button has a task assigned
                if (stackPanel.Children.Count > 1 && stackPanel.Children[1] is TextBlock taskTextBlock && !string.IsNullOrEmpty(taskTextBlock.Text))
                {
                    string task = taskTextBlock.Text.ToLower();

                    // Redirect to the corresponding tab based on the task
                    if (task == "garbage")
                        MenuTabs.SelectedIndex = 1; // Redirect to tab 1
                    else if (task == "cleaning")
                        MenuTabs.SelectedIndex = 4; // Redirect to tab 3
                    else if (task == "groceries")
                        MenuTabs.SelectedIndex = 3; // Redirect to tab 4
                }
            }
        }


        private void SetTask(string dayOfWeek, string task, string name)
        {
            var dayTextBlock = FindName($"{dayOfWeek}Task") as TextBlock;
            var nameTextBlock = FindName($"{dayOfWeek}Name") as TextBlock;

            if (dayTextBlock != null && nameTextBlock != null)
            {
                dayTextBlock.Text = task;
                nameTextBlock.Text = name;

               
            }

        }

        private void VoteButtonFor_click(object sender, RoutedEventArgs e)
        {

        }

        private void VoteButtonAgainst_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
