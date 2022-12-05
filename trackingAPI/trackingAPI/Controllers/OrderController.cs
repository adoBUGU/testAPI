using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trackingAPI.Data;
using trackingAPI.Model;

namespace trackingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDBContext _context;

        public OrderController(OrderDBContext context) 
        { 
            _context = context; 
        }

        [HttpGet]
        [Route("Get")]
        public List<Order> GetOrder() 
        {
            //return _context.Orders.ToList();
            return _context.Orders.Include(Order => Order.Items).ToList();
    
        }
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("Get/{id}")]
        public Order GetOrderbyID(int id)
        {
            //return _context.Orders.SingleOrDefault(x => x.OrderID == id);
            return _context.Orders.Where(x => x.OrderID == id).Include(Order => Order.Items).First();

        }
        //[HttpDelete("{id}")]
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id) 
        { 
            var del = _context.Orders.SingleOrDefault(x=>x.OrderID ==id);
            if (del == null)
            {
                return NotFound("Order with Order ID " + id + " does not exist");
            }
            _context.Orders.Remove(del);
            _context.SaveChanges();
            return Ok("Order with Order ID " + id + " deleted Successfully");

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddOrder(Order order) 
        { 
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Created("api/Orders/" + order.OrderID, order);
        }

        [HttpGet]
        [Route("GetByPage/{pageNo}/{noOfOrders}")]
        public List<Order> GetOrders(int pageNo, int noOfOrders)
        {
            return _context.Orders.Include(Order => Order.Items).Skip((pageNo - 1) * noOfOrders).Take(noOfOrders).ToList();
  
        }


        //[HttpPut("{id}")]
        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult Update(int id, Order order)
        {
            var odr = _context.Orders.Where(x => x.OrderID == id).Include(order => order.Items).FirstOrDefault(); 
            if (odr == null)
            {
                return NotFound("Order with Order ID " + id + " does not exist");
            }

            if (order.CustomerName != null)
            {
                odr.CustomerName = order.CustomerName;
            }
            if (order.Address != null)
            {
                odr.Address = order.Address;
            }

            foreach (Item item in odr.Items)
            {
                if (!odr.Items.Any(t => t.ItemID == item.ItemID))
                    _context.Items.Remove(item);
            }

            foreach (var newItem in order.Items)
            {
                var existingItem = odr.Items
                    .Where(t => t.ItemID == newItem.ItemID && t.ItemID != default(int))
                    .SingleOrDefault();

                if (existingItem != null)
                    _context.Entry(existingItem).CurrentValues.SetValues(newItem);
                else
                {
                    var insertitem = new Item
                    {
                        OrderId = newItem.OrderId,
                        ProductID = newItem.ProductID,
                        ProductQuantity = newItem.ProductQuantity

                    };
                    odr.Items.Add(insertitem);
                }
            }
            _context.SaveChangesAsync();
            //_context.Update(odr);
            //_context.SaveChanges();
            return Ok("Order with the Order ID " + id + " Updated Succesfully.");
        }
    }
}
