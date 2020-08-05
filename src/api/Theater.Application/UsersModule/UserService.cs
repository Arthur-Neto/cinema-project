using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Theater.Application.UsersModule.Commands;
using Theater.Application.UsersModule.Models;
using Theater.Domain.UsersModule;
using Theater.Infra.Crosscutting.Exceptions;
using Theater.Infra.Crosscutting.Guards;

namespace Theater.Application.UsersModule
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> RetrieveAllAsync();
        Task<UserModel> RetrieveByIDAsync(int id);
        Task<int> CreateAsync(UserCreateCommand command);
        Task<bool> DeleteAsync(UserDeleteCommand command);
        Task<bool> Update(UserUpdateCommand command);

        Task<string> AuthenticateAsync(UserAuthenticateCommand command);
    }

    public class UserService : AppServiceBase<IUserRepository>, IUserService
    {
        private readonly IConfiguration _appSettings;

        public UserService(
            IConfiguration appSettings,
            IUserRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        {
            _appSettings = appSettings;
        }

        public async Task<int> CreateAsync(UserCreateCommand command)
        {
            var user = _mapper.Map<User>(command);
            var createdUser = await _repository.CreateAsync(user);

            return await CommitAsync() > 0 ? createdUser.ID : 0;
        }

        public async Task<bool> DeleteAsync(UserDeleteCommand command)
        {
            var user = _mapper.Map<User>(command);
            await _repository.DeleteAsync(user.ID);

            return await CommitAsync() > 0;
        }

        public async Task<bool> Update(UserUpdateCommand command)
        {
            var user = _mapper.Map<User>(command);
            _repository.Update(user);

            return await CommitAsync() > 0;
        }

        public async Task<IEnumerable<UserModel>> RetrieveAllAsync()
        {
            var users = await _repository.RetrieveAllAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> RetrieveByIDAsync(int id)
        {
            var user = await _repository.RetrieveByIDAsync(id);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<string> AuthenticateAsync(UserAuthenticateCommand command)
        {
            var user = await _repository.SingleOrDefaultAsync(x => x.Username.Equals(command.Username));
            Guard.Against(user, ErrorType.UserNotFound);

            var isCorrectPassword = user.Password.Equals(command.Password);
            Guard.Against(!isCorrectPassword, ErrorType.IncorrectUserPassword);

            var tokenExpiration = _appSettings.GetValue<string>("TokenExpiration");
            var secret = _appSettings.GetValue<string>("Secret");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            await CommitAsync();

            return user.Token;
        }
    }
}
