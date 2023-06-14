using Firebase.Auth.UI;
using Microsoft.Web.WebView2.Core;
using StudentHousing.voteRecords;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        private Calendar calendar;
        private TaskManager taskManager = new TaskManager();
        private PartyManager partyManager = new PartyManager();
        private groceriesList groceries = new groceriesList();
        private DispatcherTimer changeTaskTimer;
        private bool flashForTimer = true;
        private Dispatcher uiDispatcher;
<<<<<<< HEAD
        private VoteManager VoteManager { get; set; }
=======
        private List<(string, DateTime, string, string)> taskDates = new List<(string, DateTime, string, string)>();
>>>>>>> 048d925d25c22f57f77c56f65af94579daace04d

        public MainWindow()
        {
            InitializeComponent();
            calendar = new Calendar();
            taskDates = calendar.GetTaskDates();
            

            DataContext = this;
            userManager.refreshUsers(); // uncomment if changes happened in user class
            taskManager.firstAssignment(); // starts the cycle of assigning users (if the cycle is hasn't started yet
            MyDataItems = new ObservableCollection<string>();
            addToCollectionAndShow();
            FirebaseUI.Instance.Client.AuthStateChanged += this.AuthStateChanged;

            if (uiDispatcher == null)
            { uiDispatcher = Dispatcher; }
            VoteManager = new VoteManager();
        }

      
        private void addToCollectionAndShow()
        {
            foreach (var item in groceries.GetGroceriesStr())
            {
                MyDataItems.Add(item);
            }
        }

        public void showTheAssignedUsers() 
        {
            List<Task> tasks = taskManager.GetAllTasks();
            user = userManager.refeshCurrentUser(user);

            foreach (Task task in tasks)
            {
                
                switch (task.TaskName)
                {
                    case "Trash":
                        UserNameGarbage.Text = "";
                        TrashDay.Text = "";
                        TrashMonth.Text = "";

                        if (user.Id != task.AssignedUser.Id)
                        {
                            garbageAccept.Visibility = Visibility.Hidden;
                            garbageDecline.Visibility = Visibility.Hidden;
                            garbageAcceptedText.Visibility = Visibility.Hidden;
                            UserNameGarbage.Text = $"Next garbage disposal assigned to, \n{task.AssignedUser.Email}"; 
                        }
                        else
                        {
                            if (garbageAccept.Visibility != Visibility.Collapsed)
                            {
                                garbageAccept.Visibility = Visibility.Visible;
                                garbageDecline.Visibility = Visibility.Visible;
                                garbageAcceptedText.Visibility = Visibility.Hidden;
                            }
                            UserNameGarbage.Text = $"Next garbage disposal assigned to, \nYou"; 
                        }
                        TrashDay.Text = $"day: {task.EndTime.Day}";
                        TrashMonth.Text = $"month: {task.EndTime.Month}";
                        break;

                    case "Cleaning":
                        UserNameCLeaning.Text = "";
                        CleaningDay.Text = "";
                        CleaningMonth.Text = "";

                        if (user.Id != task.AssignedUser.Id)
                        {
                            cleaningAccept.Visibility = Visibility.Hidden;
                            cleaningDecline.Visibility = Visibility.Hidden;
                            cleaningAcceptedText.Visibility = Visibility.Hidden;
                            UserNameCLeaning.Text = $"Next cleaning is assigned to, \n{task.AssignedUser.Email}"; 
                        }
                        else
                        {
                            if (cleaningAccept.Visibility != Visibility.Collapsed)
                            {
                                cleaningAccept.Visibility = Visibility.Visible;
                                cleaningDecline.Visibility = Visibility.Visible;
                                cleaningAcceptedText.Visibility = Visibility.Hidden;
                            }
                            UserNameCLeaning.Text = $"Next cleaning is assigned to, \nYou"; 
                        }
                        CleaningDay.Text = $"day: {task.EndTime.Day}";
                        CleaningMonth.Text = $"month: {task.EndTime.Month}";
                        break;

                    case "Groceries":
                        UserNameGroceries.Text = "";
                        groceriesDay.Text = "";
                        groceriesMonth.Text = "";

                        if (user.Id != task.AssignedUser.Id)
                        {
                            groceriesAccept.Visibility = Visibility.Hidden;
                            groceriesDecline.Visibility = Visibility.Hidden;
                            if (!user.payedForGroceries) { PayButton.Visibility = Visibility.Visible; }
                            groceriesAcceptanceGrid.Visibility = Visibility.Hidden;
                            groceriesMainGrid.Visibility = Visibility.Visible;
                            groceriesAcceptedText.Visibility = Visibility.Hidden;
                            date.Text = $"{task.EndTime.Day} {task.EndTime.ToString("MMMM")} {task.EndTime.Year} \n{task.AssignedUser.Email}";
                            amount.Text = $"You pay: {groceries.CountTotalForEachUser()}";
                        }
                        else
                        {
                            if (groceriesAccept.Visibility != Visibility.Collapsed)
                            {
                                groceriesMainGrid.Visibility = Visibility.Hidden;
                                groceriesAcceptanceGrid.Visibility = Visibility.Visible;
                                groceriesAccept.Visibility = Visibility.Visible;
                                groceriesDecline.Visibility = Visibility.Visible;
                                groceriesAcceptedText.Visibility = Visibility.Hidden;
                            }
                            PayButton.Visibility = Visibility.Hidden;
                            UserNameGroceries.Text = $"Next groceries must be done by \nYou";
                            date.Text = $"{task.EndTime.Day} {task.EndTime.ToString("MMMM")} {task.EndTime.Year} \nYou";
                            amount.Text = $"You pay: {groceries.CountTotal()}";
                        }
                        groceriesDay.Text = $"day: {task.EndTime.Day}";
                        groceriesMonth.Text = $"month: {task.EndTime.Month}";
                        break;
                }
            }
        }

        // checks if the user is logged in
        private void AuthStateChanged(object sender, Firebase.Auth.UserEventArgs e)
        {
            uiDispatcher = Dispatcher;
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
                
                if (user.IsAdmin) uiDispatcher.Invoke(() => {  showAdmibTab(); }); // should be called before initializing the window (or won't be possible to change)

                uiDispatcher.Invoke(() => 
                {
                    closeLoginPage();
                    this.Show();
                    showUserInfAcc();
                    showTheAssignedUsers();
                });

                TimeSpan interval = TimeSpan.FromSeconds(1);
                changeTaskTimer = new DispatcherTimer();
                changeTaskTimer.Tick += changeTask;
                changeTaskTimer.Interval = interval;
                changeTaskTimer.Stop();
                changeTaskTimer.Start();
            }
        }

        private void changeTask(object sender, EventArgs e)
        {
            //MessageBox.Show(userManager.GetUserList().Count.ToString());
            if (flashForTimer)
            {
                flashForTimer = false;
                taskManager.CheckAndReassignTasks();
                showTheAssignedUsers();
                flashForTimer = true;
            }
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
<<<<<<< HEAD
=======

       

      

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
            Button clickedButton = (Button)sender;
            string task = GetTaskFromButton(clickedButton);

            // Determine the tab to navigate based on the task
            int tabIndex = GetTabIndexForTask(task);

            // Set the selected tab
            MenuTabs.SelectedIndex = tabIndex;
        }

        private string GetTaskFromButton(Button button)
        {
            // Retrieve the task associated with the clicked button
            // You may need to modify this logic based on how the task information is stored
            StackPanel stackPanel = (StackPanel)button.Content;
            TextBlock taskTextBlock = (TextBlock)stackPanel.Children[0];
            string task = taskTextBlock.Text;
            return task;
        }

        private int GetTabIndexForTask(string task)
        {
            // Map the task to the corresponding tab index
            switch (task)
            {
                case "Cleaning":
                    return 3; // Tab index 2 corresponds to "Cleaning" tab
                case "Trash":
                    return 1; // Tab index 0 corresponds to "Trash" tab
                case "Groceries":
                    return 4; // Tab index 3 corresponds to "Groceries" tab
                default:
                    return 0; // Default to the first tab (index 0)
            }
        }




        private void Grid_Loaded(object sender, RoutedEventArgs e)
            {
                Grid calendarGrid = (Grid)sender;

                List<(string taskName, DateTime date, string firstName, string lastName)> tasks = calendar.GetTaskDates();

                // Create a dictionary to store task information for each day of the week
                Dictionary<DayOfWeek, List<string>> taskInfoByDay = new Dictionary<DayOfWeek, List<string>>();

                // Initialize the dictionary with empty lists for each day of the week
                foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                {
                    taskInfoByDay[day] = new List<string>();
                }

                // Add task information to the dictionary based on the task date
                foreach (var task in tasks)
                {
                    DayOfWeek taskDay = task.date.DayOfWeek;
                    string taskInfo = task.taskName + Environment.NewLine + " - " + task.firstName + " " + task.lastName;

                    taskInfoByDay[taskDay].Add(taskInfo);
                }

                // Populate the calendar with the task information
                int rowOffset = 0; // Adjust this value based on the starting row of the calendar
                int columnOffset = 1; // Adjust this value based on the starting column of the calendar
                int maxTasksPerDay = 3; // Maximum number of tasks to display per day

                foreach (var taskInfo in taskInfoByDay)
                {
                    DayOfWeek day = taskInfo.Key;
                    List<string> tasksForDay = taskInfo.Value;

                    // Find the corresponding grid cell for the day
                    int row = (int)day + rowOffset;
                    int column = columnOffset;

                    // Iterate over the tasks for the day and display them in the calendar
                    for (int i = 0; i < tasksForDay.Count && i < maxTasksPerDay; i++)
                    {
                        // Create the task textblock
                        TextBlock taskTextBlock = new TextBlock
                        {
                            Text = tasksForDay[i],
                            HorizontalAlignment = HorizontalAlignment.Center,
                            FontSize = 10,
                            Margin = new Thickness(0, 0, 0, 0)
                        };

                        // Add the task textblock to the calendar grid
                        Grid.SetRow(taskTextBlock, row);
                        Grid.SetColumn(taskTextBlock, column);
                        calendarGrid.Children.Add(taskTextBlock);

                        // Increment the column to place the next task textblock
                        column += 2;
                    }
                }
            }


   
>>>>>>> 048d925d25c22f57f77c56f65af94579daace04d
        private void VoteButtonFor_click(object sender, RoutedEventArgs e)
        { 
            if (theCalendar.SelectedDate.HasValue)
            {
                DateTime date = theCalendar.SelectedDate.Value;
                foreach (var party in partyManager.GetPartyList())
                {
                    if (party.PartyDay == date)
                    {
                        if (VoteManager.VotedUser(new Vote(party.Id, Convert.ToInt32(user.Id))))
                        {
                            MessageBox.Show("You have already voted for that party!", "Error!",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            party.AddPositiveVote();
                            partyManager.changeParty(party);
                        }
                    }
                }
            }
        }

        private void VoteButtonAgainst_click(object sender, RoutedEventArgs e)
        {
            if (theCalendar.SelectedDate.HasValue)
            {
                DateTime date = theCalendar.SelectedDate.Value;
                foreach (var party in partyManager.GetPartyList())
                {
                    if (party.PartyDay == date)
                    {
                        if (VoteManager.VotedUser(new Vote(party.Id, Convert.ToInt32(user.Id))))
                        {
                            MessageBox.Show("You have already voted for that party!", "Error!",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            party.AddNegativeVote();
                            partyManager.changeParty(party);
                        }
                    }
                }
            }
        }

        private Task getTaskForThisPage(string name)
        {
            List<Task> tasks = taskManager.GetAllTasks();
            foreach (Task task in tasks)
            {
                if (task.TaskName == name)
                {
                    return task;
                }
            }
            return null;
        }

        private void garbageAccept_Click(object sender, RoutedEventArgs e)
        {
            Task task = getTaskForThisPage("Trash");
            task.AssignedUser.AcceptTask(task);
            garbageAccept.Visibility = Visibility.Collapsed;
            garbageDecline.Visibility = Visibility.Collapsed;
            garbageAcceptedText.Visibility = Visibility.Visible;
        }

        private void garbageDecline_Click(object sender, RoutedEventArgs e)
        {
            Task task = getTaskForThisPage("Trash");
            task.AssignedUser.DeclineTask(task);
        }

        private void groceriesAccept_Click(object sender, RoutedEventArgs e)
        {
            Task task = getTaskForThisPage("Groceries");
            task.AssignedUser.AcceptTask(task);
            groceriesAccept.Visibility = Visibility.Collapsed;
            groceriesDecline.Visibility = Visibility.Collapsed;
            groceriesAcceptedText.Visibility = Visibility.Visible;
            groceriesAcceptanceGrid.Visibility = Visibility.Hidden;
            groceriesMainGrid.Visibility = Visibility.Visible;
        }

        private void groceriesDecline_Click(object sender, RoutedEventArgs e)
        {
            Task task = getTaskForThisPage("Groceries");
            task.AssignedUser.DeclineTask(task);
        }

        private void cleaningAccept_Click(object sender, RoutedEventArgs e)
        {
            Task task = getTaskForThisPage("Cleaning");
            task.AssignedUser.AcceptTask(task);
            cleaningAccept.Visibility = Visibility.Collapsed;
            cleaningDecline.Visibility = Visibility.Collapsed;
            cleaningAcceptedText.Visibility = Visibility.Visible;
        }

        private void cleaningDecline_Click(object sender, RoutedEventArgs e)
        {
            Task task = getTaskForThisPage("Cleaning");
            task.AssignedUser.DeclineTask(task);
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            if (user.payedForGroceries)
            {PayButton.Visibility = Visibility.Hidden;}
            else
            {
                PayButton.Visibility = Visibility.Collapsed;
                user.payedForGroceries = true;
                userManager.changeUser(user);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            FirebaseUI.Instance.Client.AuthStateChanged -= this.AuthStateChanged;
            FirebaseUI.Instance.Client.SignOut();

            Thread.Sleep(1);

            uiDispatcher.Invoke(() =>
            {
                Window main = new MainWindow();
                this.Close();
            });
        }

        private void createParty_Click(object sender, RoutedEventArgs e)
        {
            if (theCalendar.SelectedDate.HasValue)
            {
                DateTime dateTime = theCalendar.SelectedDate.Value;
                Party party = new Party();
                party.CreateParty(user, dateTime);
                partyManager.AddParty(party);
                MessageBox.Show("You have created party successfully!", "Success!",
                     MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
