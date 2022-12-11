namespace WordSearchWebApplication.Helpers;

/// <summary>
/// Class with help methods for the search word function
/// </summary>
public class WordSearchHelper
{
    /// <summary>
    /// static Method that split strWords to single words. Removes empty words and trim word
    /// Return result as a List
    /// </summary>
    /// <param name="strWords">string with search words</param>
    /// <returns>List with the search words or null</returns>
    public static IList<string>? GetSearchWords(string strWords)
    {
        if(strWords == null || strWords.Length <= 0)
            return null;    

        List<string>? lsWords = null;

        string[] arrWords = strWords.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if(arrWords != null && arrWords.Length > 0 ) 
        {
            lsWords = new List<string>(arrWords.Length);

            foreach (string word in arrWords) 
                lsWords.Add(word.Trim());
        }

        return lsWords;
    }
}