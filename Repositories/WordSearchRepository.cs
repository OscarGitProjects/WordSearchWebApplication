using Microsoft.EntityFrameworkCore;
using WordSearchWebApplication.Models;

namespace WordSearchWebApplication.Repositories
{
    public class WordSearchRepository : IWordSearchRepository
    {
        private readonly WordSearchContext m_Context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Reference to context</param>
        public WordSearchRepository(WordSearchContext context)
        {
            this.m_Context = context;
        }


        /// <summary>
        /// Async method that return all results
        /// </summary>
        /// <returns>List with results</returns>
        public async Task<IEnumerable<WordSearchResult>> GetResultsAsync()
        {
            var results = await m_Context.WordSearchResults.ToListAsync();
            return results;
        }


        /// <summary>
        /// Async method that add a list of wordsearch results
        /// </summary>
        /// <param name="lsWordSearchResult">List of wordsearch results</param>
        /// <returns>Number of saved results</returns>
        /// <exception cref="ArgumentNullException">Will be throw if WordSearchResult List is null or empty</exception>
        public async Task<int> AddWordSearchResultsAsync(IList<WordSearchResult> lsWordSearchResult)
        {
            if (lsWordSearchResult == null || lsWordSearchResult.Count <= 0)
                throw new ArgumentNullException($"{nameof(WordSearchRepository)}->AddWordSearchResultsAsync(). List is null or empty");

            await m_Context.AddRangeAsync(lsWordSearchResult);

            int iNumberOfSaved = await m_Context.SaveChangesAsync();

            return iNumberOfSaved;
        }


        /// <summary>
        /// Async method that  add a wordsearch result
        /// </summary>
        /// <param name="wordSearchResult">Reference to a wordsearch result</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Will be throw if WordSearchResult is null</exception>
        public async Task<int> AddWordSearchResultAsync(WordSearchResult wordSearchResult)
        {
            if (wordSearchResult == null)
                throw new ArgumentNullException($"{nameof(WordSearchRepository)}->AddWordSearchResultAsync(). Reference to WordSearchResult is null");

            await m_Context.AddAsync(wordSearchResult);

            int iNumberOfSaved = await m_Context.SaveChangesAsync();

            return iNumberOfSaved;
        }
    }
}
