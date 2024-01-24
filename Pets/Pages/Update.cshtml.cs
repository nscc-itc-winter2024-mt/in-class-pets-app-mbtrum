using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pets.Data;
using Pets.Models;
using System.ComponentModel;

namespace Pets.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly ILogger<UpdateModel> _logger;
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public Pet Pet { get; set; } = new();

        [BindProperty]
        [DisplayName("Change Photo")]
        public IFormFile? FileUpload { get; set; }

        public UpdateModel(ILogger<UpdateModel> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnGet(int id)
        {
            // Initialize pet
            Pet = DbContext.GetPetById(id);
        }

        public IActionResult OnPost()
        {
            //
            // Validate the input
            //
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Change photo
            if(FileUpload != null)
            {
                // Upload file to server

                string filename = FileUpload.FileName;

                // Update Pet object to include the photo filename
                Pet.PhotoFileName = filename;

                // Save the file
                string projectRootPath = _environment.ContentRootPath;
                string fileSavePath = Path.Combine(projectRootPath, "wwwroot\\uploads", filename);

                // We use a "using" to ensure the filestream is disposed of when we're done with it
                using (FileStream fileStream = new FileStream(fileSavePath, FileMode.Create))
                {
                    FileUpload.CopyTo(fileStream);
                }
            }

            // Save to database

            DbContext.UpdatePet(Pet);

            return RedirectToPage("Index");
        }
    }
}
