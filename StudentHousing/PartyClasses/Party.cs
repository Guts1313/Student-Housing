using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing.PartyClasses
{
    [Serializable]
    public class Party
    {
        public User Organiser { private set; get; }
        public int Id { get; private set; }
        public int Votes { get; private set; }
        public DateTime PartyDay { get; private set; }
        public Party(User organiser, int votes)
        {
            CreateUniquePartyId();
            Organiser = organiser;
            Votes = votes;
        }
        public void CreateParty(DateTime partyDate)
        {
            this.PartyDay = partyDate;
        } 

        public void AddVote()
        {
            Votes++;
        }
        private void CreateUniquePartyId()
        {
            Id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        }
    }
}
