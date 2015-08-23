using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVC.Classes
{
    public interface ICacheProvider
    {
        object Get(string key);
        void Set(string key, object data, int cacheTime);
        void Set(string key, object data, string filePath);
        bool IsSet(string key);
        void Invalidate(string key);
    }
}