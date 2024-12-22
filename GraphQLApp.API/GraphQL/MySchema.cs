using GraphQL.Types;

namespace GraphQLApp.API.GraphQL;

public class MySchema : Schema
{
    public MySchema(IServiceProvider provider) : base(provider)
    {
        Query = provider.GetRequiredService<Query>();
    }
}