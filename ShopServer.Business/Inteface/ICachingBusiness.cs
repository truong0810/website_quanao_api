using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface ICachingBusiness
    {
        string? Get(string key);
        List<T> Get<T>(string key);

        // insert or update
        void Upsert<T>(string key, T value);

        // insert or update
        //void Upsert<T>(string key, T value, TimeSpan expiration);

        void Delete(string key);
    }
}
