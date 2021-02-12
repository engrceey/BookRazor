using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.ModelView
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IEnumerable<Book> Books {get; set;}

        public async Task Onget()
        {
            Books = await _dbContext.Books.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Standard");
            return RedirectToPage("Index");
        }
    }
}