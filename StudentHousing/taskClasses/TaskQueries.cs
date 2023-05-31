using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing
{
    internal class TaskQueries
    {
        private string pathToUsersFile = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));

        public TaskQueries() 
        {
            pathToUsersFile = System.IO.Path.Combine(pathToUsersFile, "DataCSV", "partiesAssigned.bin");
        }

        public List<User> getUsersAssigned()
        {
            List<User> users = new List<User>();

            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        users.Add((User)formatter.Deserialize(fs));
                    }
                }
            }
            catch (Exception ex) { throw new IOException("Couldn't get all the users"); }

            return users;
        }

        public void changeUser(User userChange)
        {
            List<User> users = getUsersAssigned();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].ToString() == userChange.ToString())
                {
                    users[i] = userChange;
                    break;
                }
            }

            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    foreach (User user in users)
                    {
                        formatter.Serialize(fs, userChange);
                    }
                }
            }
            catch (Exception ex) { throw new IOException("Couldn't change user"); }
        }

        public void assignUser(User user)
        {
            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Append))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, user);
                }
            }
            catch (Exception ex) { throw new IOException("Couldn't add a new user"); }
        }
    }
}
