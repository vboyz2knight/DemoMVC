using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Classes;
using DemoMVC.Respiratory;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class FlashCardController : Controller
    {
        private IDrugCardsRepository<Drug> cardRepository { get; set; }

        public FlashCardController(IDrugCardsRepository<Drug> repository)
        {
            this.cardRepository = repository;
        }

        // GET: FlashCard
        public ActionResult Indexs()
        {
            List<Drug> myDrugList =  this.cardRepository.GetAllDrugs();
            //List<Drug> myDrugList = new DrugCardsRepository().GetAllDrugs();
            FlashDrugSetModel setModel = new FlashDrugSetModel();
            setModel.userDrugInfoArray = MyRandomizeStringArray.RandomizeStringsDurstenfeld( myDrugList.Select(x => x.DrugBrand).ToArray() );
            setModel.currentDisplayDrug = myDrugList.Where(x => x.DrugBrand.ToLower() == setModel.userDrugInfoArray[0].ToLower()).First();
            setModel.currentArrayIndex = 0;

            IEnumerable<string> myDrugTypes = new DrugCardsRepository().GetAllPossibleTypes();

            ViewBag.myDrugTypes = myDrugTypes;

            return View(setModel);
        }

        public JsonResult GetSearchDrugs(string term)
        {
            List<string> myDrugString = MySearchPossibleDrugs(term);

            return Json(myDrugString, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private static List<string> MySearchPossibleDrugs(string term)
        {
            List<Drug> myDrugList = new DrugCardsRepository().GetAllDrugs();

            List<Drug> myDrugListBrand = myDrugList.Where(item => item.DrugBrand.ToLower().Contains(term.ToLower())).ToList<Drug>();
            List<Drug> myDrugListGeneric = myDrugList.Where(item => item.DrugBrand.ToLower().Contains(term.ToLower())).ToList<Drug>();

            List<string> myDrugString = myDrugListBrand.Select(x => x.DrugBrand).ToList();
            myDrugString.AddRange(myDrugListGeneric.Select(x => x.DrugGeneric).ToList());
            return myDrugString;
        }

        public PartialViewResult DrugSearch(string term)
        {
            List<string> myDrugString = MySearchPossibleDrugs(term);
            SearchDrugsModel searchDrugsModel = new SearchDrugsModel();
            searchDrugsModel.searchDrugsResult = myDrugString;

            return PartialView("_SearchDrugsResult", searchDrugsModel);
        }

        [NonAction]
        private static List<string> MyTypePossibleDrugs(string searchTerm)
        {
            List<Drug> myDrugList = new DrugCardsRepository().GetAllDrugs();

            List<Drug> myDrugListBrand = myDrugList.Where(item => item.DrugType.ToLower().Contains(searchTerm.ToLower())).ToList<Drug>();
            List<string> myDrugString = myDrugListBrand.Select(x => x.DrugBrand).ToList();

            return myDrugString;
        }

        public PartialViewResult DrugTypeSearch(string drugTypeSearch)
        {
            List<string> myDrugString = MyTypePossibleDrugs(drugTypeSearch);
            SearchDrugsModel searchDrugsModel = new SearchDrugsModel();
            searchDrugsModel.searchDrugsResult = myDrugString;

            return PartialView("_SearchDrugsResult", searchDrugsModel);
        }

        //[ValidateAntiForgeryToken]
        //http://www.leniel.net/2013/09/detecting-fixing-ajax-beginform-partial-view-stale-data.html#sthash.agYs0tDH.dpbs
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult FlashMyCard(FlashDrugSetModel drugSetModel,string submit)
        {
            if (submit.ToLower() == "next")
            {
                if ((drugSetModel.userDrugInfoArray.Length > 1) && (drugSetModel.currentArrayIndex < drugSetModel.userDrugInfoArray.Length-1))
                {
                    drugSetModel.currentArrayIndex = drugSetModel.currentArrayIndex + 1;
                }
            }
            else if(submit.ToLower()=="previous")
            {
                if ((drugSetModel.userDrugInfoArray.Length > 0) && (drugSetModel.currentArrayIndex > 0))
                {
                    drugSetModel.currentArrayIndex = drugSetModel.currentArrayIndex - 1;
                }

            }

            List<Drug> myDrugSearch = new DrugCardsRepository().GetDrugBrand(drugSetModel.userDrugInfoArray[drugSetModel.currentArrayIndex]);

            if (myDrugSearch.Count > 0)
            {
                drugSetModel.currentDisplayDrug = myDrugSearch.First();
            }
            else
            {
                myDrugSearch = new DrugCardsRepository().GetDrugGeneric(drugSetModel.userDrugInfoArray[drugSetModel.currentArrayIndex]);
                drugSetModel.currentDisplayDrug = myDrugSearch.First();
            }

            ModelState.Clear();

            return PartialView("_FlashMyCard", drugSetModel);
        }

        public PartialViewResult FlashACard(string wantedDisplayDrug)
        {
            FlashDrugSetModel drugSetModel = new FlashDrugSetModel();

            List<Drug> myBrandSearch = new DrugCardsRepository().GetDrugBrand(wantedDisplayDrug);
            List<Drug> myGenericSearch = new DrugCardsRepository().GetDrugGeneric(wantedDisplayDrug);

            drugSetModel.currentArrayIndex = 0;
            List<string> resultInfo = new List<string>();

            if (myBrandSearch.Count > 0 )
            {                
                drugSetModel.currentDisplayDrug = myBrandSearch.First();
                
                resultInfo.AddRange(myBrandSearch.Select(x => x.DrugBrand).ToList());
                drugSetModel.userDrugInfoArray = resultInfo.ToArray();
            }
            else if(myGenericSearch.Count>0)
            {
                drugSetModel.currentDisplayDrug = myGenericSearch.First();

                resultInfo.AddRange(myGenericSearch.Select(x => x.DrugBrand).ToList());
                drugSetModel.userDrugInfoArray = resultInfo.ToArray();
            }

            return PartialView("_FlashMyCard", drugSetModel);
        }
    }
}