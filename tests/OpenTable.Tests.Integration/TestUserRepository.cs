﻿namespace OpenTable.Tests.Integration;

internal class TestUserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    public Task<User> GetByIdAsync(UserId id) 
        => Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

    public Task<User> GetByEmailAsync(Email email)
    => Task.FromResult(_users.SingleOrDefault(x => x.Email == email));

    public Task<User> GetByUserNameAsync(UserName userName)
        => Task.FromResult(_users.SingleOrDefault(x => x.UserName == userName));

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }
}