public interface IUserRepository
{

    public Task<Guid> Create(User user);
    public Task<User> GetByUserEmail(string userEmail);

    public Task<IEnumerable<User>> GetAll();

    public Task<User> GetByUserGUID(Guid userGUID);

    public Task<User> GetByCredentials(string userEmail, string userPassword);
}