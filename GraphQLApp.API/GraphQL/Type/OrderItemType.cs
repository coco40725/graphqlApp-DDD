using GraphQL.Types;
using GraphQLApp.Domain.AggregatesModel.OrderAggregate;

namespace GraphQLApp.API.GraphQL.Type;

public class OrderItemType : ObjectGraphType<OrderItem>
{
   public OrderItemType()
   {
      Field(x => x.Id);
      Field(x => x.ProductName);
      Field(x => x.UnitPrice);
   }
}