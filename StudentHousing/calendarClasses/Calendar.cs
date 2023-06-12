using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing.calendarClasses
{
    internal class Calendar
    {
        private UserQueries queries;

        public List<(string, DateTime)> GetTaskDates()
        {
            List<(string, DateTime)> taskDates = new List<(string, DateTime)>();
            queries = new UserQueries();
            List<User> users = queries.getAllTheUsers();
            foreach (User user in users)
            {
                taskDates.Add((user.AssignedTasks[0].taskName, user.AssignedTasks[0].endTime));
            }
            return taskDates;
        }

    }
}
