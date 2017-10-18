using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discogs.Api.Models;
using System.IO;
using Newtonsoft.Json;

namespace Discogs.Api.Data
{
    public class DiscogsRepository : IDiscogsRepository
    {
        private Collection _collection;
        private Wantlist _wantlist;

        public DiscogsRepository()
        {
            _collection = JsonConvert.DeserializeObject<Collection>(File.ReadAllText(@"Data\collection.json"));
            _wantlist = JsonConvert.DeserializeObject<Wantlist>(File.ReadAllText(@"Data\wantlist.json"));
        }

        public Collection GetCollection()
        {
            return _collection;
        }

        public Wantlist GetWantlist()
        {
            return _wantlist;
        }
    }
}
