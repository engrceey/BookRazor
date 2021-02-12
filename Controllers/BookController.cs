using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            return Json(new {data = await _context.Books.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (bookFromDb == null)
            {
                return Json(new {success = false, message = "Error while Deleting"});
            }
            _context.Books.Remove(bookFromDb);
            await _context.SaveChangesAsync();
            return Json(new {success=true, message = "Delete successful"});
        }

    }
}