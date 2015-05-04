using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMVC.Classes;
using System.IO;

namespace DemoMVC.Respiratory
{
    public class DrugCardsRepository : IDrugCardsRepository<Drug>
    {
        public ICacheProvider Cache { get; set; }
        public IReadDrugData<Drug> ReadDrugData { get; set; }

        string defaultDrugFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["drugsFilePath"]);

        public DrugCardsRepository()
        {
            this.Cache = new DefaultCacheFileDependencyProvider();
            this.ReadDrugData = new ReadDrugsDataFromXML(defaultDrugFilePath);
        }
        public DrugCardsRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
            this.ReadDrugData = new ReadDrugsDataFromXML(defaultDrugFilePath);
        }

        public DrugCardsRepository(IReadDrugData<Drug> readDrugData)
        {
            this.Cache = new DefaultCacheFileDependencyProvider();
            this.ReadDrugData = ReadDrugData;
        }

        public DrugCardsRepository(ICacheProvider cacheProvider, IReadDrugData<Drug> readDrugData)
        {
            this.Cache = cacheProvider;
            this.ReadDrugData = ReadDrugData;
        }
        public void ClearCache()
        {
            this.Cache.Invalidate("drugListCache");
        }

        public List<Drug> GetDrugGeneric(string drugGeneric)
        {
            // First, check the cache
            List<Drug> drugList = Cache.Get("drugListCache") as List<Drug>;

            // If it's not in the cache, we need to read it from the repository
            if (drugList == null)
            {
                // Get the repository data
                drugList = ReadDrugData.GetDataDrugs();
            }

            return drugList.FindAll(d=>(d.DrugGeneric.ToLower().Trim() == drugGeneric.ToLower().Trim()));
        }

        public List<Drug> GetDrugBrand(string drugBrand)
        {
            // First, check the cache
            List<Drug> drugList = Cache.Get("drugListCache") as List<Drug>;

            // If it's not in the cache, we need to read it from the repository
            if (drugList == null)
            {
                // Get the repository data
                drugList = ReadDrugData.GetDataDrugs();
            }

            return drugList.FindAll(d => (d.DrugBrand.ToLower().Trim() == drugBrand.ToLower().Trim()));
        }

        public List<Drug> GetDrugType(string drugType)
        {
            // First, check the cache
            List<Drug> drugList = Cache.Get("drugListCache") as List<Drug>;

            // If it's not in the cache, we need to read it from the repository
            if (drugList == null)
            {
                // Get the repository data
                drugList = ReadDrugData.GetDataDrugs();
            }

            return drugList.FindAll(d => d.DrugType.ToLower().Trim() == drugType.ToLower().Trim());
        }

        public IEnumerable<string> GetAllPossibleTypes()
        {
            // First, check the cache
            List<Drug> drugList = Cache.Get("drugListCache") as List<Drug>;

            // If it's not in the cache, we need to read it from the repository
            if (drugList == null)
            {
                // Get the repository data
                drugList = ReadDrugData.GetDataDrugs();
            }

            return drugList.Select(x => x.DrugType).Distinct();
        }

        public List<Drug> GetAllDrugs()
        {
            // First, check the cache
            List<Drug> drugList = Cache.Get("drugListCache") as List<Drug>;
 
            // If it's not in the cache, we need to read it from the repository
            if (drugList == null)
            {
                // Get the repository data
                drugList = ReadDrugData.GetDataDrugs();
 
                if (drugList.Any())
                {
                    // Put this data into the cache
                    if(ReadDrugData is ReadDrugsDataFromXML)
                    {
                        Cache.Set("drugListCache", drugList,((ReadDrugsDataFromXML)ReadDrugData).drugFilePath);
                    }
                    else
                    {
                        //24hr
                        Cache.Set("drugListCache", drugList, 1440);
                    }
                }
            }
 
            return drugList;
        }
    }
}