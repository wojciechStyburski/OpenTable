namespace OpenTable.Tests.Integration.Controllers;

public class UsersControllerTests : ControllerTests, IDisposable
{
    [Fact]
    public async Task given_valid_post_user_should_return_created_201_status_code()
    {
        var command = new SignUp
        (
            Guid.Empty, "test-user1@gmail.com", "test-user1", 
            "secret", "Joe Doe", "user"
        );

        var response = await Client.PostAsJsonAsync("users/sign-up", command);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task given_valid_post_sign_in_should_return_ok_200_status_code_and_jwt()
    {
        var password = "secret";
        var user = await CreateUserAsync(password); 
        
        var command = new SignIn(user.Email, password);
        var response = await Client.PostAsJsonAsync("users/sign-in", command);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();
        
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task get_users_me_should_return_ok_200_status_code_and_user()
    {
        var password = "secret";
        var user = await CreateUserAsync(password);

        Authorize(user.Id, user.Role);

        var userDto = await Client.GetFromJsonAsync<UserDto>("users/me");
        userDto.ShouldNotBeNull();
        userDto.Id.ShouldBe(user.Id.Value);
    }

    private async Task<User> CreateUserAsync(string password)
    {
        var clock = new Clock();
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var user = User.Create
        (
            Guid.NewGuid(), "test-user2@gmail.com", "test-user2", 
            passwordManager.Secure(password), "Joe Doe", "user", clock.Current()
        );
        
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        return user;
    }
    
    private readonly TestDatabase _testDatabase;
    // private IUserRepository _userRepository;
    
    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase?.Dispose();
    }

    /*
    protected override void ConfigureService(IServiceCollection services)
    {
        _userRepository = new TestUserRepository();
        services.AddSingleton(_userRepository);
    }
    */
}