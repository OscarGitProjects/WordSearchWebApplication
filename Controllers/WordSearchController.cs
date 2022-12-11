using Microsoft.AspNetCore.Mvc;
using WordSearchWebApplication.Helpers;
using WordSearchWebApplication.Services;

namespace WordSearchWebApplication.Controllers
{
    public class WordSearchController : Controller
    {
        private readonly IWordSearchService m_WordSearchService;

        /// <summary>
        /// Consructor
        /// </summary>
        /// <param name="wordSearchService">Reference to service</param>
        public WordSearchController(IWordSearchService wordSearchService)
        {
            m_WordSearchService = wordSearchService;
        }


        /// <summary>
        /// Action that search for words from a url
        /// </summary>
        /// <param name="txtSearchWords">words we want to search for</param>
        /// <param name="txtUrl">Url to webpage</param>
        /// <returns>ActionResult to View ListResult or Index</returns>
        public async Task<ActionResult> Search(string txtSearchWords, string txtUrl)
        {
            int iNumberOfError = 0;
            if (txtSearchWords == null || txtSearchWords.Length <= 0)
            {
                ViewBag.Error_txtSearchWords = "You must enter at least one word";
                iNumberOfError++;
            }

            if (txtUrl == null || txtUrl.Length <= 0)
            {
                ViewBag.Error_txtUrl = "You must enter a url";
                iNumberOfError++;
            }

            if (iNumberOfError > 0)
                return View("Index");

            try
            {
                var lsWordList = WordSearchHelper.GetSearchWords(txtSearchWords);

                // Search for the word in the webpage
                var lsResult = m_WordSearchService.WordSearch(lsWordList, txtUrl);// "http://127.0.0.1:5500/index.html");

                if (lsResult == null || lsResult.Count <= 0)
                {
                    ViewBag.Message = "We have no result";
                    ViewBag.MsgType = "MESSAGE";
                    return View("Index");
                }
                else
                {
                    // Save this search in the repository
                    await m_WordSearchService.AddWordSearchResultsAsync(lsResult);
                    return View("ListResult", lsResult);
                }

            }
            catch(Exception) 
            {
                ViewBag.Message = "Exception";
                ViewBag.MsgType = "ERROR";
            }

            return View("Index");
        }


        /// <summary>
        /// Action that list results from repository
        /// </summary>
        /// <returns>ActionResult to View ListResult</returns>
        public async Task<ActionResult> ListResult()
        {
            try
            {                
                var lsResult = await m_WordSearchService.GetResultsAsync();

                if (lsResult == null || lsResult.Count() <= 0)
                {
                    ViewBag.Message = "We have no result";
                    ViewBag.MsgType = "MESSAGE";
                }

                return View("ListResult", lsResult);
            }
            catch (Exception) 
            {
                ViewBag.Message = "Exception";
                ViewBag.MsgType = "ERROR";
            }

            return View("Index");
        }


        // GET: WordSearchController
        public ActionResult Index()
        {
            return View();
        }
    }
}
