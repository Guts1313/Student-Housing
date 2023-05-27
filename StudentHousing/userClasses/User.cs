using System.Collections.Generic;

namespace StudentHousing;

public class User
{
    private List<string> adminList = new List<string>() 
    { "BGxH0fJqjPak2L27FQ8p7bQDm3Y2", 
      "V9j8vToydGgWhB9W18TF8ZC1dwC2", 
      "BGxH0fJqjPak2L27FQ8p7bQDm3Y2" 
    }; 
    private string id;
    private string firstName;
    private string secondName;
    private string email;
    private bool isAdmin;

    public User(string id, string firstName, string secondName, string email)
    {
        this.id = id;
        this.firstName = firstName;
        this.secondName = secondName;
        this.email = email;
        if (CheckIsAdmin(id))
            isAdmin = true;
    }

    private bool CheckIsAdmin(string userId)
    {
        foreach (string adminId in adminList)
        {
            if (adminId == userId)
                return true;
        }

        return false;
    }
    
    public string Id
    {
        get => id;
        private set => id = value;
    }
}