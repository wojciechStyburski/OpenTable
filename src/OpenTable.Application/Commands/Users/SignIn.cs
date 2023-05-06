namespace OpenTable.Application.Commands.Users;

public record SignIn(string Email, string Password) : ICommand;