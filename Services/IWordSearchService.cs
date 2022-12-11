using WordSearchWebApplication.Models;

namespace WordSearchWebApplication.Services
{
    public interface IWordSearchService
    {
        /// <summary>
        /// Async method that return all results
        /// </summary>
        /// <returns>List with results</returns>
        Task<IEnumerable<WordSearchResult>> GetResultsAsync();

        /// <summary>
        /// Async method that  add a wordsearch result
        /// </summary>
        /// <param name="wordSearchResult">Reference to a wordsearch result</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Will be throw if WordSearchResult is null</exception>
        Task<int> AddWordSearchResultAsync(WordSearchResult wordSearchResult);

        /// <summary>
        /// Async method that add a list of wordsearch results
        /// </summary>
        /// <param name="lsWordSearchResult">List of wordsearch results</param>
        /// <returns>Number of saved results</returns>
        /// <exception cref="ArgumentNullException">Will be throw if WordSearchResult List is null or empty</exception>
        Task<int> AddWordSearchResultsAsync(IList<WordSearchResult> lsWordSearchResult);        

        /// <summary>
        /// Method calculate how many time words exist in a webpage. We dont care about the case of a character.
        /// This means Hello and hello is the same
        /// </summary>
        /// <param name="lsWords">List of words that we are searching for in the webpage</param>
        /// <param name="strUrl">Url to a webpage</param>
        /// <returns>List of words and how mmany times we found them in a webpage. Can also return null</returns>
        /// <exception cref="Exception">Trows exception if shit happens</exception>
        IList<WordSearchResult>? WordSearch(IList<string>? lsWords, string strUrl);
    }
}