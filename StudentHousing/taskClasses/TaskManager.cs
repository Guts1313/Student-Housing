using System;
using System.Collections.Generic;

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