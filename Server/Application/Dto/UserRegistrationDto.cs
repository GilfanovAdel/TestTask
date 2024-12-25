using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class UserRegistrationDto
    {
        public Guid Organizationid { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
