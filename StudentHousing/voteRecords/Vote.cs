using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing.voteRecords
{
    [Serializable]
    public class Vote
    {
        public int PartyId { get; private set; }
        public int UserId { get; private set; }
        public Vote(int partyId, int userId) 
        {
            PartyId = partyId;
            UserId = userId;
        }

    }
}
