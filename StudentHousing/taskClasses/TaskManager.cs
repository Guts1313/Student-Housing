using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentHousing
{
    [Serializable]
    public class TaskManager
    {
        private List<Task> _listOfTasks;


        public TaskManager()
        {
            _listOfTasks = new List<Task>();
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
                    ReassignTask(task);
                }
            }
        }

        public void ReassignTask(Task task)
        {
            // All users who are not currently assigned with this task
            UserManager uManager = new UserManager();
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
                oldUser.DeclineTask(task);
            }
            
            // Assign a new user
            var rng = new Random();
            var userIndex = rng.Next(allUsers.Count);
            var newUser = allUsers[userIndex];
            task.AssignedUser = newUser;
            newUser.GetTask(task);
        }

        public void AssignTask(User user, Task task)
        {
            if (user == null || task == null) throw new ArgumentNullException($"Task/User is null");
            user.GetTask(task);
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