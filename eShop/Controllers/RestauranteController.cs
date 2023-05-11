using System.Linq;
using System.Threading.Tasks;
using eShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly AppDbContext _context;

        public RestauranteController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Restaurante.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restaurante == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurante.Include(r => r.Adresa).FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Poza,Descriere,Adresa")] DTO.Restaurant r)
        {
            if (ModelState.IsValid)
            {
                var restaurant = new Models.Restaurant()
                {
                   
                    Nume = r.Nume,
                    Poza = r.Poza,
                    Descriere = r.Descriere,
                    Adresa = new Models.Adresa()
                    {
                        Oras = r.Adresa.Oras,
                        Strada = r.Adresa.Strada
                    }
                };
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(r);
        }

        // GET: Restaurante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurante == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurante.Include(r => r.Adresa).FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            DTO.Restaurant r = new DTO.Restaurant()
            {
                Id = restaurant.Id,
                Nume = restaurant.Nume,
                Poza = restaurant.Poza,
                Descriere = restaurant.Descriere,
                Adresa = new DTO.Adresa()
                {
                    Id = restaurant.Adresa.Id,
                    Strada = restaurant.Adresa.Strada,
                    Oras = restaurant.Adresa.Oras,
                }
            };
            return View(r);
        }

        // POST: Restaurante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Poza,Descriere,Adresa")] DTO.Restaurant r)
        {
            if (_context.Restaurante == null || id != r.Id)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurante.Include(ri => ri.Adresa).FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    restaurant.Descriere = r.Descriere;
                    restaurant.Nume = r.Nume;
                    restaurant.Poza = r.Poza;
                    restaurant.Adresa.Oras = r.Adresa.Oras;
                    restaurant.Adresa.Strada = r.Adresa.Strada;
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.Id))
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
            return View(r);
        }

        // GET: Restaurante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurante == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurante.Include(r => r.Adresa).FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restaurante == null)
            {
                return Problem("Entity set 'AppDbContext.Restaurante'  is null.");
            }
            var restaurant = await _context.Restaurante.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurante.Remove(restaurant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurante.Any(e => e.Id == id);
        }
    }
}