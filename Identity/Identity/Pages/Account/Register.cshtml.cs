using Identity.PageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> m_userManager;
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            m_userManager = userManager;
        }

        [BindProperty]
        public UserRegistrationViewModel RegisterViewModel { get; set; } = new UserRegistrationViewModel();


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            //Create the User
            var user = new IdentityUser
            {
                Email = RegisterViewModel.Email,
                UserName = RegisterViewModel.Email,
            };

            var result = await m_userManager.CreateAsync(user, RegisterViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToPage("/Account/Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return Page();
            }
        }
    }
}
