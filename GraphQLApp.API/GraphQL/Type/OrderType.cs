using GraphQL.Types;
using GraphQLApp.Domain.AggregatesModel.OrderAggregate;
using GraphQLApp.Infrastructure.Repositories;

namespace GraphQLApp.API.GraphQL.Type;

public class OrderType : ObjectGraphType<Order>
{
    public OrderType(
        IOrderItemRepo orderItemRepo)
    {
        Name = "Order";
        Description = "這是一個 Order";
        
        Field(x => x.Id).Description("The id of the order.");
        
        Field(x => x.OrderStatus).Description("The status of the order.");

        Field(x => x.Address).Description("The address of the order.");
        
        Field("myDesc", x => x.Description).Description("The description of the order.");

        Field<ListGraphType<OrderItemType>, IEnumerable<OrderItem>>("orderItems")
            .ResolveAsync(context =>
            {
                var itemIds = context.Source.OrderItemIds;
                return orderItemRepo
                    .GetAsync(itemIds.ToList())
                    .ContinueWith(r => r.Result.Select(e => e.ToOrderItem()))!;
            });

    }
    
}