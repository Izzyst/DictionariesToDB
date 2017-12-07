
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
            List<Word> words;
            words = NHibernateHelper.GetRandomWordsFromDictionary(language);
            List<WordTest> wordsTest = new List<WordTest>();

            foreach (var item in words)
            {
                var w = new WordTest();
                w.Id = item.Id;
                w.W = item.W;
                w.Lang = item.Lang;
               // w.Defs.Add(item.Defs);

                wordsTest.Add(w);

            }
            /* if (words == null)
{
    return BadRequest();
}*/
            //var result =  JsonConvert.SerializeObject(value: words);
            return Json(wordsTest, JsonRequestBehavior.AllowGet);
        }
    }
}