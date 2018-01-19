using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public enum Membership : byte
    {
        None = 0,
        PayAsYouGo = 1,
        Quaterly = 2,
        Annually = 3,
        Monthly = 2,
    }
}