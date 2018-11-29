using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personkartotek.Models
{
    //public enum AddressType { PrimaryAddress, SecondaryAddress }

    public class Address
    {
        public Address()
        {
            this.PersonsResidingAtAddress = new HashSet<Person>();
        }

        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public int CityId { get; set; }
        public string AddressType { get; set; }

        public virtual City City { get; set; }

        public virtual IEnumerable<Person> PersonsResidingAtAddress { get; set; }
    }
}
