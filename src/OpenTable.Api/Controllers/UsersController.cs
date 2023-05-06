namespace OpenTable.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _signUpHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
    private readonly ITokenStorage _tokenStorage;

    public UsersController
    (
        ICommandHandler<SignUp> signUpHandler, 
        ICommandHandler<SignIn> signInHandler,
        IQueryHandler<GetUser, UserDto> getUserHandler, 
        IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler ,
        ITokenStorage tokenStorage
    )
    {
        _signUpHandler = signUpHandler;
        _getUserHandler = getUserHandler;
        _getUsersHandler = getUsersHandler;
        _tokenStorage = tokenStorage;
        _signInHandler = signInHandler;
    }

    [HttpGet("{userId:guid}")]
    [Authorize(Policy = "is-admin")]
    [SwaggerOperation("Get single user by id if exists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUser(Guid userId)
    {
        var user = await _getUserHandler.HandleAsync( new GetUser(userId));
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    [Authorize(Policy = "is-admin")]
    [SwaggerOperation("Get users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
        => Ok(await _getUsersHandler.HandleAsync(query));
    
    [HttpGet("me")]
    [Authorize]
    [SwaggerOperation("Get account details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(HttpContext.User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(HttpContext.User.Identity.Name);
        var user = await _getUserHandler.HandleAsync( new GetUser(userId));
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [HttpPost("sign-up")]
    [SwaggerOperation("Sing up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> SignUp(SignUp command)
    {
        var userId = Guid.NewGuid();
        await _signUpHandler.HandleAsync(command with
        {
            UserId = userId
        });

        return CreatedAtAction(nameof(Get), new {command.UserId}, null);
    }
    
    [HttpPost("sign-in")]
    [SwaggerOperation("Sing in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<JwtDto>> SignIn(SignIn command)
    {
        await _signInHandler.HandleAsync(command);
        var jwt = _tokenStorage.Get();

        return Ok(jwt);
    }
}