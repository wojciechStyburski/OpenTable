namespace OpenTable.Infrastructure.DAL.Handlers;

internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly DbSet<User> _users;
    
    public GetUsersHandler(OpenTableDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        => await _users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}