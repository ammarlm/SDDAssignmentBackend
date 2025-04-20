using SDDAssignmentBackend.Context;
using SDDAssignmentBackend.DTO;
using SDDAssignmentBackend.Entities;
using SDDAssignmentBackend.Helpers;
using SDDAssignmentBackend.Repositories.Interface;
using SDDAssignmentBackend.Services.Interface;

namespace SDDAssignmentBackend.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserEntity> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
        public async Task<UserEntity> GetUser(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new Exception("Username or password did not match.");
            }

            var passwordHash = PasswordHasher.ComputeHash(password, user.Salt, 1);
            if (user.PasswordHash != passwordHash)
                throw new Exception("Username or password did not match.");

            return user;
        }
        public async Task<UserEntity> CreateUser(CreateUserDTO createUserDTO)
        {
            //check if user already exists
            var oldUser =  await _userRepository.GetByUsernameAsync(createUserDTO.Username);
            if (oldUser != null)
            {
                throw new Exception("Username already exists");
            }
            //create user
            var salt = PasswordHasher.GenerateSalt();
            var user = new UserEntity()
            {
                Username = createUserDTO.Username,
                PasswordHash = PasswordHasher.ComputeHash(createUserDTO.Password, salt, 1),
                Salt = salt,
                Role = createUserDTO.Role.GetDescription(),
                CreatedAt = DateTime.Now
            };

            //save user
            user = _userRepository.Add(user);

            try
            {
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating user", ex);
            }
            return user;
        }
    }
}
