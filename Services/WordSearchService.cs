using HtmlAgilityPack;
using System.Diagnostics;
using System.Text;
using WordSearchWebApplication.Helpers;
using WordSearchWebApplication.Models;
using WordSearchWebApplication.Repositories;

namespace WordSearchWebApplication.Services
{
    public class WordSearchService : IWordSearchService
    {
        private readonly IWordSearchRepository m_WordSearchRepository;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="wordSearchRepository">Reference to the repository</param>
        public WordSearchService(IWordSearchRepository wordSearchRepository)
        {
            this.m_WordSearchRepository = wordSearchRepository;
        }


        /// <summary>
        /// Async method that return all results
        /// </summary>
        /// <returns>List with results</returns>
        public async Task<IEnumerable<WordSearchResult>> GetResultsAsync()
        {
            return await m_WordSearchRepository.GetResultsAsync();
        }


        /// <summary>
        /// Async method that add a list of wordsearch results
        /// </summary>
        /// <param name="lsWordSearchResult">List of wordsearch results</param>
        /// <returns>Number of saved results</returns>
        /// <exception cref="ArgumentNullException">Will be throw if WordSearchResult List is null or empty</exception>
        public async Task<int> AddWordSearchResultsAsync(IList<WordSearchResult> lsWordSearchResult)
        {
            return await m_WordSearchRepository.AddWordSearchResultsAsync(lsWordSearchResult);
        }


        /// <summary>
        /// Async method that  add a wordsearch result
        /// </summary>
        /// <param name="wordSearchResult">Reference to a wordsearch result</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Will be throw if WordSearchResult is null</exception>
        public async Task<int> AddWordSearchResultAsync(WordSearchResult wordSearchResult)
        {
            return await m_WordSearchRepository.AddWordSearchResultAsync(wordSearchResult);
        }


        /// <summary>
        /// Method calculate how many time words exist in a webpage. We dont care about the case of a character.
        /// This means Hello and hello is the same
        /// </summary>
        /// <param name="lsWords">List of words that we are searching for in the webpage</param>
        /// <param name="strUrl">Url to a webpage</param>
        /// <returns>List of words and how mmany times we found them in a webpage. Can also return null</returns>
        /// <exception cref="Exception">Trows exception if shit happens</exception>
        public IList<WordSearchResult>? WordSearch(IList<string>? lsWords, string strUrl)
        {
            if (lsWords == null || lsWords.Count == 0)
                return null;

            IList<WordSearchResult>? lsWordSearchResults = null;
            StringBuilder strBuilder = new StringBuilder();

            try
            {
                // Use Html Agility pack to get the text data from the webpage
                HtmlWeb web = new HtmlWeb();
                var totalTimeStopwWatch = Stopwatch.StartNew();
                var loadTimeStopWatch = Stopwatch.StartNew();
                var htmlDoc = web.Load(strUrl);
                loadTimeStopWatch.Stop();

                // Get the text from the webpage
                foreach (var childNode in htmlDoc.DocumentNode.ChildNodes)
                    strBuilder.Append($"{childNode.InnerText.Trim()} ");

                // Convert text to a list of words
                var listOfWords = WordSearchHelper.GetSearchWords(strBuilder.ToString());
                WordSearchResult? wordSearchResult = null;

                if (listOfWords != null && listOfWords.Count > 0)
                {
                    // Now we calculate how many times a word is found in the webpage
                    lsWordSearchResults = new List<WordSearchResult>(lsWords.Count);
                    foreach (var word in lsWords)
                    {
                        wordSearchResult = new WordSearchResult();
                        wordSearchResult.Info = "No filtering";
                        wordSearchResult.Url = strUrl;
                        wordSearchResult.Time = loadTimeStopWatch.ElapsedMilliseconds;
                        wordSearchResult.TotalTime = loadTimeStopWatch.ElapsedMilliseconds;
                        wordSearchResult.TimeForTest = DateTime.Now;
                        wordSearchResult.Keyword = word;
                        wordSearchResult.NumberOfHits = listOfWords.Where(w => w.Equals(word, StringComparison.OrdinalIgnoreCase)).Count();
                        lsWordSearchResults.Add(wordSearchResult);
                    }
                }

                totalTimeStopwWatch.Stop();

                if (lsWordSearchResults != null && lsWordSearchResults.Count > 0)
                {
                    foreach (var w in lsWordSearchResults)
                        w.TotalTime = totalTimeStopwWatch.ElapsedMilliseconds;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lsWordSearchResults;
        }
    }
}
