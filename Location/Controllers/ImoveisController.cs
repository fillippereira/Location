using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Location.Models;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Location.Controllers
{
    public class ImoveisController : Controller
    {
        private readonly LocationContext _context;
        private readonly IHostingEnvironment _environment;

        public ImoveisController(LocationContext context, IHostingEnvironment IHostingEnvironment)
        {
            _context = context;
            _environment = IHostingEnvironment;
        }

        // GET: Imoveis
        public async Task<IActionResult> Index()
        {
            var locationContext = _context.Imovel.Include(i => i.Tipo).Include( i=> i.Fotos);
            return View(await locationContext.ToListAsync());
        }

        // GET: Imoveis/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imovel
                .Include(i => i.Tipo)
                .Include(i => i.Fotos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }



        public async Task<IActionResult> Reservar(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imovel
                .Include(i => i.Tipo)
                .Include(i => i.Fotos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imovel == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            return View(imovel);
        }

        // GET: Imoveis/Create
        public IActionResult Create()
        {
            ViewData["TipoId"] = new SelectList(_context.Tipo, "Id", "TipoImovel");
            return View();
        }

        // POST: Imoveis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,TipoId,QUartos,Banheiros,Vagas,Area,Endereco,Numero,CEP,Bairro,Cidade,Uf")] Imovel imovel)
        {
            if (ModelState.IsValid)
            {
                imovel.Id = Guid.NewGuid();
                _context.Add(imovel);
                await _context.SaveChangesAsync();

                

                if (HttpContext.Request.Form.Files != null)
                {
                    var files = HttpContext.Request.Form.Files;

                     await AddFoto(imovel, files, _context);
                }


                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoId"] = new SelectList(_context.Tipo, "Id", "TipoImovel", imovel.TipoId);
            return View(imovel);
        }

        public async Task<IActionResult> AddFoto(Imovel imovel,IFormFileCollection files, LocationContext _context)
        {
            var nomeFoto = string.Empty;

            //Verifica se o diretorio foi criado, se não cria um pasta para armazenar as imagens
            string path = imovel.Diretorio(_environment, imovel.Id);

            var foto = string.Empty;
            string PathDB = string.Empty;
            

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    foto = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var guidFoto = Convert.ToString(Guid.NewGuid());
                    var extensao = Path.GetExtension(foto);
                    nomeFoto = guidFoto + extensao;
                    foto = Path.Combine(_environment.WebRootPath, path) + $@"\{nomeFoto}";
                    PathDB = "Storage/" + nomeFoto;
                    using (FileStream fs = System.IO.File.Create(foto))
                    {
                        file.CopyTo(fs);
                        fs.Flush();

                        var novaFoto = new Foto
                        {
                            Imovel = imovel,
                            Caminho = nomeFoto
                        };

                        _context.Add(novaFoto);
                        await _context.SaveChangesAsync();

                    }
                }
            }
            return NoContent();
        }

        // GET: Imoveis/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imovel
                .Include(i => i.Tipo)
                .Include(i => i.Fotos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imovel == null)
            {
                return NotFound();
            }

            ViewData["TipoId"] = new SelectList(_context.Tipo, "Id", "TipoImovel", imovel.TipoId);
            return View(imovel);
        }

        // POST: Imoveis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,TipoId,QUartos,Banheiros,Vagas,Area,Endereco,Numero,CEP,Bairro,Cidade,Uf")] Imovel imovel)
        {
            if (id != imovel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imovel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImovelExists(imovel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (HttpContext.Request.Form.Files != null)
                {
                    var files = HttpContext.Request.Form.Files;

                    await AddFoto(imovel, files, _context);
                }


                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoId"] = new SelectList(_context.Tipo, "Id", "TipoImovel", imovel.TipoId);
            return View(imovel);
        }

        // GET: Imoveis/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imovel
                .Include(i => i.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // POST: Imoveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var imovel = await _context.Imovel.FindAsync(id);


            _context.Imovel.Remove(imovel);
            await _context.SaveChangesAsync();

            string path = imovel.Diretorio(_environment, imovel.Id);

            if (Directory.Exists(path))
                Directory.Delete(path,true);

            return RedirectToAction(nameof(Index));
        }



        private bool ImovelExists(Guid id)
        {
            return _context.Imovel.Any(e => e.Id == id);
        }
    }
}
