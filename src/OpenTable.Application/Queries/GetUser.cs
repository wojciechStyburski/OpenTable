namespace OpenTable.Application.Queries;

public record GetUser(Guid UserId) : IQuery<UserDto>;
