using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StudentHousing
{
    [Serializable]
    public class TaskManager
    {
        private List<Task> _listOfTasks = new List<Task>();
        UserManager uManager = new UserManager();
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
                if (task.TaskStatus == TaskStatus.Completed && task.EndTime < now)
                {
                    task.changeDate(DateTime.Now, DateTime.Now.AddDays(7));
                    ReassignTask(task);
                }
            }
        }

        private void ReassignTask(Task task)
        {
            // All users who are not currently assigned with this task
            List<User> allUsers = new List<User>();
            foreach (var user in uManager.GetUserList())
            {
                if (!user.AssignedTasks.Contains(task))
                {
                    allUsers.Add(user);
                }
            }

            if (allUsers.Count == 0)
            {
                // No available users -> end
                return;
            }

            // Unassign the old user
            var oldUser = task.AssignedUser;
            if (oldUser != null)
            {
                oldUser.unassignTask(task);
                oldUser.wasAssignedPrev = true;
                uManager.changeUser(oldUser);
            }
            
            // Assign a new user
            var rng = new Random();
            var userIndex = rng.Next(allUsers.Count);
            var newUser = allUsers[userIndex];
            task.AssignedUser = newUser;
            newUser.SetTask(task);

            uManager.changeUser(newUser);
            taskQueries.changeUser(newUser);
        }

        public void AssignTask(User user, Task task)
        {
            if (user == null || task == null) throw new ArgumentNullException($"Task/User is null");
            user.SetTask(task);
            uManager.changeUser(user);
            _listOfTasks.Add(task);
        }

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