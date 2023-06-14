using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentHousing
{
    internal class groceriesList
    {
        UserManager userManager = new UserManager();
        List<groceriesInf> groceries = new List<groceriesInf>();

        public groceriesList()
        {
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
            groceries.Add(new groceriesInf() { name = "Toilet paper", price = 10 });
        }

        public List<string> GetGroceriesStr()
        {
            List<string> tmp = new List<string>();

            foreach (var item in groceries)
            {
                tmp.Add($"{item.name}               {item.price}");
            }

            return tmp;
        }

        public double CountTotalForEachUser()
        {
            double total = 0;
            int usersInSys = userManager.GetUserList().Count;

            foreach (var item in groceries)
            {
                total += item.price;
            }

            return Math.Round(total / usersInSys, 2, MidpointRounding.AwayFromZero);
        }

        public double CountTotal()
        {
            double total = 0;

            foreach (var item in groceries)
            {
                total += item.price;
            }

            return total;
        }
    }
}
