namespace OpenTable.Application.Commands.Users;

public record SignUp (Guid UserId, string Email, string UserName, string Password, string FullName, string Role) : ICommand;
