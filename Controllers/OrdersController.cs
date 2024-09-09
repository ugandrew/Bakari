using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bakari.Data;
using Bakari.Models;
using Bakari.Migrations;

namespace Bakari.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BakariContext _context;

        public OrdersController(BakariContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }
        public async Task<IActionResult> OrderList(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;


            var orders = from s in _context.Order
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.OrderNumber.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    orders = orders.OrderByDescending(s => s.OrderDate);
                    break;
              

                default:
                    orders = orders.OrderByDescending(s => s.OrderDate);
                    break;
            }


            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,OrderNumber,SubTotal,Discount,OrderTotal")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }
        public async Task<IActionResult> OrderDiscount(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDiscount(int id, [Bind("OrderId,OrderDate,OrderNumber,SubTotal,Discount,OrderTotal,Orderby")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }
            order.OrderTotal = order.SubTotal - order.Discount;
            var trans = await _context.Transanction
               .FirstOrDefaultAsync(m => m.Description == order.OrderNumber);
            if (trans != null) {
                trans.Amount = order.OrderTotal;
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                try
                {
                    _context.Transanction.Update(trans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(OrderList));
            }
            return View(order);
        }

        public async Task<IActionResult> OrderCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderCustomer(int id, [Bind("OrderId,OrderDate,OrderNumber,SubTotal,Discount,OrderTotal,Orderby")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }
            order.OrderTotal = order.SubTotal - order.Discount;
            var trans = await _context.Transanction
               .FirstOrDefaultAsync(m => m.Description == order.OrderNumber);
            if (trans != null)
            {
                trans.Amount = order.OrderTotal;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (trans != null)
                {
                    try
                    {
                        _context.Transanction.Update(trans);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(OrderList));
            }
            return View(order);
        }
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,OrderNumber,SubTotal,Discount,OrderTotal")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> CancelOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["orderid"] = order.OrderId;
            ViewData["orderdate"] = order.OrderDate;
            ViewData["ordernumber"] = order.OrderNumber;
            ViewData["ordersubtotal"] = order.SubTotal.ToString(format: "#,##0");
            ViewData["orderdiscount"] = order.Discount.ToString(format: "#,##0");
            ViewData["ordertotal"] = order.OrderTotal.ToString(format: "#,##0");

            var orderdetail = await _context.OrderDetail
               .FirstOrDefaultAsync(m => m.OrderId == id);
            

            return View(_context.OrderDetail
                .Include(x => x.Order)
                .Include(y => y.Item)
                .Where(x => x.OrderId == id).ToList());
        }
        [HttpPost, ActionName("CancelOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);


            if (order != null)
            {

                if (! await ReverseStockQuantityAsync(order))
                {
                    return NotFound();  
                }

                var trans = await _context.Transanction.FirstOrDefaultAsync(m => m.Description == order.OrderNumber);

                if (trans != null)
                {

                    _context.Transanction.Remove(trans);
                    await _context.SaveChangesAsync();
                }

             
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderList));
        }
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            
          

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
           

            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
           
            return RedirectToAction(nameof(OrderList));
        }
        private async Task<bool> ReverseStockQuantityAsync(Order order)
        {
            if (order == null) 
            {
                return false;
            }
            var orderdeatils = _context.OrderDetail
                .Include(x => x.Order)
                .Include(y => y.Item)
                .Where(x => x.OrderId == order.OrderId).ToList();
            foreach (var item in orderdeatils) 
            {
                var stock = await _context.Stock.FirstOrDefaultAsync(m => m.ItemId == item.ItemId);
                if (stock != null) 
                {
                    stock.Quantity = +item.Quantity;
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(stock);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            throw;
                        }
                        _context.OrderDetail.Remove(item);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            return false;
            
           

           
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
