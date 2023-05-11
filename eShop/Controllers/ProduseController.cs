using System.Linq;
using System.Threading.Tasks;
using eShop.Data;
using eShop.DTO;
using eShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers
{
    public class ProduseController : Controller
    {
        private readonly AppDbContext _context;

        public ProduseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produse
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Produse.Include(m => m.Restaurant).OrderBy(m => m.Denumire);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Produse/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var detaliiProdus = await GetProdusByIdAsync(id);
            return View(detaliiProdus);
        }

        // GET: Produse/Create
        public async Task<IActionResult> Create()
        {
            var produsDropdownsData = await GetNewProdusDropdownsValues();

            ViewBag.listaRestaurante = new SelectList(produsDropdownsData.listaRestaurante, "Id", "Nume");
            ViewBag.listaIngrediente = new SelectList(produsDropdownsData.listaIngrediente, "Id", "Denumire");

            return View();
        }

        // POST: Produse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdusNou produsNou)
        {
            if (ModelState.IsValid)
            {
                await AddNewProdusAsync(produsNou);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var produsDropdownsData = await GetNewProdusDropdownsValues();

            ViewBag.listaRestaurante = new SelectList(produsDropdownsData.listaRestaurante, "Id", "Nume");
            ViewBag.listaIngrediente = new SelectList(produsDropdownsData.listaIngrediente, "Id", "Denumire");
            return View(produsNou);
        }

        public async Task AddNewProdusAsync(ProdusNou data)
        {
            var newProdus = new Produs()
            {
                Denumire = data.Denumire,
                Descriere = data.Descriere,
                Pret = data.Pret,
                Imagine = data.Imagine,
                RestaurantId = data.RestaurantId,
                
            };
            await _context.Produse.AddAsync(newProdus);
            await _context.SaveChangesAsync();

            //Add Produs ingredient
            foreach (var ingredientId in data.listaIdIngrediente)
            {
                var newIngredientProdus = new Produs_Ingredient()
                {
                    ProdusId = newProdus.Id,
                    IngredientId = ingredientId
                };
                await _context.Produse_Ingrediente.AddAsync(newIngredientProdus);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<DropDownProdusNou> GetNewProdusDropdownsValues()
        {
            var response = new DropDownProdusNou()
            {
                listaIngrediente = await _context.Ingrediente.OrderBy(n => n.Denumire).ToListAsync(),
                listaRestaurante = await _context.Restaurante.OrderBy(n => n.Nume).ToListAsync(),
            };

            return response;
        }

        // GET: Produse/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var detaliiProdus = await GetProdusByIdAsync(id);
            if (_context.Produse == null)
            {
                return NotFound();
            }

            var response = new ProdusNou()
            {
                Id = detaliiProdus.Id,
                Denumire = detaliiProdus.Denumire,
                Descriere = detaliiProdus.Descriere,
                Pret = detaliiProdus.Pret,
                Imagine = detaliiProdus.Imagine,
                RestaurantId = detaliiProdus.RestaurantId,
                listaIdIngrediente = detaliiProdus.listaProduse_Ingrediente.Select(n => n.IngredientId).ToList(),
            };

            var produsDropdownsData = await GetNewProdusDropdownsValues();
            ViewBag.listaRestaurante = new SelectList(produsDropdownsData.listaRestaurante, "Id", "Nume");
            ViewBag.listaIngrediente = new SelectList(produsDropdownsData.listaIngrediente, "Id", "Denumire");
            return View(response);
        }

        public async Task<Produs> GetProdusByIdAsync(int id)
        {
            var detaliiProdus = await _context.Produse
                .Include(r => r.Restaurant)
                .Include(pi => pi.listaProduse_Ingrediente).ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(n => n.Id == id);

            return detaliiProdus;
        }

        // POST: Produse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProdusNou produs)
        {
            if (id != produs.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var produsDropdownsData = await GetNewProdusDropdownsValues();

                ViewBag.listaRestaurante = new SelectList(produsDropdownsData.listaRestaurante, "Id", "Nume");
                ViewBag.listaIngrediente = new SelectList(produsDropdownsData.listaIngrediente, "Id", "Denumire");

                return View(produs);
            }

            await UpdateProdusAsync(produs);
            return RedirectToAction(nameof(Index));
        }

        public async Task UpdateProdusAsync(ProdusNou data)
        {
            var dbProdus = await _context.Produse.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProdus != null)
            {
                dbProdus.Denumire = data.Denumire;
                dbProdus.Descriere = data.Descriere;
                dbProdus.Pret = data.Pret;
                dbProdus.Imagine = data.Imagine;
                dbProdus.RestaurantId = data.RestaurantId;
                await _context.SaveChangesAsync();
            }

            var existingIngredienteDb = _context.Produse_Ingrediente.Where(n => n.ProdusId == data.Id).ToList();
            _context.Produse_Ingrediente.RemoveRange(existingIngredienteDb);
            await _context.SaveChangesAsync();

            //Add Produse Ingredient
            foreach (var ingredientId in data.listaIdIngrediente)
            {
                var newIngredientProdus = new Produs_Ingredient()
                {
                    ProdusId = data.Id,
                    IngredientId = ingredientId
                };
                await _context.Produse_Ingrediente.AddAsync(newIngredientProdus);
            }
            await _context.SaveChangesAsync();
        }


        // GET: Produse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produse == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse
                .Include(p => p.Restaurant)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (produs == null)
            {
                return NotFound();
            }

            return View(produs);
        }

        // POST: Produse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produse == null)
            {
                return Problem("Entity set 'AppDbContext.listaProduse'  is null.");
            }
            var movie = await _context.Produse.FindAsync(id);
            if (movie != null)
            {
                _context.Produse.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
