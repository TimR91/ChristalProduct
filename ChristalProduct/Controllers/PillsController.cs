using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChristalProduct.Data;
using ChristalProduct.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ChristalProduct.Controllers
{
    public class PillsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public PillsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Pills
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pills.ToListAsync());
        }

        // GET: Pills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pills = await _context.Pills
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pills == null)
            {
                return NotFound();
            }

            return View(pills);
        }

        // GET: Pills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PillCount,PillCost,PillIngredients,Description,Milligrams")] Pills pills, IFormFile ImagePath)
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
                pills.ImagePath = "uploads/productImages/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(pills);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pills);
        }

        // GET: Pills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pills = await _context.Pills.SingleOrDefaultAsync(m => m.Id == id);
            if (pills == null)
            {
                return NotFound();
            }
            return View(pills);
        }

        // POST: Pills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PillCount,PillCost,PillIngredients,Description,Milligrams")] Pills pills, IFormFile ImagePath)
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
                pills.ImagePath = "uploads/productImages/" + fileName;
                if (id != pills.Id)
                {
                    return NotFound();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pills);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PillsExists(pills.Id))
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
            return View(pills);
        }

        // GET: Pills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pills = await _context.Pills
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pills == null)
            {
                return NotFound();
            }

            return View(pills);
        }

        // POST: Pills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pills = await _context.Pills.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pills.Remove(pills);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PillsExists(int id)
        {
            return _context.Pills.Any(e => e.Id == id);
        }

        //search for method
        public IActionResult Search(string searchFor)
        {
            if (searchFor == null)
            {
                return NotFound();
            }

            
            List<Pills> allpills = _context.Pills.ToList();
            List<Pills> pills = null;
            string searchToken = searchFor;
                    pills = allpills.Where(s => (s.Name.IndexOf(searchToken,StringComparison.CurrentCultureIgnoreCase) >= 0)).ToList();
             
            ViewBag.SearchTerm = searchFor;
            // return View("~/Views/Songs/Index.cshtml", songs);
            return View(pills);
        }

    }
}
