using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVC.Classes
{
    public interface IReadDrugData<T>
    {
        List<T> GetDataDrugs();
        
    }
}