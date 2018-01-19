
using ConsoleApp1;
using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebServiceDictionaries.Models;

namespace WebServiceDictionaries.Controllers
{
    public class WordController : Controller
    {

        //   /Word/GetWords?language=ang
        [System.Web.Http.HttpGet]
        public JsonResult GetWords(string language)//, int? amoutOfWords)
        { 
            List<WordTest> words;
            words = NHibernateHelper.GetRandomWordsFromDictionary(language);
            
            return Json(words, JsonRequestBehavior.AllowGet);
        }

        //   /Word/InsertWords?language=Polish
        [System.Web.Http.HttpGet]
        public int InsertWords(string language)//, int? amoutOfWords)
        {
            List<Words> list = new List<Words>();          
            try
            {

                list.AddRange(collection: Program.GetWordsFromFactory(language));
                NHibernateHelper.InsertWordToDatabase(list);

            }catch(Exception e)
            {
                return 0;
            }

            return 1;
        }
    }
}