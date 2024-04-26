using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteC41.G02.DAL.Models;
using RouteC41.G02.PL.ViewModels.User;
using System.Threading.Tasks;

namespace RouteC41.G02.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager) 
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

        #region Sign Up
        public IActionResult SignUp() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
					user = new ApplicationUser()
					{
						FName = model.FName,
						LName = model.LName,
						UserName = model.UserName,
						Email = model.Email,
						IsAgree = model.IsAgree

					};

                  var result=  await userManager.CreateAsync(user,model.Password);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(SignIn));
                
                    foreach(var error in  result.Errors)
                        ModelState.AddModelError(string.Empty,error.Description);

                }

                ModelState.AddModelError(string.Empty, "This User Name is Already used before");

            }
            return View(model);
        }
        #endregion

        #region Sign In
        public IActionResult SignIn()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var flag = await userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.IsLockedOut)
						{
							ModelState.AddModelError(string.Empty, "Your account is locked");
						}
						if (result.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index), "Home");
						}
						if (result.IsNotAllowed)
						{
							ModelState.AddModelError(string.Empty, "Your account is not confirmed yet"); ;
						}

					}
				}
				ModelState.AddModelError(string.Empty, "Invalid login");
			}
			return View(model);

		}

		#endregion
	}
	public async new Task<IActionResult> SignOut()
	{
		await signInManager.SignOutAsync();
		return RedirectToAction(nameof(SignIn));
	}
}
}
