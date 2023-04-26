public interface IPaymentRepository{
    public Task<Guid> Create(Payment payment);
}