using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing.PartyClasses
{
    public class PartyQueries
    {
        private string pathToUsersFile = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
        public PartyQueries()
        {
            pathToUsersFile = System.IO.Path.Combine(pathToUsersFile, "DataCSV", "partiesAssigned.bin");
        }
        private bool checkIfPartyExists(Party party)
        {
            if (party == null) return false;

            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length == 0 || fs.Length == 1 || fs.Length == 2 || fs.Length == 3)
                        return false;

                    BinaryFormatter formatter = new BinaryFormatter();
                    Party tmpParty;
                    while (fs.Position < fs.Length)
                    {
                        tmpParty = (Party)formatter.Deserialize(fs);
                        if (tmpParty.ToString() == party.ToString())
                            return true;
                    }
                    return false;
                }
            }
            catch (IOException ex)
            { throw new IOException("Couldn't check if the party exists"); }
        }

        public void addPartyToCSV(Party party)
        {
            if (!checkIfPartyExists(party))
            {
                try
                {
                    using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Append))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(fs, party);
                    }
                }
                catch (Exception ex) { throw new IOException("Couldn't add a new party"); }
            }
        }

        public List<Party> getAllTheParties()
        {
            List<Party> parties = new List<Party>();

            //try
            //{
            using (FileStream fs = new FileStream(pathToUsersFile, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                int i = 0;
                while (fs.Position < fs.Length)
                {
                    i++;
                    parties.Add((Party)formatter.Deserialize(fs));
                }
            }
            //}
            //catch (Exception ex) { throw new IOException("Couldn't get all the users"); }

            return parties;
        }

        public void changeParty(Party partyChange)
        {
            List<Party> parties = getAllTheParties();

            for (int i = 0; i < parties.Count; i++)
            {
                if (parties[i].ToString() == partyChange.ToString())
                {
                    parties[i] = partyChange;
                    break;
                }
            }

            File.Delete(pathToUsersFile);

            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    foreach (Party party in parties)
                    {
                        formatter.Serialize(fs, party);
                    }
                }
            }
            catch (Exception ex) { throw new IOException("Couldn't change party"); }
        }

        public void refreshParties()
        {
            List<Party> parties = getAllTheParties();

            for (int i = 0; i < parties.Count; i++)
            {
                parties[i] = new Party(parties[i].Organiser, parties[i].Votes);
            }

            File.Delete(pathToUsersFile);

            try
            {
                using (FileStream fs = new FileStream(pathToUsersFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    foreach (Party party in parties)
                    {
                        formatter.Serialize(fs, party);
                    }
                }
            }
            catch (Exception ex) { throw new IOException("Couldn't refresh users"); }
        }
    }
}

