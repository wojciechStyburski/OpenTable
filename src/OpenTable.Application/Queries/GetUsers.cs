namespace OpenTable.Application.Queries;

public record GetUsers() : IQuery<IEnumerable<UserDto>>;