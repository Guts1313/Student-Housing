using System;
using System.Collections.Generic;
namespace StudentHousing
{
    public class PartyManager
    {
        private PartyQueries partyQueries = new PartyQueries();
        public List<Party> PartyList { get; set; }


        public List<Party> GetPartyList()
        {
            return partyQueries.getAllTheParties();
        }

        public void AddParty(Party party)
        {
            if (party == null) throw new ArgumentNullException("Party is null");

            partyQueries.addPartyToCSV(party);
        }

        public void changeParty(Party party)
        {
            partyQueries.changeParty(party);
        }

        public void refreshParties()
        {
            partyQueries.refreshParties();
        }

        public Party refreshCurrentParty(Party party)
        {
            foreach (Party partyIter in partyQueries.getAllTheParties())
            {
                if (partyIter.Id == party.Id)
                {
                    party = partyIter;
                }
            }
            return party;
        }
    }
}
