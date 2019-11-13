using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Location.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Location.Controllers
{
    public class FotosController : Controller
    {
        private readonly LocationContext _context;
        private readonly IHostingEnvironment _environment;
        public FotosController(LocationContext context, IHostingEnvironment IHostingEnvironment)
        {
            _context = context;
            _environment = IHostingEnvironment;
        }

        // GET: Fotos
        public async Task<IActionResult> Index()
        {
            var locationContext = _context.Foto.Include(f => f.Imovel);
            return View(await locationContext.ToListAsync());
        }

        // GET: Fotos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foto = await _context.Foto
                .Include(f => f.Imovel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foto == null)
            {
                return NotFound();
            }

            return View(foto);
        }

        // GET: Fotos/Create
        public IActionResult Create()
        {
            ViewData["ImovelId"] = new SelectList(_context.Imovel, "Id", "Bairro");
            return View();
        }

        // POST: Fotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImovelId,Caminho")] Foto foto)
        {
            if (ModelState.IsValid)
            {
                foto.Id = Guid.NewGuid();
                _context.Add(foto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImovelId"] = new SelectList(_context.Imovel, "Id", "Bairro", foto.ImovelId);
            return View(foto);
        }

        // GET: Fotos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foto = await _context.Foto.FindAsync(id);
            if (foto == null)
            {
                return NotFound();
            }
            ViewData["ImovelId"] = new SelectList(_context.Imovel, "Id", "Bairro", foto.ImovelId);
            return View(foto);
        }

        // POST: Fotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ImovelId,Caminho")] Foto foto)
        {
            if (id != foto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotoExists(foto.Id))
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
            ViewData["ImovelId"] = new SelectList(_context.Imovel, "Id", "Bairro", foto.ImovelId);
            return View(foto);
        }

        // GET: Fotos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foto = await _context.Foto
                .Include(f => f.Imovel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foto == null)
            {
                return NotFound();
            }

            return View(foto);
        }

        // POST: Fotos/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var foto = await _context.Foto.FindAsync(id);
            _context.Foto.Remove(foto);
            await _context.SaveChangesAsync();

            string path = _environment.WebRootPath + "\\Storage\\" + foto.ImovelId+"\\"+foto.Caminho;

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            return NoContent();
        }

        private bool FotoExists(Guid id)
        {
            return _context.Foto.Any(e => e.Id == id);
        }
    }
}
