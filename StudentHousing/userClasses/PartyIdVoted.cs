using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing.userClasses
{
    [Serializable]
    public class PartyIdVoted
    {
        public int partyId { get; set; }
        public bool voted { get; set; }
    }
}
