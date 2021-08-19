using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chuanhoafile.Models
{
    public class places : IdentityBase
    {
        
        public string NameOutput { get; set; }

        public string Code { get; set; }

        public string FatherId { get; set; }

            
    }
}
