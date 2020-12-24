using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartOrder.API.DTOs;
using SmartOrder.API.Model;

// Controller για τη διαχείριση του κάθε καταστήματος

namespace SmartOrder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {

        private readonly ILogger<StoreController> _logger;
        private readonly DB_A25A8E_orderspapiContext _context;

        public StoreController(ILogger<StoreController> logger, DB_A25A8E_orderspapiContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<StoreDTO> Get()
        {
            return _context.Stores.Select(s => new StoreDTO
            {
                Id = s.Id,
                Name = s.Name
            });
        }

        //register a new store
        [HttpPost("Register")]
        public string Register(RegisterDTO data)
        {
            //Ensure passwords are same
            if (data.Password != data.RepeatPassword)
            {
                return "Passwords do not match";
            }

            //Ensure password is long enough
            if (data.Password == null || data.Password.Length < 3)
            {
                return "Password too short";
            }

            //Ensure store does not exist
            var store = _context.Stores.FirstOrDefault(s => s.Name == data.StoreName);
            if (store != null)
            {
                return "Store already exists";
            }

            //create store
            var nextId = 1;
            var latestStore = _context.Stores.OrderByDescending(s => s.Id).FirstOrDefault();
            if (latestStore != null)
            {
                nextId = latestStore.Id + 1;
            }
            var newStore = new Stores
            {
                Name = data.StoreName,
                Password = data.Password,
                Id = nextId
            };

            //Add store to DB
            _context.Stores.Add(newStore);
            _context.SaveChanges();

            return newStore.Id.ToString();
        }

        // Έλεγχος ποιο κατάστημα κάνει Login
        [HttpGet("Authenticate/{username}/{password}")]
        public int Authenticate(string username, string password)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Name == username && s.Password == password);

            //αν δε βρεθεί κατάστημα, επέστρεψε "0" (για να εμφανιστεί κατάλληλο μήνυμα)
            if (store == null)
            {
                return 0;
            }

            return store.Id;
        }

        // επιστρέφει τα στοιχεία ενός καταστήματος (settings) ώστε να τα διαχειριστεί ο user
        [HttpGet("Info/{storeId}")]
        public StoreDTO Info(int storeId)
        {
            // φέρνει το κατάστημα απο τη βάση
            var store = _context.Stores.First(s => s.Id == storeId);
            //δημιουργεί το DTO (data transfer object)
            var storeDto = new StoreDTO
            {
                Id = store.Id,
                Name = store.Name
            };
            //προσθέτει στο DTO τα στοιχεία του καταστήματος
            if (store.AddressId != null)
            {
                var address = _context.Addresses.First(a => a.Id == store.AddressId);
                storeDto.Address = address.Street;
                storeDto.Lat = address.Latitude;
                storeDto.Long = address.Longtitude;
                storeDto.AddressNumber = address.Number;
                storeDto.TK = address.Tk;
                storeDto.City = address.City;
            }

            return storeDto;
        }

        // αποθηκεύει τα στοιχεία του καστήματος 
        [HttpPost("UpdateInfo")]
        public StoreDTO UpdateInfo(StoreDTO data)
        {
            //φέρνει το κατάστημα απο τη βάση
            var store = _context.Stores.First(s => s.Id == data.Id);
            //ενημερώνει τη βάση με αυτό πληκτρολογησε ο χρήστης
            store.Name = data.Name;

            //φέρνει απο τη βάση τη διεύθυνση, αλλιώς (αν δεν εχει) δημιουργεί νέο address
            Addresses address = null;
            if (store.AddressId != null)
            {
                address = _context.Addresses.First(a => a.Id == store.AddressId);
            }
            else
            {
                address = new Addresses();
                address.City = "";
                address.Number = "";
                address.Tk = "";
                var latestAddress = _context.Addresses.OrderByDescending(s => s.Id).FirstOrDefault();
                store.AddressId = latestAddress == null ? 1 : latestAddress.Id + 1;
                _context.Addresses.Add(address);
            }

            //γίνεται update η διεύθυνση με τα νέα στοιχεία που στέλνει ο client 
            address.Street = data.Address;
            address.Longtitude = data.Long;
            address.Latitude = data.Lat;

            address.Number = data.AddressNumber;
            address.Tk = data.TK;
            address.City = data.City;
            //σώζει στη βάση
            _context.SaveChanges();

            return Info(data.Id);
        }

        //Συνάρτηση (controller action) που φέρνει τις κατηγορίες του καταστήματος (αυτές που έχει ήδη επιλέξει)
        [HttpGet("Categories/{storeId}")]
        public IEnumerable<CategoryDTO> Categories(int storeId)
        {
            var storeCategories = _context.StoreToCategories
                .Where(s => s.StoreId == storeId)
                .Select(s => s.CategoryId)
                .ToArray();

            return _context.Categories
                .Where(m => storeCategories.Contains(m.Id))
                .Select(s => new CategoryDTO
                {
                    Id = s.Id,
                    ParentId = s.ParentId,
                    Name = s.Name
                });
        }

        //προσθέτει την επιλεγμένη κατηγορία στο store
        [HttpGet("Categories/Add/{categoryId}/{storeId}")]
        public IEnumerable<CategoryDTO> AddCategory(int categoryId, int storeId)
        {
            var categoryToAdd = new StoreToCategories();
            categoryToAdd.CategoryId = categoryId;
            categoryToAdd.StoreId = storeId;

            _context.StoreToCategories.Add(categoryToAdd);
            _context.SaveChanges();

            return Categories(storeId);
        }
        //αφαιρεί την επιλεγμένη κατηγορία απο το κατάστημα
        [HttpGet("Categories/Remove/{categoryId}/{storeId}")]
        public IEnumerable<CategoryDTO> RemoveCategory(int categoryId, int storeId)
        {
            var catToRemove = _context.StoreToCategories.First(c => c.CategoryId == categoryId && c.StoreId == storeId);

            if (catToRemove != null)
            {
                _context.StoreToCategories.Remove(catToRemove);
                _context.SaveChanges();
            }

            return Categories(storeId);
        }
        //φέρνει από τη βάση τα προϊόντα του συγκεκριμένου καταστήματος
        [HttpGet("Products/{storeId}")]
        public IEnumerable<MenuItemDTO> Products(int storeId)
        {
            return _context.MenuItems
                .Where(m => m.StoreId == storeId)
                .OrderBy(m => m.Category)
                .Select(s => new MenuItemDTO
                {
                    Id = s.Id,
                    StoreId = s.StoreId,
                    Title = s.Title,
                    CategoryId = int.Parse(s.Category),
                    Price = s.Price
                });
        }
        //προσθέτει προϊόν για το συγκεκριμένο κατάστημα
        [HttpPost("Products/Add")]
        public int AddProduct(MenuItemDTO product)
        {
            var productToAdd = new MenuItems();
            productToAdd.StoreId = product.StoreId;
            productToAdd.Title = product.Title;
            productToAdd.Category = product.CategoryId.ToString();
            productToAdd.Price = product.Price;
            _context.MenuItems.Add(productToAdd);
            _context.SaveChanges();
            return productToAdd.Id;
        }
        // φέρνει το προϊόν απο τη βάση και το αλλάζει 
        [HttpPost("Products/Update")]
        public void UpdateProduct(MenuItemDTO product)
        {
            var productToUpdate = _context.MenuItems.First(p => p.Id == product.Id);
            productToUpdate.StoreId = product.StoreId;
            productToUpdate.Title = product.Title;
            productToUpdate.Category = product.CategoryId.ToString();
            productToUpdate.Price = product.Price;
            _context.SaveChanges();
        }
        //αφαιρεί προϊόν απο το κατάστημα
        [HttpGet("Products/Remove/{productId}")]
        public void RemoveProduct(int productId)
        {
            var productToRemove = _context.MenuItems.First(p => p.Id == productId);
            _context.MenuItems.Remove(productToRemove);
            _context.SaveChanges();
        }
    }
}
