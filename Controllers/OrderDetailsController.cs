using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bakari.Data;
using Bakari.Models;

namespace Bakari.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly BakariContext _context;

        public OrderDetailsController(BakariContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var bakariContext = _context.OrderDetail.Include(o => o.Item).Include(o => o.Order);
            return View(await bakariContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .Include(o => o.Item)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }
        // GET: OrderDetails/Create
        public IActionResult CustomerOrder()
        {
           
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemName");
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderNumber");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerOrder([Bind("OrderDetailId,OrderId,ItemId,,Quantity,UnitPrice,TotalPrice,Orderby")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
          
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemName", orderDetail.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderNumber", orderDetail.OrderId);
            return View(orderDetail);
        }
        // GET: OrderDetails/Create
        public IActionResult Create()
        {
          
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId");
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,OrderId,ItemId,CustomerId,Quantity,UnitPrice,TotalPrice,Orderby")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId", orderDetail.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
           
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId", orderDetail.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,OrderId,ItemId,Quantity,UnitPrice,TotalPrice,Orderby")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailId))
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
           
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId", orderDetail.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .Include(o => o.Item)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetail.Remove(orderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetail.Any(e => e.OrderDetailId == id);
        }
    }
}
