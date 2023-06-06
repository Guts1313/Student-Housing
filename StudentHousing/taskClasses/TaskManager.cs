using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StudentHousing
{
    [Serializable]
    public class TaskManager
    {
        private List<Task> _listOfTasks = new List<Task>();
        private UserManager uManager = new UserManager();
        private TaskQueries taskQueries = new TaskQueries();


        public TaskManager()
        {
            foreach (User user in taskQueries.getUsersAssigned())
            {
                _listOfTasks.AddRange(user.AssignedTasks);
            }
        }

        public void firstAssignment()
        {
            if (_listOfTasks.Count == 0)
            {
                List<User> users = uManager.GetUserList();
                _listOfTasks = new List<Task>()
                {
                    // change later 
                    new Task("Groceries", DateTime.Now, DateTime.Now.AddSeconds(10)),
                    new Task("Trash", DateTime.Now, DateTime.Now.AddSeconds(30)),
                    new Task("Cleaning", DateTime.Now, DateTime.Now.AddSeconds(50))
                };

                for (int i = 0; i < 3; i++)
                {
                    _listOfTasks[i].AssignedUser = users[i];
                    users[i].SetTask(_listOfTasks[i]);
                    taskQueries.assignUser(users[i]);
                    uManager.changeUser(users[i]);
                }
            }
        }

        public void SetTasks(List<Task> tasks)
        {
            this._listOfTasks = tasks;
        }
        
        public void CheckAndReassignTasks()
        {
            var now = DateTime.Now;

            foreach (var task in _listOfTasks)
            {
                // Task is not completed and end time is passed
                if (task.TaskStatus == TaskStatus.Completed || task.TaskStatus == TaskStatus.Declined || task.EndTime < now)
                {
                    task.changeDate(DateTime.Now, DateTime.Now.AddSeconds(20));
                    ReassignTask(task);
                    break;
                }
            }
        }

        private void ReassignTask(Task task)
        {
            // All users who are not currently assigned with this task
            List<User> allUsers = getAllUsersThatCanBeAssignedATask(task);


            if (allUsers.Count == 0)
            {
                // No available users -> end
                return;
            }

            // Unassign the old user
            var oldUser = task.AssignedUser;
            if (oldUser != null)
            {
                task.ChangeTaskStatus(TaskStatus.Completed);
                oldUser.wasAssignedPrev = true;
                uManager.changeUser(oldUser);
            }

            //reset paymant var
            if (task.TaskName == "Groceries")
            { uManager.setAllGroceriesPaymentToFalse(); }    
            
            // Assign a new user
            var rng = new Random();
            var userIndex = rng.Next(allUsers.Count);
            var newUser = allUsers[userIndex];
            task.AssignedUser = newUser;

            // Assign a new task to user 
            assignNewTaskInAListTask(newUser, oldUser, task);

            uManager.changeUser(newUser);
            taskQueries.changeUser(newUser);
            //MessageBox.Show(taskQueries.getUsersAssigned().Count.ToString());
        }

        private List<User> getAllUsersThatCanBeAssignedATask(Task task)
        {
            List<User> allUsers = new List<User>();
            List<User> tmpUsers = uManager.GetUserList();
            foreach (var user in tmpUsers)
            {
                if (user.AssignedTasks.Count == 0 && !user.wasAssignedPrev)
                {
                    allUsers.Add(user);
                }
                if (user.AssignedTasks.Count != 0 &&
                    (user.AssignedTasks[0].TaskStatus == TaskStatus.Completed || user.AssignedTasks[0].TaskStatus == TaskStatus.Declined) &&
                    task.TaskName == user.AssignedTasks[0].TaskName)
                {
                    user.unassignTask(user.AssignedTasks[0]);
                    user.wasAssignedPrev = false;
                    uManager.changeUser(user);
                }
            }
            return allUsers;
        }

        private void assignNewTaskInAListTask(User newUser, User oldUser, Task task)
        {
            for (int i = 0; i < _listOfTasks.Count; i++)
            {
                if (oldUser == null)
                {
                    Task newTask = new Task(task.TaskName, task.StartTime, task.EndTime);
                    newUser.SetTask(newTask);
                    newTask.AssignedUser = newUser;
                    _listOfTasks.Remove(task);
                    _listOfTasks.Add(newTask);
                    break;
                }
                else if (_listOfTasks[i].TaskName == oldUser.AssignedTasks[0].TaskName)
                {
                    Task newTask = new Task(task.TaskName, task.StartTime, task.EndTime);
                    newUser.SetTask(newTask);
                    newTask.AssignedUser = newUser;
                    _listOfTasks[i] = newTask;
                    break;
                }
            }
        }

        /*public void AssignTask(User user, Task task)
        {
            if (user == null || task == null) throw new ArgumentNullException($"Task/User is null");
            user.SetTask(task);
            uManager.changeUser(user);
            _listOfTasks.Add(task);
        }*/

        public List<Task> GetAllTasks()
        {
            return _listOfTasks;
        }

        public List<Task> GetUserTasks(User user)
        {
            if (user == null) throw new NullReferenceException($"User is null");
            return user.AssignedTasks;
        }
    }
}