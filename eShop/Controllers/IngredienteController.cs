using System.Linq;
using System.Threading.Tasks;
using eShop.Data;
using eShop.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers
{
    public class IngredienteController : Controller
    {

        private readonly AppDbContext _context;

        public IngredienteController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingrediente.ToListAsync());
        }

        // GET: Ingrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ingrediente == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingrediente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingrediente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Denumire")] Ingredient i)
        {
            if (ModelState.IsValid)
            {
                Ingredient ingredient = new()
                {
                    Denumire = i.Denumire

                };
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(i);
        }

        // GET: Ingrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ingrediente == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingrediente.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            DTO.Ingredient i = new()
            {
                Id = ingredient.Id,
                Denumire = ingredient.Denumire,
            };
            return View(i);
        }

        // POST: Ingrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Denumire")] DTO.Ingredient i)
        {
            if (_context.Ingrediente == null || id != i.Id)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingrediente.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ingredient.Denumire = i.Denumire;
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(i.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(i);
        }

        // GET: Ingrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ingrediente == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingrediente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Ingrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ingrediente == null)
            {
                return Problem("Entity set 'AppDbContext.Actors'  is null.");
            }
            var ingredient = await _context.Ingrediente.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingrediente.Remove(ingredient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingrediente.Any(i => i.Id == id);
        }
    }
}
