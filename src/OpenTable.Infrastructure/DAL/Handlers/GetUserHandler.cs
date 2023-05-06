namespace OpenTable.Infrastructure.DAL.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly DbSet<User> _users;

    public GetUserHandler(OpenTableDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var user = await _users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == new UserId(query.UserId));

        return user?.AsDto();
    }
}