using Dapper;
using System.Data;
public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;
    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(User user)
    {
        string sql = "INSERT INTO Users (UserGuid, Username, Email, Password, CreatedDate) VALUES (@UserGuid, @Username, @Email, @Password, @CreatedDate)";
        var parametersUsers = new DynamicParameters();
        Guid userGuid = Guid.NewGuid();

        parametersUsers.Add("UserGuid", userGuid, DbType.Guid);
        parametersUsers.Add("Username", user.Username, DbType.String);
        parametersUsers.Add("Email", user.Email, DbType.String);
        parametersUsers.Add("Password", user.Password, DbType.String);
        parametersUsers.Add("CreatedDate", DateTime.UtcNow, DbType.DateTime);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(sql, parametersUsers);
        }
        return userGuid;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        string sql = "SELECT * FROM Users";
        using (var connection = _context.CreateConnection())
        {
            var users = await connection.QueryAsync<User>(sql);
            return users.ToList();
        }
    }

    public async Task<User> GetByCredentials(string userEmail, string userPassword)
    {
        string sql = "SELECT * FROM Users WHERE Email = @EmailParam AND PASSOWRD = @PasswordParam";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { EmailParam = userEmail, PasswordParam = userPassword });
        }
    }

    public async Task<User> GetByUserEmail(string userEmail)
    {
        string sql = "SELECT * FROM Users Where Email = @EmailParam";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { EmailParam = userEmail });
        }
    }

    public async Task<User> GetByUserGUID(Guid userGuid)
    {
        string sql = "SELECT * FROM Users WHERE UserGuid = @UserGuidParam";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<User>(sql, new { UserGuidParam = userGuid });
        }
        throw new NotImplementedException();
    }

}