using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace TaskManager.Helpers
{
    public class UserInfoHelper
    {

        public int GetUserId()
        {
            //return 1;

            if (Membership.GetUser() == null)
                return 0;

            MembershipUser mu = Membership.GetUser(Thread.CurrentPrincipal.Identity.Name);
            if (mu != null)
                return (int)mu.ProviderUserKey;
            else
                return 0;
        }

    }
}