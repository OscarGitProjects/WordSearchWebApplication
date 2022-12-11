using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordSearchWebApplication.Models;

public class WordSearchResult
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayName("Url")]
    public string Url { get; set; }

    [Required]
    [DisplayName("Keyword")]
    public string Keyword{ get; set; }

    [Required]
    [DisplayName("Time to load webpage in ms")]
    [DataType(DataType.Duration)]
    public long Time { get; set; }

    [Required]
    [DisplayName("Time to load and parse webpage in ms")]
    [DataType(DataType.Duration)]
    public long TotalTime { get; set; }    

    [Required]
    [DisplayName("Time for test")]
    [DataType(DataType.DateTime)]
    public DateTime TimeForTest { get; set; }

    [Required]
    [DisplayName("Number of hits on keyword")]
    public int NumberOfHits { get; set; }

    [NotMapped]
    public string Info { get; set; }
}
