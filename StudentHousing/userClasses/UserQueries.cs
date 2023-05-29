using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Provider;
using Firebase.Auth;

namespace StudentHousing
{
    internal class UserQueries
    {
        private string pathToUsersFile = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));

        public UserQueries()
        {
            pathToUsersFile = System.IO.Path.Combine(pathToUsersFile, "DataCSV", "users.bin");
        }

        private bool checkIfUserExists(User user)
        {
            if (user == null) return false;

            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length == 0 || fs.Length == 1 || fs.Length == 2 || fs.Length == 3)
                        return false;

                    BinaryFormatter formatter = new BinaryFormatter();
                    User tmpUser;
                    while (fs.Position < fs.Length)
                    {
                        tmpUser = (User)formatter.Deserialize(fs);
                        if (tmpUser.ToString() == user.ToString())
                            return true;
                    }
                    return false;
                }
            }
            catch (IOException ex)
            { throw new IOException("Couldn't check if the user exists"); }
        }

        public void addUserToSCV(User user)
        {
            if (!checkIfUserExists(user))
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

        public List<User> getAllTheUsers()
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
    }
}
