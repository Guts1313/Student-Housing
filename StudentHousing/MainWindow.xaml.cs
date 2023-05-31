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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
        private TaskManager taskManager = new TaskManager();
        private static DispatcherTimer changeTaskTimer;

        public MainWindow()
        {
            InitializeComponent();
            MyDataItems = new ObservableCollection<string>();
            FirebaseUI.Instance.Client.AuthStateChanged += this.AuthStateChanged;

            changeTaskTimer = new DispatcherTimer();
            TimeSpan interval = TimeSpan.FromSeconds(3000);
            changeTaskTimer.Tick += changeTask;
            changeTaskTimer.Interval = interval;
            changeTaskTimer.Start();
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

                if (user.IsAdmin) uiDispatcher.Invoke(() => { showAdmibTab(); }); // should be called before initializing the window (or won't be possible to change)

                uiDispatcher.Invoke(() => 
                {
                    closeLoginPage();
                    this.Show();
                    showUserInfAcc();
                });
                MessageBox.Show(userManager.GetUserList().Count.ToString());
            }
        }

        private static void changeTask(object sender, EventArgs e)
        {
            
        }

        // neaded beacause of thread issues
        public void closeLoginPage()
        {
            foreach (var window in Application.Current.Windows.OfType<Window>().ToList())
            {
                if (window.Title == "LoginPage")
                {
                    window.Close();
                    break;
                }
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

        private void VoteButtonFor_click(object sender, RoutedEventArgs e)
        {

        }

        private void VoteButtonAgainst_click(object sender, RoutedEventArgs e)
        {

        }

        private void garbageAccept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void garbageDecline_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
