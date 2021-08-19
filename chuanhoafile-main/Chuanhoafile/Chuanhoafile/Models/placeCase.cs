using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chuanhoafile.Models
{
    public class placeCase : IdentityBase
    {
        public string nameCase { get; set; }
        
        public string placeCode { get; set; }
    }
}
