using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChristalProduct.Data;
using ChristalProduct.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ChristalProduct.Controllers
{
    public class LubesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;


        public LubesController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Lubes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lubes.ToListAsync());
        }

        // GET: Lubes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lubes = await _context.Lubes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lubes == null)
            {
                return NotFound();
            }

            return View(lubes);
        }

        // GET: Lubes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lubes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,SiliconBased,WaterBased,Hybrid,Desensitizing,Flavor,AddedEffects")] Lubes lubes, IFormFile ImagePath)
        {
            if (ImagePath != null)
            {
                var fileName = Path.GetFileName(ImagePath.FileName);
                var path = _env.WebRootPath + "\\uploads\\productImages\\" + fileName;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }

                //update song path
                lubes.ImagePath = "uploads/productImages/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(lubes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lubes);
        }

        // GET: Lubes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lubes = await _context.Lubes.SingleOrDefaultAsync(m => m.Id == id);
            if (lubes == null)
            {
                return NotFound();
            }
            return View(lubes);
        }

        // POST: Lubes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,SiliconBased,WaterBased,Hybrid,Desensitizing,Flavor,AddedEffects")] Lubes lubes, IFormFile ImagePath)
        {
            if (ImagePath != null)
            {
                var fileName = Path.GetFileName(ImagePath.FileName);
                var path = _env.WebRootPath + "\\uploads\\productImages\\" + fileName;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }

                //update song path
                lubes.ImagePath = "uploads/productImages/" + fileName;
            }
            if (id != lubes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lubes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LubesExists(lubes.Id))
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
            return View(lubes);
        }

        // GET: Lubes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lubes = await _context.Lubes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lubes == null)
            {
                return NotFound();
            }

            return View(lubes);
        }

        // POST: Lubes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lubes = await _context.Lubes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Lubes.Remove(lubes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LubesExists(int id)
        {
            return _context.Lubes.Any(e => e.Id == id);
        }
    }
}
