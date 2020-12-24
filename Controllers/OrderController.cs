using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartOrder.API.DTOs;
using SmartOrder.API.Model;

//Controller για τη διαχειριση των παραγγελιών

namespace SmartOrder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly DB_A25A8E_orderspapiContext _context;

        public OrderController(ILogger<OrderController> logger, DB_A25A8E_orderspapiContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("HasNew/{storeId}/{clientCount}")]
        public bool HasNewOrders(int storeId, int clientCount)
        {
            //φέρνει απο τη βάση το πλήθος των pending orders του καταστήματος
            var dbOrdersCount = _context.Orders.Where(o => o.Status == 1 && o.StoreId == storeId).Count();
            //αν ειναι διαφορετικές απο αυτές που γνωρίζει ο client, σημαίνει ότι προστέθηκε νέα παραγγελία
            return clientCount != dbOrdersCount;
        }

        // φέρνει τις pending orders
        [HttpGet("Pending/{storeId}")]
        public IEnumerable<OrderDTO> GetPendingOrders(int storeId)
        {
            return GetOrders(1, storeId);
        }

        // φέρνει τις in progress orders
        [HttpGet("InProgress/{storeId}")]
        public IEnumerable<OrderDTO> GetInProgressOrders(int storeId)
        {
            return GetOrders(2, storeId);
        }

        // Controller action που θα καλεστει απο τον client όταν ένα μαγαζί κάνει accept μια παραγγελία
        [HttpGet("Accept/{orderId}/{minutes}")]
        public async Task AcceptOrder(int orderId, int minutes)
        {
            // φέρνουμε απο την βαση την παραγγελία με το id που ζητήθηκε
            var order = _context.Orders.First(o => o.Id == orderId);
            var firebaseTable = "";
            // ανάλογα το status της παραγγελίας μαρκάρεται είτε ως finished είτε ως pending
            // επίσης βρίσκουμε το table της firebase που πρέπει να ενημερωθεί ώστε να σταλεί αντίστοιχο Notification στον πελάτη
            if (order.Status == 2)
            {
                firebaseTable = "finished_orders";
                order.Status = 3;
            }
            else
            {
                firebaseTable = "pending_orders";
                order.Status = 2;
            }
            //ενημερώνονται τα λεπτά που θα ολοκληρωθεί η παραγγελία και αποθηκεύεται στη βάση δεδομένων
            order.EstimatedMinutes = minutes;
            _context.SaveChanges();

            //Συνδεση με τη firebase 
            var firebaseClient = new FirebaseClient(
                "https://smartorder-f831d.firebaseio.com/",
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult("ZyRTXq7TCUVeJvxZw8RYNlgo1qBgk1Q0x9IHz7AM") 
                });
            //στέλνεται στη firebase ο αριθμός της παραγγελίας και ο χρόνος άφιξης σε λεπτά
            await firebaseClient
                .Child(firebaseTable)
                .PutAsync("{ \"" + orderId + "\": " + minutes + " }");

            //return new { Ok = true };
        }
        private IEnumerable<OrderDTO> GetOrders(int status, int storeId)
        {
            // φέρνει από τη βάση τις παραγγελίες του συγκεκριμένου καταστήματος για το συγκεκριμένο status
            var orders = _context.Orders.Where(o => o.Status == status && o.StoreId == storeId);

            //μετρέπονται τα αντικείμενα της βάσης στα αντικείμενα που περιμένει ο client (orderDTO)
            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                DateCreated = o.DateCreated,
                Status = o.Status,
                Store = o.StoreId,
                TotalPrice = o.TotalPrice,
                User = new UserDTO
                {
                    Email = o.User.Email,
                    Name = o.User.Name,
                    Phone = o.User.Phone
                },
                OrderItems = o.OrderItems.Select(i => new OrderItemDTO
                {
                    Quantity = i.Quantity,
                    Menuitem = new MenuItemDTO
                    {
                        Id = i.Menuitem.Id,
                        Price = i.Menuitem.Price,
                        Title = i.Menuitem.Title
                    }
                })
            });
        }
    }
}