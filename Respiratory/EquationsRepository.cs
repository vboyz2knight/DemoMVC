using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMVC.Classes;

namespace DemoMVC.Respiratory
{
    public class EquationsRepository
    {
        public ICacheProvider Cache { get; set; }
        public IReadEquationData<Equation> ReadEquationData { get; set; }

        public EquationsRepository(ICacheProvider _cache, IReadEquationData<Equation> _readEquationData)
        {
            this.Cache = _cache;
            this.ReadEquationData = _readEquationData;
        }

        public List<Equation> GetAllEquations()
        {
            // First, check the cache
            List<Equation> equationList = Cache.Get("equationListCache") as List<Equation>;

            // If it's not in the cache, we need to read it from the repository
            if (equationList == null)
            {
                // Get the repository data
                equationList = ReadEquationData.GetAllEquations();

                if (equationList.Any())
                {
                    // Put this data into the cache
                    if (ReadEquationData is ReadEquationsFromXML)
                    {
                        Cache.Set("equationListCache", equationList, ((ReadEquationsFromXML)ReadEquationData).equationFilePath);
                    }
                    else
                    {
                        //24hr
                        Cache.Set("equationListCache", equationList, 1440);
                    }
                }
            }

            return equationList;
        }

        public Equation GetAnEquation(string abbreviation)
        {
            // First, check the cache
            List<Equation> equationList = GetAllEquations();
            
            return equationList.FirstOrDefault(x => x.Abbreviation.ToLower() == abbreviation.ToLower());
        }
    }
}