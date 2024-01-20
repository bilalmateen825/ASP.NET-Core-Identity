using Identity.Data.BO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> m_userManager;

        [BindProperty]
        public string Message { get; set; }

        public ConfirmEmailModel(UserManager<User> userManager)
        {
            m_userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string token)
        {
            var user = await m_userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await m_userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    this.Message = "Email address is sucessfully confirmed, you can now try to login.";
                    return Page();
                }
            }

            this.Message = "Failed to validate email";
            return Page();
        }
    }
}
