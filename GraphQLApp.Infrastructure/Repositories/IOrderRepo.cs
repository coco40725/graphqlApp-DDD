using GraphQLApp.Domain.AggregatesModel.OrderAggregate;
using GraphQLApp.Domain.AggregatesModel.SeedWork;
using GraphQLApp.Infrastructure.Repositories.DbModel;

namespace GraphQLApp.Infrastructure.Repositories;

public interface IOrderRepo : IRepository<Order>
{
    // Order Add(Order order);
    //     
    // void Update(Order order);

    Task<OrderDbModel> GetAsync(int orderId);
    Task<IEnumerable<OrderDbModel>> GetAllAsync();
}