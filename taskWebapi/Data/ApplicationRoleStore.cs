using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskWebapi.Data
{
  public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbcontext>
  {
    public ApplicationRoleStore(ApplicationDbcontext context, IdentityErrorDescriber errorDescriber) : base(context, errorDescriber)
    {

    }
  }
}
