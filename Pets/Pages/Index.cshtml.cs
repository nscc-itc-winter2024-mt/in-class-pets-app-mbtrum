using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pets.Data;
using Pets.Models;

namespace Pets.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Pet> pets = new();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;            
        }

        public void OnGet()
        {
            pets = DbContext.GetAllPets();
        }
    }
}
