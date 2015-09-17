using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVC.Classes
{
    public interface IReadEquationData<T>
    {
        List<T> GetAllEquations();
    }
}