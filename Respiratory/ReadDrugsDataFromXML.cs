using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMVC.Classes;
using System.IO;
using System.Xml.Linq;

namespace DemoMVC.Respiratory
{
    public class ReadDrugsDataFromXML : IReadDrugData<Drug>
    {
        public string drugFilePath { get; private set; }
        
        public ReadDrugsDataFromXML(string filePath)
        {
            this.drugFilePath = filePath;
            if (!File.Exists(drugFilePath))
            { throw new FileNotFoundException("{0}: file is not found.", drugFilePath); }
        }
        public List<Drug> GetDataDrugs()
        {
            List<Drug> drugList = new List<Drug>();
          
            XDocument xDrugDocument = XDocument.Load(drugFilePath);
            var query = from drug in xDrugDocument.Descendants("drug")
                        select new Drug
                        {
                            DrugBrand = drug.Element("Brand").Value,
                            DrugGeneric = drug.Element("Generic").Value,
                            DrugType = drug.Element("Type").Value,
                            Action = drug.Element("Action").Value,
                            Indication = drug.Element("Indication").Value,
                            Therapeutic = drug.Element("Therapeutic").Value
                        };

            foreach(var item in query)
            {
                drugList.Add(item);
            }

            return drugList;
        }
    }
}
