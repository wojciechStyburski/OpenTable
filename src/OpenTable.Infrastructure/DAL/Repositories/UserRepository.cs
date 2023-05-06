namespace OpenTable.Infrastructure.DAL.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(OpenTableDbContext dbContext)
    {
        _users = dbContext.Users;
    }
    
    public async Task<User> GetByIdAsync(UserId id) 
        => await _users.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<User> GetByEmailAsync(Email email)
        => await _users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task<User> GetByUserNameAsync(UserName userName)
        => await _users.SingleOrDefaultAsync(x => x.UserName == userName);

    public async Task AddAsync(User user)
        => await _users.AddAsync(user);
}