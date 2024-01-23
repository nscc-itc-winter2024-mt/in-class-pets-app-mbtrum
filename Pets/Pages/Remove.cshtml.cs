using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pets.Data;
using Pets.Models;

namespace Pets.Pages
{
    public class RemoveModel : PageModel
    {
        [BindProperty]
        public Pet Pet { get; set; } = new();

        public void OnGet(int id)
        {
            // Initialize pet
            Pet = DbContext.GetPetById(id);
        }

        public IActionResult OnPost() 
        {
            DbContext.RemovePet(Pet.PetId);

            return RedirectToPage("Index");
        }

    }
}
