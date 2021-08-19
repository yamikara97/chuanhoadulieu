using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chuanhoafile.Models
{
    public class UserViewModel
    {
        public ApplicationUser user { get; set; }
        public IdentityRole<Guid> role { get; set; }
    }
}
