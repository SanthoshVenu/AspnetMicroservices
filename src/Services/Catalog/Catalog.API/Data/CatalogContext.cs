using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            // Making a connection with MongoDB using the connectionstring from appsetting.json file a.k.a Configuration file (Like Web.config)
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            // Getting Database access
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:ProductDb"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

        }
        public IMongoCollection<Product> Products { get; }
    }
}
