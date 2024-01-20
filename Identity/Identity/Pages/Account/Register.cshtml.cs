using Identity.Contracts;
using Identity.Data.BO;
using Identity.PageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> m_userManager;
        IEmailService m_emailService;

        public RegisterModel(UserManager<User> userManager, IEmailService emailService)
        {
            m_userManager = userManager;
            m_emailService = emailService;
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
            var user = new User
            {
                Email = RegisterViewModel.Email,
                UserName = RegisterViewModel.Email,
                FirstName = RegisterViewModel.FirstName,
                LastName = RegisterViewModel.LastName,
            };

            var result = await m_userManager.CreateAsync(user, RegisterViewModel.Password);

            if (result.Succeeded)
            {
                Claim departmentClaim = new Claim("Department", RegisterViewModel.Department);
                Claim designationClaim = new Claim("Designation", RegisterViewModel.Designation);

                await m_userManager.AddClaimAsync(user, departmentClaim);
                await m_userManager.AddClaimAsync(user, designationClaim);

                var confirmationToken = await m_userManager.GenerateEmailConfirmationTokenAsync(user);

                string stConfirmationLink = Url.PageLink(pageName: "/Account/ConfirmEmail",
                     values: new { userId = user.Id, token = confirmationToken }) ?? "";

                await m_emailService.SendAsync(user.Email, "Please confirm your email", $"please click on this link: {stConfirmationLink}");

                return RedirectToPage("/Account/Login");

                //////////////////////////////////////////////////////////////////
                // We can direct redirect without email confirmation using below code
                //////////////////////////////////////////////////////////////////
               
                //return RedirectToPage(Url.PageLink(pageName: "/Account/ConfirmEmail",
                //    values: new { userId = user.Id, token = confirmationToken }) ?? "");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    if (error.Description.Contains("Username"))
                        continue;

                    ModelState.AddModelError("Register", error.Description);
                }

                return Page();
            }
        }
    }
}
