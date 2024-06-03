using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Enums
{
    public enum UserType
    {
        [Description("Admnistrador")]
        Admin,
        [Description("Usuario")]
        User
    }
}
