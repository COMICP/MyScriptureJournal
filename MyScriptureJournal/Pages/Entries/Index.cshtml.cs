using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Entries
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Entry> Entry { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? EntryReference { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchDate { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<string> bookQuery = from m in _context.Entry
                                            orderby m.ReferenceBook
                                            select m.ReferenceBook;

            var Entries = from m in _context.Entry
                          select m;
            
            if (!string.IsNullOrEmpty(SearchString))
            {
                Entries = Entries.Where(s => s.Notes.Contains(SearchString));
            }

            if (SearchDate.HasValue)
            {
                Entries = Entries.Where(s => s.DateTime == SearchDate);
            }
            if (!string.IsNullOrEmpty(EntryReference))
            {
                Entries = Entries.Where(x => x.ReferenceBook == EntryReference);
            }
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            Entry = await Entries.ToListAsync();
        }
    }
}
