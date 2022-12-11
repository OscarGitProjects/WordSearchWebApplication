using WordSearchWebApplication.Models;

namespace WordSearchWebApplication.Repositories
{
    public interface IWordSearchRepository
    {
        /// <summary>
        /// Async method that add a list of wordsearch results
        /// </summary>
        /// <param name="lsWordSearchResult">List of wordsearch results</param>
        /// <returns>Number of saved results</returns>
        /// <exception cref="ArgumentNullException">Will be throw if WordSearchResult List is null or empty</exception>
        Task<int> AddWordSearchResultsAsync(IList<WordSearchResult> lsWordSearchResult);

        /// <summary>
        /// Async method that  add a wordsearch result
        /// </summary>
        /// <param name="wordSearchResult">Reference to a wordsearch result</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Will be throw if WordSearchResult is null</exception>
        Task<int> AddWordSearchResultAsync(WordSearchResult wordSearchResult);

        /// <summary>
        /// Async method that return all results
        /// </summary>
        /// <returns>List with results</returns>
        Task<IEnumerable<WordSearchResult>> GetResultsAsync();
    }
}