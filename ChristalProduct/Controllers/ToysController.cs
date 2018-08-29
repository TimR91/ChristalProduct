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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ChristalProduct.Controllers
{
    public class ToysController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;


        public ToysController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Toys
        public async Task<IActionResult> Index()
        {
            return View(await _context.Toys.ToListAsync());
        }

        // GET: Toys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toys = await _context.Toys
                .SingleOrDefaultAsync(m => m.Id == id);
            if (toys == null)
            {
                return NotFound();
            }

            return View(toys);
        }

        // GET: Toys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Recharable,Battery,WaterProof,Glass,Silicon,Realistic")] Toys toys, IFormFile ImagePath)
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
                toys.ImagePath = "uploads/productImages/" + fileName;
            }
                if (ModelState.IsValid)
                {
                    _context.Add(toys);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(toys);
        }
        

        // GET: Toys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toys = await _context.Toys.SingleOrDefaultAsync(m => m.Id == id);
            if (toys == null)
            {
                return NotFound();
            }
            return View(toys);
        }

        // POST: Toys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Recharable,Battery,WaterProof,Glass,Silicon,Realistic")] Toys toys, IFormFile ImagePath)
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
                toys.ImagePath = "uploads/productImages/" + fileName;
            }
            if (id != toys.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toys);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToysExists(toys.Id))
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
            return View(toys);
        }

        // GET: Toys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toys = await _context.Toys
                .SingleOrDefaultAsync(m => m.Id == id);
            if (toys == null)
            {
                return NotFound();
            }

            return View(toys);
        }

        // POST: Toys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toys = await _context.Toys.SingleOrDefaultAsync(m => m.Id == id);
            _context.Toys.Remove(toys);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToysExists(int id)
        {
            return _context.Toys.Any(e => e.Id == id);
        }
    }
}
