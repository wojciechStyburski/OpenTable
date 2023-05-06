namespace OpenTable.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUserNameAsync(UserName userName);
    Task AddAsync(User user);
}