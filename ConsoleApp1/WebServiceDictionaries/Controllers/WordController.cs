
using System.Collections.Generic;
using System.Web.Mvc;
using WebServiceDictionaries.Models;

namespace WebServiceDictionaries.Controllers
{
    public class WordController : Controller
    {


        // GET: Word/GetA/5
        //[ResponseType(typeof(Word))]
        //[System.Web.Http.Route("GetApp/{language}")]
        [System.Web.Http.HttpGet]
        public JsonResult GetApp(string language)//, int? amoutOfWords)
        {
            List<WordTest> words;
            words = NHibernateHelper.GetRandomWordsFromDictionary(language);
            
            /* if (words == null)
{
    return BadRequest();
}*/
            //var result =  JsonConvert.SerializeObject(value: words);
            return Json(words, JsonRequestBehavior.AllowGet);
        }
    }
}