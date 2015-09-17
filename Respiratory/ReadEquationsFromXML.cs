using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMVC.Classes;
using System.IO;
using System.Xml.Linq;

namespace DemoMVC.Respiratory
{
    public class ReadEquationsFromXML:IReadEquationData<Equation>
    {
        public string equationFilePath { get; set; }

        public ReadEquationsFromXML()
        {
            this.equationFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["equationFilePath"]);
            if (!File.Exists(equationFilePath))
            { throw new FileNotFoundException("{0}: file is not found.", equationFilePath); }
        }

        public List<Equation> GetAllEquations()
        {
            List<Equation> equationList = new List<Equation>();

            XDocument xDrugDocument = XDocument.Load(equationFilePath);
            var query = from equation in xDrugDocument.Descendants("equation")
                        select new Equation
                        {
                            Abbreviation=equation.Element("abbreviation").Value,
                            Id=equation.Element("id").Value,
                            Description=equation.Element("description").Value,
                            EquationExpression = equation.Element("equationExpression").Value
                        };

            foreach (var item in query)
            {
                equationList.Add(item);
            }

            return equationList;
        }
    }
}