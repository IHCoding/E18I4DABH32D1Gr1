using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personkartotek.Models
{
    //public enum EmailType { Private, Work }

    public class Email
    {
        public int EmailId { get; set; }
        public string EmailAddress { get; set; }
        public string EmailType { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
