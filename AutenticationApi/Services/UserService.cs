using AutenticationApi.DTOs;
using AutenticationApi.Entidades;
using AutenticationApi.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public UserDTO Create(User user)
        {
            var userExists = _userRepository.GetByUsername(user.UserName);

            if (userExists != null)
                throw new Exception("O nome do usuário já está sendo utilizado, tente outro!");

            var userCreated = _userRepository.Create(user);

            return new UserDTO
            {
                Role = userCreated.Role,
                Username = userCreated.UserName,
                Id = userCreated.Id                
                
            };

        }

        public IEnumerable<UserDTO> Get()
        {
            var users = _userRepository.Get();

            return users.Select(u => 
            {
                return new UserDTO
                {
                    Role = u.Role,
                    Username = u.UserName,
                    Id = u.Id 
                };
            });
        }

        public UserDTO Get(Guid id)
        {
            var user = _userRepository.Get(id);

            return new UserDTO
            {
                Role = user.Role,
                Username = user.UserName,
                Id = user.Id 
            };
        }

        public LoginResultDTO Login(string username, string password)
        {
            var loginResult = _userRepository.Login(username, password);

            if (loginResult.Error)
            {
                return new LoginResultDTO
                {
                    Success = false,
                    Errors = new string[] { $"Ocorreu um erro ao autenticar: {loginResult.Exception?.Message}" }
                };
            }
                        
            var token = _tokenService.GenerateToken(loginResult.User);

            return new LoginResultDTO
            {
               Success = true,
               User = new UserLoginResultDTO
               {
                   Id = loginResult.User.Id,
                   Role = loginResult.User.Role,
                   Token = token,
                   Username = loginResult.User.UserName

               }
            };

        }

    }
}
