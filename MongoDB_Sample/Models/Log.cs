using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_Sample.Models
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserName { get; set; }
        public string APIName { get; set; } = null!;
        public string MethodName { get; set; } = null!;
        public int NoteID { get; set; }
        public string IPAddress { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public DateTime ResponseDate { get; set; }
        public double RT { get; set; }
    }
}
