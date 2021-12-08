using ProjetcLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcLibrary.Models
{
    public class User
    {
        public int Id
        {
            get;
            set;
        }

        public string Login
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public RoleEnum Role
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj is User user
                && user.Id == Id && user.Login == Login && user.PasswordHash == PasswordHash && user.Role == Role;
        }
    }
}
