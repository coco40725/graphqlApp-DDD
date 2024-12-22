using GraphQLApp.Domain.AggregatesModel.OrderAggregate;

namespace GraphQLApp.Infrastructure.Repositories.DbModel;

public class OrderItemDbModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    
    public OrderItem ToOrderItem()
    {
        return new OrderItem(Id, ProductName, UnitPrice);
    }
}