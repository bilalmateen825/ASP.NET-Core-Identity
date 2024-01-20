using Identity.Data.BO;
using Identity.PageViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> m_signInManager;

        public LoginModel(SignInManager<User> signInManager)
        {
            m_signInManager = signInManager;
        }

        [BindProperty]
        public CredentialViewModel Credential { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page(); //Error will be displayed on Form

            var result = await m_signInManager.PasswordSignInAsync(
                 this.Credential.Email,
                 this.Credential.Password,
                 this.Credential.RememberMe,
                 false);

            if(result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                if(result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out.");
                }
                else
                {
                    ModelState.AddModelError("Login", "Failed to login.");
                }
            }

            return Page();
        }
    }
}