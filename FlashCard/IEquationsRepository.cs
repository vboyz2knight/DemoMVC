using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVC.Classes
{
    public interface IEquationsRepository<T>
    {
        List<T> GetDrugType(string drugType);
        List<T> GetDrugGeneric(string drugGeneric);
        List<Drug> GetDrugBrand(string drugBrand);
        IEnumerable<string> GetAllPossibleTypes();
        List<T> GetAllDrugs();
    
    }
}