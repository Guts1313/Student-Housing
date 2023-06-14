using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing
{
    [Serializable]
    public class Party
    {
        public User Organiser { private set; get; }
        public int Id { get; private set; }
        public int positiveVotes { get; private set; }
        public int negativeVotes { get; private set; }

        public DateTime PartyDay { get; private set; }

        public void CreateParty(User organiser, DateTime partyDate)
        {
            CreateUniquePartyId();
            this.PartyDay = partyDate;
            Organiser = organiser;
            organiser.party = this;
            positiveVotes = 1;
            negativeVotes = 0;
        } 

        public void AddPositiveVote()
        {
            positiveVotes++;
        }
        public void AddNegativeVote()
        {
            negativeVotes++;
        }

        private void CreateUniquePartyId()
        {
            Id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        }
    }
}
