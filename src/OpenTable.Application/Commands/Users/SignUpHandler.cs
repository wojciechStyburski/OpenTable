namespace OpenTable.Application.Commands.Users;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;

    public SignUpHandler(IClock clock, IPasswordManager passwordManager, IUserRepository userRepository)
    {
        _clock = clock;
        _passwordManager = passwordManager;
        _userRepository = userRepository;
    }
    public async Task HandleAsync(SignUp command)
    {
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var userName = new UserName(command.UserName);
        var fullName = new FullName(command.FullName);
        var password = new Password(command.Password);
        var role = new Role(command.Role);

        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await _userRepository.GetByUserNameAsync(userName) is not null)
        {
            throw new UsernameAlreadyInUseException(userName);
        }
        
        var securedPassword = _passwordManager.Secure(command.Password);
        var user = User.Create(userId, email, userName, securedPassword, fullName,
            role, _clock.Current());

        await _userRepository.AddAsync(user);
    }
}