using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Care.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Care.Controllers
{
    public class ItemController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ServiceDbContext _context;

        public ItemController(ServiceDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = await _context.Items
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (itemModel == null)
            {
                return NotFound();
            }

            return View(itemModel);
        }

        // GET: Item/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Item/Upload
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([Bind("ImageId,Name,ImageFile")] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/ItemImages
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(itemModel.ImageFile.FileName);
                string extension = Path.GetExtension(itemModel.ImageFile.FileName);
                itemModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                Directory.CreateDirectory(wwwRootPath + "/ItemImages/");
                string path = Path.Combine(wwwRootPath + "/ItemImages/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await itemModel.ImageFile.CopyToAsync(fileStream);
                }
                //Insert record
                _context.Add(itemModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemModel);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = await _context.Items.FindAsync(id);
            if (itemModel == null)
            {
                return NotFound();
            }
            return View(itemModel);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Name,ImageName")] ItemModel itemModel)
        {
            if (id != itemModel.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemModelExists(itemModel.ImageId))
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
            return View(itemModel);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = await _context.Items
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (itemModel == null)
            {
                return NotFound();
            }

            return View(itemModel);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemModel = await _context.Items.FindAsync(id);
            _context.Items.Remove(itemModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemModelExists(int id)
        {
            return _context.Items.Any(e => e.ImageId == id);
        }
    }
}
