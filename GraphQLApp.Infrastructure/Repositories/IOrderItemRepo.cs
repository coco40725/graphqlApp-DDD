using GraphQLApp.Domain.AggregatesModel.OrderAggregate;
using GraphQLApp.Domain.AggregatesModel.SeedWork;
using GraphQLApp.Infrastructure.Repositories.DbModel;

namespace GraphQLApp.Infrastructure.Repositories;

public interface IOrderItemRepo : IRepository<OrderItem>
{
    Task<IEnumerable<OrderItemDbModel>> GetAsync(List<int> ids);
}