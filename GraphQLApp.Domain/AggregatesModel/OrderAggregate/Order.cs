using GraphQLApp.Domain.AggregatesModel.SeedWork;

namespace GraphQLApp.Domain.AggregatesModel.OrderAggregate;

// This class is the Order aggregate root
public class Order(int id, string address, OrderStatus orderStatus, string description, List<int> orderItemIds)
    : Entity, IAggregateRoot
{
    public override int Id { get;  protected set; } = id;
    public string Address { get; private set; } = address;
    public OrderStatus OrderStatus { get; private set; } = orderStatus;
    public string Description { get; private set; } = description;
    private readonly List<int> _orderItemIds = orderItemIds;
    public IReadOnlyCollection<int> OrderItemIds => _orderItemIds;


    public void AddOrderItem(int id)
    {
        //...
        // Domain rules/logic for adding the OrderItem to the order
        // ...
        _orderItemIds.Add(id);

    }
   
}