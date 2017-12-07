
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
    }
}