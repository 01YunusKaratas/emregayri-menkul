using System.Threading.Tasks;
using emregayrımenkul.Models;
using emregayrımenkul.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace emregayrımenkul.Controllers
{   
    public class AccountController : Controller
    {   
        private readonly SignInManager<AppUser> _signInManager; // sonradan ekledik

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager; // sonradan ekledik
        public AccountController(SignInManager<AppUser> signInManager ,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager=userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        //kayıt sayfası
        [AllowAnonymous]
        [HttpGet]
        [Route("panel")]
        public ActionResult Login(){

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("panel")] 
        public async Task<IActionResult> Login(LoginVM model){

            if(ModelState.IsValid){
                var result = await _signInManager.PasswordSignInAsync(model.Username,model.Password,model.RememberMe,false);//burada gerekli işlemleri control ettik
                
                if(result.Succeeded){
                    
                    return RedirectToAction("Index","Admin");
                }else{
                   ModelState.AddModelError("","Geçersiz giriş");
                   return View(model);
                }
            }


            return View(model);
        }

       
        // GET: AccountController
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Register()
        {   

            return View();
        }
       
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterVM model){

             if (ModelState.IsValid)
        {
            AppUser user = new()
            {
                UserName = model.Name,
                Email = model.Email,
            };

            var hashing = await _userManager.CreateAsync(user, model.Password!);

            if (hashing.Succeeded)
            {
                // Admin rolünü kontrol et, yoksa oluştur
                var roleExist = await _roleManager.RoleExistsAsync("Admin");
                if (!roleExist)

                {
                    // Admin rolü yoksa oluştur
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Admin rolü oluşturulamadı.");
                        return View(model);
                    }
                }

                // Kullanıcıyı Admin rolüne ekle
                await _userManager.AddToRoleAsync(user, "Admin");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login", "Admin");
            }

            foreach (var error in hashing.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }
        
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult>Logout(){

            await _signInManager.SignOutAsync();//çıkış yapıyor burada
            return RedirectToAction("Index","Home");
        }


        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

    }
}
