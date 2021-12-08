using AutenticationApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutenticationApi.Repositorio
{
    public class UserRepository
    {
        private readonly Dictionary<Guid, User> _users;
        public UserRepository()
        {
            _users ??= new Dictionary<Guid, User>();
        }

        public IEnumerable<User> Get()
        {
            return _users.Values;

        }

        public User Get(Guid id)
        {
            if (_users.TryGetValue(id, out var user))
                return user;

            throw new Exception("Usuário não encontrado.");

        }

        public User Create(User user)
        {
            user.Id = Guid.NewGuid();
            if (_users.TryAdd(user.Id, user))
                return user;

            throw new Exception("");
        }


        public bool Remove(Guid id)
        {
            return _users.Remove(id);

        }

        public User Update(Guid id, User user)
        {
            if (_users.TryGetValue(id, out var userToUpdate))
            {
                userToUpdate.Role = user.Role;
                userToUpdate.UserName = user.UserName;
                userToUpdate.Password = user.Password;

                return Get(id);
            }

            throw new Exception("Usuário não encontrado.");
        }

        public User Login(string username, string password)
        {
            var user = _users.Values.Where(u => u.UserName == username && u.Password == password).SingleOrDefault();

            if (user is null)
                throw new Exception("Usuário não encontrado.");

            return user;


        }
    }

}