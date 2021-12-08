using AutenticationApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutenticationApi.Repositorio
{
    public class UserRepository
    {
        private readonly List<User> _users;
        public UserRepository()
        {
            _users ??= new List<User>();
        }

        
    }
}
