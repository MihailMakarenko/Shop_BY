using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Presentation.ActionFilters
{
    public class UserAccessAttribute : TypeFilterAttribute
    {
        public UserAccessAttribute() : base(typeof(UserAccessFilter))
        {
        }
    }
}
