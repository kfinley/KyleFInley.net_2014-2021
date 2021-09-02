using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _928.Web.Authentication
{
    public interface IFormsAuthentication
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);

        void SignOut();
    }
}
