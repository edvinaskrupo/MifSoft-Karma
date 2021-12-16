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
using Microsoft.AspNetCore.Http;
using System.Dynamic;
using Care.Helpers;
using Autofac.Extras.DynamicProxy;

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

        // GET: Item/Market
        public async Task<IActionResult> Market()
        {
            List<UserAndItemModel> UsersAndItems = (await getItemInfo()).Where(item => item.UserId != HttpContext.Session.GetInt32("UserId")).ToList();
            return View(UsersAndItems);
        }

        // GET: Item/Inventory
        public async Task<IActionResult> Inventory()
        {
            List<UserAndItemModel> UsersAndItems = (await getItemInfo()).Where(item => item.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
            return View(UsersAndItems);
        }
        // GET: Item
        public async Task<IActionResult> Index()
        {
            List<UserAndItemModel> UsersAndItems = await getItemInfo();
            return View(UsersAndItems);
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
        public async Task<IActionResult> Upload([Bind("ImageId,Name,ImageFile,Category,Condition")] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/ItemImages
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName;
                string extension;
                
                try {
                    fileName = Path.GetFileNameWithoutExtension(itemModel.ImageFile.FileName);
                    extension = Path.GetExtension(itemModel.ImageFile.FileName);
                }
                catch (NullReferenceException) {
                    ModelState.AddModelError("ImageFile", "You must select an image.");
                    return View();
                }

                itemModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                Directory.CreateDirectory(wwwRootPath + "/ItemImages/");
                string path = Path.Combine(wwwRootPath + "/ItemImages/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await itemModel.ImageFile.CopyToAsync(fileStream);
                }
                //Insert record
                itemModel.UserId = (int) HttpContext.Session.GetInt32("UserId");
                _context.Add(itemModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Inventory));
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
        public async Task<IActionResult> Edit(int id, [Bind("UserId,ImageId,Name,ImageName")] ItemModel itemModel)
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
                if (Authenticator.GetUserType(HttpContext.Session.GetInt32("UserType")) == Authenticator.UserType.ADMIN)
                {
                    return RedirectToAction(nameof(Index));
                } else
                {
                    return RedirectToAction(nameof(Inventory));
                }
                
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

            //delete image from wwwroot/image
            //crashes if the file does not exist
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "ItemImages", itemModel.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record
            _context.Items.Remove(itemModel);
            await _context.SaveChangesAsync();
            if (Authenticator.GetUserType(HttpContext.Session.GetInt32("UserType")) == Authenticator.UserType.ADMIN)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Inventory));
            }
        }

        private bool ItemModelExists(int id)
        {
            return _context.Items.Any(e => e.ImageId == id);
        }

        public async Task<List<UserAndItemModel>> getItemInfo() {
            var Users = await _context.Users.ToListAsync();
            var Items = await _context.Items.ToListAsync();

            var list = (from item in Items
                        join user in Users on item.UserId equals user.UserId
                        orderby item.UserId
                        select new {
                            UserId = item.UserId, 
                            EmailAdress = user.EmailAddress, 
                            ItemName = item.Name,
                            ItemCategory = item.Category,
                            ItemCondition = item.Condition,
                            ImageName = item.ImageName,
                            ImageId = item.ImageId
                        }).AsEnumerable().Select(linq => new UserAndItemModel {
                            UserId = linq.UserId,
                            EmailAddress = linq.EmailAdress,
                            ItemName = linq.ItemName,
                            ItemCategory = linq.ItemCategory,
                            ItemCondition = linq.ItemCondition,
                            ImageName = linq.ImageName,
                            ImageId = linq.ImageId
                        }).ToList();
            
            return list;
        }
    }
}
