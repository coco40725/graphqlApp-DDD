using Dapper;
using GraphQLApp.Domain.AggregatesModel.OrderAggregate;
using GraphQLApp.Infrastructure.Repositories.DbModel;
using MySqlConnector;

namespace GraphQLApp.Infrastructure.Repositories;

public class OrderRepo : IOrderRepo
{
    
  // public Order Add(Order order)
  //   {
  //       throw new NotImplementedException();
  //   }
  //
  //   public void Update(Order order)
  //   {
  //       throw new NotImplementedException();
  //   }

    public async Task<OrderDbModel> GetAsync(int orderId)
    {
        var options = new SingleDatabaseOptions
        {
            Host = "localhost",
            Username = "root",
            Password = "root",
            Port = 3306
        };
       
        using ( var conn = new MySqlConnection(BuildConnectionString(options, "graphqlapp")))
        {
            var results = await conn.QueryAsync<OrderDbModel>(
                $"SELECT * FROM graphqlapp.order WHERE Id = @OrderId",
                new { OrderId = orderId }
            );
            return results.First();
        }
    }

    public async Task<IEnumerable<OrderDbModel>> GetAllAsync()
    {
        var options = new SingleDatabaseOptions
        {
            Host = "localhost",
            Username = "root",
            Password = "root",
            Port = 3306
        };
        
        using ( var conn = new MySqlConnection(BuildConnectionString(options, "graphqlapp")))
        {
            return await conn.QueryAsync<OrderDbModel>(
                $"SELECT * FROM graphqlapp.order"
            );
        }
    }

    private static string BuildConnectionString(SingleDatabaseOptions options, string database)
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Server = options.Host,
            UserID = options.Username,
            Password = options.Password,
            Port = options.Port,
            // If not specifying a database, dapper will throw exception when closing connection.
            Database = database,
            TreatTinyAsBoolean = false,
            GuidFormat = MySqlGuidFormat.None,
            // The default is 30s, which is not sufficient for querying some reports.
            DefaultCommandTimeout = 300,
            SslMode = MySqlSslMode.None
        };
        return builder.ToString();
    }
    
    public class SingleDatabaseOptions
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public uint Port { get; set; }
    }
 
}