using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EasyCodeAcademy.Web.Areas.Admin.Pages.Role
{
    public class CreateModel : RolePageModel
    {
        public CreateModel(RoleManager<IdentityRole> roleManager, EasyCodeContext easyCodeContext) : base(roleManager, easyCodeContext)
        {
        }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} is required")]
            [Display(Name = "Role Name")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} required {2} - {1} characters")]
            public string? Name { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var newRole = new IdentityRole(Input.Name);

            var result = await _roleManager.CreateAsync(newRole);

            if(result.Succeeded)
            {
                StatusMessage = $"Create Role: {Input.Name} Successfully";
                return RedirectToPage("./Index");
            } else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }

            return Page();
        }
    }
}
