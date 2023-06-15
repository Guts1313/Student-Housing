using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing
{
    internal class Calendar
    {
        private TaskQueries queries;

        public List<(string, DateTime, string)> GetTaskDates()
        {
            List<(string, DateTime, string)> taskDates = new List<(string, DateTime, string)>();
            queries = new TaskQueries();
            List<User> users = queries.getUsersAssigned();
            foreach (User user in users)
            {
                taskDates.Add((user.AssignedTasks[0].taskName, user.AssignedTasks[0].endTime, user.Email));
            }
            return taskDates;
        }

    }
}
