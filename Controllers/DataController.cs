using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartOrder.API.DTOs;
using SmartOrder.API.Model;

// Controller για τη διαχείριση των δεδομένων 

namespace SmartOrder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {

        private readonly ILogger<DataController> _logger;
        private readonly DB_A25A8E_orderspapiContext _context;

        public DataController(ILogger<DataController> logger, DB_A25A8E_orderspapiContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Φέρνει απο τη βάση όλες τις κατηγορίες
        [HttpGet("Categories")]
        public IEnumerable<CategoryDTO> Categories()
        {
            return _context.Categories
                .Where(m => m.ParentId != null)
                .Select(s => new CategoryDTO
            {
                Id = s.Id,
                ParentId = s.ParentId,
                Name = s.Name
            });
        }
    }
}