using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bakari.Data;
using Bakari.Models;
using System.Diagnostics.Metrics;

namespace Bakari.Controllers
{
    public class ItemsController : Controller
    {
        private readonly BakariContext _context;

        public ItemsController(BakariContext context)
        {
            _context = context;
        }
        // GET: Sale
        public async Task<IActionResult> Sale(string searchString, string sortOrder)
        {
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["CurrentFilter"] = searchString;


                var products = from s in _context.Item.Include(p => p.Category)
                               select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    products = products.Where(s => s.ItemName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        products = products.OrderByDescending(s => s.ItemName);
                        break;


                    default:
                        products = products.OrderBy(s => s.ItemName);
                        break;
                }


                return View(await products.ToListAsync());
        }

       //Add to Basket
       public async Task<IActionResult> AddtoBasket(int? id)
            {
                if (id == null || _context.Item == null)
                {
                    return NotFound();
                }
            //Basket basket;
                var item = await _context.Item
                    .Include(i => i.Category)
                    .FirstOrDefaultAsync(m => m.ItemId == id);
                if (item != null)
                {


                    /*if (Viewdata["CartCounter"] != null)
                    {
                        _carts = ViewData["CartCounter"] as List<Cart>;
                    }
                    */
                    //counter++;
                    //Get list of items in basket
                    var BasketItems = _context.Basket.ToList();
                    if (BasketItems.Any(model => model.ItemId == id))
                    {
                        var basket = BasketItems.Single(model => model.ItemId == id);
                        basket.Quantity++;
                        basket.TotalPrice = basket.UnitPrice * basket.Quantity;

                        if (ModelState.IsValid)
                        {
                            try
                            {
                                _context.Update(basket);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                throw;
                            }

                        }
                    }
                    //new item added to bask
                    else
                    {
                        Basket basket = new()
                        {
                            ItemId = item.ItemId,
                            ImagePath = item.ImagePath,
                            UnitPrice = item.ItemPrice,
                            Quantity = 1,
                            TotalPrice = item.ItemPrice,
                            Item = item
                        };
                  
                        if (ModelState.IsValid)
                        {
                            _context.Add(basket);
                            await _context.SaveChangesAsync();

                        }
                        //_carts.Add(cart);

                    }
                    //Counter++;
                    //ViewData["BasketCounter"] = counter;
                    //ViewData["Cart"] = _carts;

                    return RedirectToAction(nameof(Sale));
                }
                else
                {
                    return NotFound();
                }
            }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;


            var products = from s in _context.Item.Include(p => p.Category)
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ItemName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.ItemName);
                    break;


                default:
                    products = products.OrderBy(s => s.ItemName);
                    break;
            }


            return View(await products.ToListAsync());
        }


        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var item = await _context.Item
                    .Include(i => i.Category)
                    .FirstOrDefaultAsync(m => m.ItemId == id);
                if (item == null)
                {
                    return NotFound();
                }

                return View(item);
            }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,CategoryId,ItemCode,ItemName,Description,ProductPrice,ImagePath")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,CategoryId,ItemCode,ItemName,Description,ProductPrice,ImagePath")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item != null)
            {
                _context.Item.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
