using StudentHousing;
using System;
using System.Collections.Generic;

namespace StudentHousing
{
    public class UserManager
    {
        private UserQueries userQueries = new UserQueries();

        public List<User> GetUserList()
        {
            return userQueries.getAllTheUsers();
        }

        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException("User is null");
            userQueries.addUserToSCV(user);
        }

        public void changeUser(User user)
        {
            userQueries.changeUser(user);
        }

        public void refreshUsers()
        {
            userQueries.refreshUsers();
        }

        public void setAllGroceriesPaymentToFalse()
        {
            foreach (User userIter in userQueries.getAllTheUsers())
            {
                userIter.payedForGroceries = false;
                changeUser(userIter);
            }
        }

        public User refeshCurrentUser(User user)
        {
            foreach (User userIter in userQueries.getAllTheUsers())
            {
                if (user.Id == userIter.Id)
                {
                    user = userIter;
                }
            }
            return user;
        }
    }
}