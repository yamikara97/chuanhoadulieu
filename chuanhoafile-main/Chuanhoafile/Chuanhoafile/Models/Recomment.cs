using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chuanhoafile.Models
{
    public class Recomment:IdentityBase
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
    }
}
