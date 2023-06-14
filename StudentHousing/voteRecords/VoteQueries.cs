using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing.voteRecords
{
    public class VoteQueries
    {
        private string pathToUsersFile = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
        public VoteQueries()
        {
            pathToUsersFile = System.IO.Path.Combine(pathToUsersFile, "DataCSV", "partiesAssigned.bin");
        }
        public bool checkIfVoteExists(Vote vote)
        {
            if (vote == null) return false;

            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length == 0 || fs.Length == 1 || fs.Length == 2 || fs.Length == 3)
                        return false;

                    BinaryFormatter formatter = new BinaryFormatter();
                    Vote tmpVote;
                    while (fs.Position < fs.Length)
                    {
                        tmpVote = (Vote)formatter.Deserialize(fs);
                        if (tmpVote.ToString() == vote.ToString())
                            return true;
                    }
                    return false;
                }
            }
            catch (IOException ex)
            { throw new IOException("Couldn't check if the vote exists"); }
        }

        public void addVoteToCSV(Vote vote)
        {
            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Append))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, vote);
                }
            }
            catch (Exception ex) { throw new IOException("Couldn't add new vote!"); }
        }
        public List<Vote> getAllTheVotes()
        {
            List<Vote> votes = new List<Vote>();

            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    int i = 0;
                    while (fs.Position < fs.Length)
                    {
                        i++;
                        votes.Add((Vote)formatter.Deserialize(fs));
                    }
                }
            }
            catch (Exception ex) { throw new IOException("Couldn't get all the users"); }

            return votes;
        }
    }
}
