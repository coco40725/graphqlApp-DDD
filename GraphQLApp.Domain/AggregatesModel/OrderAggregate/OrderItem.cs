using GraphQLApp.Domain.AggregatesModel.SeedWork;
using GraphQLApp.Domain.Exceptions;

namespace GraphQLApp.Domain.AggregatesModel.OrderAggregate;

// Child Entity
public class OrderItem : Entity, IAggregateRoot
{
    public override int Id { get; protected set; }
    public readonly string  ProductName;
    public readonly decimal UnitPrice;
   
    public OrderItem(int productId, string productName, decimal unitPrice)
    {
        
        Id = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
    }
}