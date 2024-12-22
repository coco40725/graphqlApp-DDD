using GraphQL;
using GraphQL.Types;
using GraphQLApp.API.GraphQL.Type;
using GraphQLApp.Domain.AggregatesModel.OrderAggregate;
using GraphQLApp.Infrastructure.Repositories;

namespace GraphQLApp.API.GraphQL;

public class Query : ObjectGraphType
{

    public Query(
        IServiceProvider serviceProvider,
        IOrderRepo orderRepo,
        IOrderItemRepo orderItemRepo)
    {
        Name = "Query";
        Description = "This is the query entrypoint";

        // Field<Source Type, Return Type>
        // Source Type: 是指這個 field 的 type 是哪一個
        // Return Type: 是指這個 resolver 會回傳的資料類型
        // graphQL 要把 IEnumerable<Order> 轉換成 ListGraphType<OrderType> 再傳給前端看
        // graphql 怎麼知道怎麼轉換呢? 因為我們在 OrderType 裡面定義了 Order 與 OrderType 的對應關係
        Field<ListGraphType<OrderType>, IEnumerable<Order>>("allOrders")
            .Description("get all order")
            .ResolveAsync(_ =>
            {
                return orderRepo
                    .GetAllAsync()
                    .ContinueWith(r => r.Result.Select(e => e.ToOrder()))!;
            });
        
        
        Field<OrderType, Order>("orderById")
            .Description("get order by id")
            .Arguments(new QueryArgument<IntGraphType> { Name = "id" })
            .ResolveAsync(context =>
            {
                return orderRepo
                    .GetAsync(context.GetArgument<int>("id"))
                    .ContinueWith(r => r.Result.ToOrder())!;
            });
        
        
        Field<IntGraphType, int>("testtest")
            .Resolve(context => 1);
        
    }
}