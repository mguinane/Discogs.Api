using Discogs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Data
{
    public interface IDiscogsRepository
    {
        Collection GetCollection();
        Wantlist GetWantlist();
    }
}
