using GraphQLApp.Domain.AggregatesModel.OrderAggregate;
using GraphQLApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLApp.API.RestApi;


[Route("/rest/order")]
public class OrderController(IOrderRepo orderRepo) : Controller
{
    [HttpGet]
    public async Task<Order> OrderTest()
    {
        var dbResult = await orderRepo.GetAsync(1);
        return dbResult.ToOrder();
    }
}