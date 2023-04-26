public interface IUserRepository
{
    public Task<Guid> Create(User user);
    public Task<User> GetByUserEmail(String userEmail);
    public Task<IEnumerable<User>> GetAll();
    public Task<User> GetByUserGuid(Guid userGuid);
    public Task<User> GetByCredentials(String userEmail, String userPassword);
}