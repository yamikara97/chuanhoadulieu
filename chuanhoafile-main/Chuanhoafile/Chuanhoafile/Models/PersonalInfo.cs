using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chuanhoafile.Models
{
    public class PersonalInfo : IdentityBase
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public DateTime DateofBirth { get; set; }
    }
}
