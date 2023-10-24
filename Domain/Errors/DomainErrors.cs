using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static Result NotFound => Result.NotFound("User not found");

           
        }
    }
}
