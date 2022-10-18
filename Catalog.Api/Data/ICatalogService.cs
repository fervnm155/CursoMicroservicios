using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public interface ICatalogService
    {
        IMongoCollection<Product> Products { get; set; }
    }
}
