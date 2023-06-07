using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousing.PartyClasses
{
    public class Party
    {
        public User partyOrganiser { private set; get; }
        public int Id { get; private set; }
    }
}
