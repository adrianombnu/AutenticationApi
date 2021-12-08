using AutenticationApi.Entidades;
using AutenticationApi.Repositorio;
using System;
using System.Collections.Generic;

namespace AutenticationApi.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;    
        private readonly JwtTokenService _tokenService;    

        public UserService(UserRepository repository, JwtTokenService tokenService)
        {
            _userRepository = repository;
            _tokenService = tokenService;
        }

        public User Create(User user)
        {
            return _userRepository.Create(user);

        }

        public IEnumerable<User> Get()
        {
            return _userRepository.Get();

        }

        public User Get(Guid id)
        {
            return _userRepository.Get(id);

        }

        public object Login(string username, string password)
        {
            var user = _userRepository.Login(username, password);
            var token = _tokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user,
                token
            };

        }

    }
}
