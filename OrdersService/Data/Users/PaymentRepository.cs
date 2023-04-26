using Dapper;
using System.Data;
public class PaymentRepository: IPaymentRepository{
    private readonly DapperContext _context;
    public PaymentRepository(DapperContext context){
        _context = context;
    }
    public async Task<Guid> Create(Payment payment){
    string sql = "INSERT INTO Payment(CardGuid, UserGuid, StreetAddress, CardNumber, ExpirationDate, CVV) VALUES (@CardGuid, @UserGuid, @StreetAddress, @CardNumber, @ExpirationDate, @CVV)";
    var parameterPayment = new DynamicParameters();
    Guid cardGuid = Guid.NewGuid();
    parameterPayment.Add("CardGuid", cardGuid, DbType.Guid);
    parameterPayment.Add("UserGuid", payment.UserGuid, DbType.Guid);
    parameterPayment.Add("StreetAddress",payment.StreetAddress, DbType.String);
    parameterPayment.Add("CardNumber", payment.CardNumber,DbType.String);
    parameterPayment.Add("ExpirationDate",payment.ExpirationDate, DbType.Date);
    parameterPayment.Add("CVV",payment.CVV,DbType.Int16);
    using( var connection = _context.CreateConnection()){
        await connection.ExecuteAsync(sql, parameterPayment);
    }
    return cardGuid;
    }
}
