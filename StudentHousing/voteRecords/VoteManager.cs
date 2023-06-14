using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentHousing.voteRecords.VoteQueries;

namespace StudentHousing.voteRecords
{
    public class VoteManager
    {
        private VoteQueries voteQueries = new VoteQueries();
        public List<Vote> VoteList { get; private set; }

        public VoteManager() 
        {
            VoteList = new List<Vote>();
        }
        public Vote CreateVote(int partyId, int userId)
        {
            return new Vote(partyId, userId);
        }
        public List<Vote> GetVoteList()
        {
            return voteQueries.getAllTheVotes();
        }
        public bool VotedUser(Vote vote)
        {
            return voteQueries.checkIfVoteExists(vote);
        }

        public void AddVote(Vote vote)
        {
            if (vote == null) throw new ArgumentNullException("Vote is null");

            voteQueries.addVoteToCSV(vote);
        }

    }
}
