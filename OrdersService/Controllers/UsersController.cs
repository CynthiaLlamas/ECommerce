using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserRepository _userRepository;

    public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {

        try
        {
            User uu = await _userRepository.GetByUserEmail(user.Email);
            if (uu != null)
            {
                return Ok(
                    new
                    {
                        Success = true,
                        Message = "User already exists!",
                        UserGuid = uu.UserGuid,
                        Email = uu.Email
                    }
                );
            }
            var userGuid = await _userRepository.Create(user);
            return Ok(
                new
                {
                    Success = true,
                    Message = "User Created.",
                    UserGuid = userGuid
                }
            );

        }
        catch (Exception ex)
        {
            return Ok("Something happened: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var Data = await _userRepository.GetAll();
            return Ok(
                new
                {
                    Success = true,
                    Message = "All users returned",
                    Data
                }
            );
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("{userGuid}")]
    public async Task<IActionResult> GetById(Guid userGuid)
    {
        try
        {
            var Data = await _userRepository.GetByUserGUID(userGuid);
            return Ok(
                new
                {
                    Success = true,
                    Message = "User Fetched.",
                    Data
                }
            );
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

}