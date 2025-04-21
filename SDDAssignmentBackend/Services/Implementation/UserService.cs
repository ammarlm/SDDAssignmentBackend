using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using SDDAssignmentBackend.Context;
using SDDAssignmentBackend.DTO;
using SDDAssignmentBackend.Entities;
using SDDAssignmentBackend.Helpers;
using SDDAssignmentBackend.Repositories.Interface;
using SDDAssignmentBackend.Services.Interface;
using Newtonsoft.Json;

namespace SDDAssignmentBackend.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuditRepository _auditRepository;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<UserService> logger, IHttpContextAccessor httpContextAccessor, IAuditRepository auditRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _auditRepository = auditRepository;
        }

        public async Task<PaginationResponse<UserEntity>> GetUsers(int page, int pageSize, string orderBy, string orderType, string search)
        {
            return await _userRepository.GetUsersAsync(page, pageSize, orderBy, orderType, search);
        }

        public async Task<UserEntity> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new LogicException("User not found");
            }
            return user;
        }
        public async Task<UserEntity> GetUser(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new LogicException("Username or password did not match.");
            }

            _logger.LogInformation("User found: {user}", user.Username);
            var passwordHash = PasswordHasher.ComputeHash(password, user.Salt, 1);
            if (user.PasswordHash != passwordHash)
                throw new LogicException("Username or password did not match.");

            return user;
        }
        public async Task<UserEntity> CreateUser(CreateUserDTO createUserDTO)
        {
            //check if user already exists
            var oldUser = await _userRepository.GetByUsernameAsync(createUserDTO.Username);
            if (oldUser != null)
            {
                throw new LogicException("Username already exists");
            }
            _logger.LogInformation("Creating user: {user}", createUserDTO.Username);
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
            AddAudit(user, "Create");
            try
            {
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating user: {user}", ex.Message);
                throw new Exception("Error while creating user", ex);
            }
            return user;
        }

        public async Task<UserEntity> UpdateUser(Guid id, UpdateUserDTO updateUserDTO)
        {
            if (updateUserDTO.Id != id)
            {
                throw new LogicException("Parameter not correct");
            }

            var user = await GetUser(id);

            //check if user already exists
            var oldUser = await _userRepository.GetByUsernameAsync(updateUserDTO.Username, user.Id);
            if (oldUser != null)
            {
                throw new LogicException("Username already exists");
            }
            _logger.LogInformation("Updating user: {user}", user.Username);

            //update user1
            if (user.Username != updateUserDTO.Username)
                user.Username = updateUserDTO.Username;

            user.Role = updateUserDTO.Role.GetDescription();

            _userRepository.Update(user);
            AddAudit(user, "Update", updateUserDTO);

            try
            {
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while updating user: {user}", ex.Message);
                throw new Exception("Error while updating user", ex);
            }
            return user;
        }
        // more logical to not update the password while update the user
        // better to updated password in a separate method
        public Task<UserEntity> ChangeUserPassword(Guid id, ChangePasswordDTO changePasswordDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await GetUser(id);

            _userRepository.Delete(user.Id);
            AddAudit(user, "Delete");
            try
            {
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting user: {user}", ex.Message);
                throw new Exception("Error while deleting user", ex);
            }
        }
        private void AddAudit(UserEntity user, string operation, UpdateUserDTO newData = null)
        {
            var username = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "System";
            var audit = new AuditEntity()
            {
                Operation = operation,
                TableName = nameof(UserEntity),
                CreatedAt = DateTime.Now,
                CreatedBy = username,
                RecordId = user.Id,

            };
            if (operation == "Update")
            {
                audit.OldData = JsonConvert.SerializeObject(user);
                if (newData != null)
                    audit.NewData = JsonConvert.SerializeObject(newData);
            }
            else if (operation == "Create")
            {
                audit.NewData = JsonConvert.SerializeObject(user);
            }
            _auditRepository.Add(audit);
        }
    }
}
