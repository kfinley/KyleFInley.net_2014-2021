using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _928.Security
{
    public interface IUser
    {
        Guid Id { get; set; }

        string UserName { get; }

        string Password { get; }

        string Email { get; }

        string FullName { get; }
    
    }
}
