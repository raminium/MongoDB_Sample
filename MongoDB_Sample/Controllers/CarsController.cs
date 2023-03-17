using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoDB_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            //var dbList = dbClient.ListDatabases().ToList();

            //Console.WriteLine("The list of databases are:");

            //return dbList;
            return null;
        }

    }
}