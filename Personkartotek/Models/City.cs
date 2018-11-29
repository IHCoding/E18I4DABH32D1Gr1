using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personkartotek.Models
{
    public class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public string Postalcode { get; set; }

        public string ProvinceState { get; set; }

        public string Country { get; set; }

        public IEnumerable<Address> Addresses { get; set; }
    }
}
