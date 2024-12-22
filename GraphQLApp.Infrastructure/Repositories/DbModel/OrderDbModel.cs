using Dapper.Contrib.Extensions;
using GraphQLApp.Domain.AggregatesModel.OrderAggregate;

namespace GraphQLApp.Infrastructure.Repositories.DbModel;

[System.ComponentModel.DataAnnotations.Schema.Table("graphqlapp.order")]
public class OrderDbModel
{
    public int Id { get;  set; }
    public string Address { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string Description { get; set; }
    public string OrderItems { get; set; }

    public Order ToOrder()
    {
        var orderItemIds = OrderItems.Split(',').Select(int.Parse).ToList();
        return new Order(Id, Address, OrderStatus, Description, orderItemIds);
    }
}