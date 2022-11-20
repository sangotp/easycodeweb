using Bogus.DataSets;
using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EasyCodeAcademy.Web.Areas.Admin.Pages.Role
{
    public class EditModel : RolePageModel
    {
        public EditModel(RoleManager<IdentityRole> roleManager, EasyCodeContext easyCodeContext) : base(roleManager, easyCodeContext)
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

        public IdentityRole role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string roleid)
        {
            if(roleid == null)
            {
                return NotFound("Cannot Find Role");
            }

            role = await _roleManager.FindByIdAsync(roleid);

            if(role != null)
            {
                Input = new InputModel()
                {
                    Name = role.Name
                };

                return Page();
            }

            return NotFound("Cannot Find Role");
        }

        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (roleid == null)
            {
                return NotFound("Cannot Find Role");
            }

            role = await _roleManager.FindByIdAsync(roleid);

            if (role == null)
            {
                return NotFound("Cannot Find Role");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            role.Name = Input.Name;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                StatusMessage = $"Updated Role: {Input.Name} Successfully";
                return RedirectToPage("./Index");
            }
            else
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
