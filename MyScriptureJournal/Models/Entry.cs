using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    public class Entry
    {
        public int Id { get; set; }
        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        [Display(Name = "Scripture")]
        public string? ReferenceBook { get; set; }
        
        public string? Notes { get; set; }
    }
}
