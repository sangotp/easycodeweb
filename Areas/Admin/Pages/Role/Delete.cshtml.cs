using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyCodeAcademy.Web.Areas.Admin.Pages.Role
{
    public class DeleteModel : RolePageModel
    {
        public DeleteModel(RoleManager<IdentityRole> roleManager, EasyCodeContext easyCodeContext) : base(roleManager, easyCodeContext)
        {
        }

        public IdentityRole role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string roleid)
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

            return Page();
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

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                StatusMessage = $"Deleted Role: {role.Name} Successfully";
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
