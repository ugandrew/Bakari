using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bakari.Data;
using Bakari.Models;
using Microsoft.IdentityModel.Tokens;
using Bakari.Migrations;

namespace Bakari.Controllers
{
    public class BasketsController : Controller
    {
        private readonly BakariContext _context;

        public BasketsController(BakariContext context)
        {
            _context = context;
        }
        public IActionResult EmptyBasket()
        {
            return View();
        }

        // GET: Baskets
        public async Task<IActionResult> Index()
        {
            if (_context.Basket.IsNullOrEmpty())
            {
                return RedirectToAction(nameof(EmptyBasket));
            }
            var glamManageContext = _context.Basket.Include(b =>b.Item);
            return View(await glamManageContext.ToListAsync());
        }

        public async Task<IActionResult> BasketList()
        {
            if (_context.Basket.IsNullOrEmpty())
            {
                return RedirectToAction(nameof(EmptyBasket));
            }
            
            var bakariContext = _context.Basket.Include(b => b.Item);
            ViewData["baskettotal"] = bakariContext.Sum(t=>t.TotalPrice);
            return View(await bakariContext.ToListAsync());
        }
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null || _context.Basket == null)
            {
                return NotFound();
            }

            var basket = await _context.Basket
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.BasketId == id);

            if (basket != null)
            {
                _context.Basket.Remove(basket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BasketList));


        }
        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null || _context.Basket == null)
            {
                return NotFound();
            }

            var basket = await _context.Basket
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.BasketId == id);

            if (basket != null)
            {
                basket.Quantity++;
                basket.TotalPrice = basket.Quantity * basket.UnitPrice;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketExists(basket.BasketId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }


            return RedirectToAction(nameof(BasketList));


        }

        public async Task<IActionResult> Reduce(int? id)
        {
            if (id == null || _context.Basket == null)
            {
                return NotFound();
            }

            var basket
                = await _context.Basket
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.BasketId == id);

            if (basket != null)
            {
                basket.Quantity--;
                basket.TotalPrice = basket.Quantity *basket.UnitPrice;
            }
            if (basket.Quantity < 1)
            {
                _context.Basket.Remove(basket);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(basket);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BasketExists(basket.BasketId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

            }

            return RedirectToAction(nameof(BasketList));


        }





        // GET: Baskets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _context.Basket
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.BasketId == id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // GET: Baskets/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId");
            return View();
        }

        // POST: Baskets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasketId,ItemId,Quantity,UnitPrice,TotalPrice,ImagePath")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId", basket.ItemId);
            return View(basket);
        }

        // GET: Baskets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _context.Basket.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId", basket.ItemId);
            return View(basket);
        }

        // POST: Baskets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BasketId,ItemId,Quantity,UnitPrice,TotalPrice,ImagePath")] Basket basket)
        {
            if (id != basket.BasketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketExists(basket.BasketId))
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
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId", basket.ItemId);
            return View(basket);
        }

        // GET: Baskets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _context.Basket
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.BasketId == id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basket = await _context.Basket.FindAsync(id);
            if (basket != null)
            {
                _context.Basket.Remove(basket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasketExists(int id)
        {
            return _context.Basket.Any(e => e.BasketId == id);
        }
        public async Task<IActionResult> CheckOut()
        {
            if (_context.Basket.IsNullOrEmpty())
            {
                return RedirectToAction("Salelist", "Items");
            }
            
            if (await CompareStockAsync())
            {
                if (await ReduceStockAsync())
                {
                    //create order
                    var order = new Order();

                    order = await CreateOrder<Order>(TotalBasket());
                    if (order != null)
                    {
                        if (await AddOrderDetail<bool>(order))
                        {
                            //Clear Shopping Basket
                            await ClearBasketAsync();
                            //add transaction
                            await AddTransanctionAsync(order);
                            return RedirectToAction("OrderList", "Orders");
                        }
                    }
                    return Problem("Order was not created");
                }
                return Problem("There a problem with Stock Database.");
            }
            return Problem("Inventory is Short.");
        }

        private async Task<bool> ClearBasketAsync()
        {
            foreach (var item in _context.Basket.ToList())
            {

                _context.Basket.Remove(item);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        private async Task<bool> ReduceStockAsync()
        {

            if (_context.Basket != null)
            {
                foreach (var item in _context.Basket.ToList())
                {
                    var stock = await _context.Stock
                 .Include(b => b.Item)
                 .FirstOrDefaultAsync(m => m.ItemId == item.ItemId);

                    if (stock == null)
                    {
                        return false;
                    }
                    stock.Quantity -= item.Quantity;
                    stock.Total = stock.Quantity * stock.CostPrice;
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(stock);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            return false;
                        }
                    }


                }
                return true;

            }
            else
            {
                return false;
            }
        }

        private async Task<bool> CompareStockAsync()
        {

            if (_context.Basket != null)
            {
                foreach (var item in _context.Basket.ToList())
                {

                    var stock = await _context.Stock
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.ItemId == item.ItemId);

                    if (stock == null)
                    {
                        return false;
                    }
                    if (stock.Quantity < item.Quantity)
                    {
                        return false;
                    }
                }
                return true;

            }
            else
            {
                return false;
            }
        }

        public decimal TotalBasket()
        {
            decimal totalbask = 0;
            if (_context.Basket != null)
            {
                foreach (var item in _context.Basket.ToList())
                {
                    totalbask += item.TotalPrice;
                }
            }
            return totalbask;
        }
        public async Task<Order> CreateOrder<CreateOrder>(decimal totalbask)
        {
            Order order = new Order();

            order.OrderDate = DateTime.Now;
            order.OrderNumber = order.OrderDate.ToString("yymmmddHHmmss");
            order.Discount = 0;
            order.SubTotal = totalbask;
            order.OrderTotal = totalbask - order.Discount;
            order.Orderby = "";
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();

            }


            return order;
        }
        public async Task<bool> AddOrderDetail<AddOrderDetail>(Order order)
        {

            if (_context.Basket != null)
            {
                foreach (var item in _context.Basket.ToList())
                {
                    OrderDetail orderdetail = new()
                    {
                        OrderId = order.OrderId,
                        Order = order,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice,
                        Item = item.Item,
                        Orderby= order.Orderby
                        
                    };

                    if (ModelState.IsValid)
                    {
                        _context.Add(orderdetail);
                        await _context.SaveChangesAsync();

                    }

                }
                return true;

            }

            return false;
        }
        public async Task<bool> AddTransAsync()
        {

            if (_context.Transanction.IsNullOrEmpty())
            {
                foreach (var item in _context.Order.ToList())
                {
                    Transanction trans = new()
                    {
                        Date = item.OrderDate,
                        Amount = item.OrderTotal,
                        Description = item.OrderNumber,
                        Type=TransanctionType.CashIn
                    };
                    if (ModelState.IsValid)
                    {
                        _context.Add(trans);
                        await _context.SaveChangesAsync();

                    }

                }
                return true;

            }

            return false;
        }
      
       

        public async Task<bool> AddTransanctionAsync(Order order)
        {

            if (order != null)
            {
                Transanction trans = new()
                {

                    Date = order.OrderDate,
                    Amount = order.OrderTotal,
                    Description = order.OrderNumber,
                    Type = TransanctionType.CashIn
                };

              

                if (ModelState.IsValid)
                {
                    _context.Add(trans);
                    await _context.SaveChangesAsync();

                }

                return true;

            }

            return false;
        }

    }

}
