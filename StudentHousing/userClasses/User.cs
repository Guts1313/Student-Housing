using StudentHousing.userClasses;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Xml.Linq;

namespace StudentHousing;

[Serializable]
public class User
{
    private List<string> adminList = new List<string>() 
    { "njuYFgEfwig7LGxIDKirU2SQjyH2", 
      "V9j8vToydGgWhB9W18TF8ZC1dwC2", 
      "BGxH0fJqjPak2L27FQ8p7bQDm3Y2" 
    };

    private string id;
    private string firstName;
    private string secondName;
    private string email;
    private bool isAdmin;
    public bool wasAssignedPrev;
    public bool payedForGroceries = false;
    public Party party = new Party();
    public List<PartyIdVoted> userVoted { get; set; }
    public List<Task> AssignedTasks { get; private set; }

    public string Id => id;
    public bool IsAdmin => isAdmin;
    public string FirstName => firstName;
    public string SecondName => secondName;
    public string Email => email;

    public User(string id, string firstName, string secondName, string email)
    {
        this.id = id;
        this.firstName = firstName;
        this.secondName = secondName;
        this.email = email;
        if (CheckIsAdmin(id))
            isAdmin = true;

        AssignedTasks = new List<Task>();
        userVoted = new List<PartyIdVoted>();
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

    public void SetTask(Task task)
    {
        task.ChangeTaskStatus(TaskStatus.Assigned);
        AssignedTasks.Add(task);
    }

    public void unassignTask(Task task)
    {
        AssignedTasks.Remove(task);
    }

    public void AcceptTask(Task task)
    {
        if (!AssignedTasks.Contains(task))
        { throw new InvalidOperationException("User has not been assigned this task"); }

        task.ChangeTaskStatus(TaskStatus.Accepted);
    }

    public void DeclineTask(Task task)
    {
        if (!AssignedTasks.Contains(task))
            throw new InvalidOperationException("User has not been assigned this task");

        task.ChangeTaskStatus(TaskStatus.Declined);
        //AssignedTasks.Remove(task);
    }

    public void CompleteTask(Task task)
    {
        task.ChangeTaskStatus(TaskStatus.Completed);
        AssignedTasks.Remove(task);
    }


    public override string ToString()
    {
        return $"ID: {id} Name: {secondName} {firstName} email:{email}";
    }
}