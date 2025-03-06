using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using emregayrımenkul.Models;
using emregayrımenkul.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace emregayrımenkul.Controllers
{   
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IAdvertService _advertService;  // buradan advertleri çektim servisleri kullanıcaz gnelde intreface baglanıyor

        public AdminController(IAdvertService advertService)
        {
            _advertService = advertService;

        }


        [HttpGet]
        public async Task<IActionResult> Index(){

            var totalResult = await _advertService.GetTotalAdvertCountsAsync();
            
            ViewBag.Total = totalResult;
            return View();


            
        }



        [HttpGet]
        public async Task<ActionResult> Dashboard()
        {
            var advert = await _advertService.GetAdvertListAsync();
            return View(advert);    // tüm ilanları getir 
        }

        [HttpGet]
        public IActionResult CreateAdvert()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdvert(Advert model, IFormFile imageFile)
        {
           
            if(ModelState.IsValid){
                var result = await _advertService.AddAdvertAsync(model, imageFile);
            if (result)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ModelState.AddModelError("" , "İlan eklerken hata oluştu.");
            }

            }

            return View(model);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdvert(int id){

            var  result = await _advertService.GetAdvertByIdAsync(id);

            if(result == null){
                
                return  NotFound(); //404 sayfasına yönlendirir
            }

            await _advertService.DeleteAdvertAsync(id);


            return RedirectToAction("Dashboard","Admin");
        }


        [HttpGet]
        public async Task<IActionResult>EditAdvert(int id){

            var advert = await _advertService.GetAdvertByIdAsync(id);
            if(advert ==null){
                return NotFound();// yani 404 sayfasına yönlendirir.
            }

            return View(advert);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>EditAdvert(Advert model, int id ,IFormFile newImageFile){

            ModelState.Remove("newImageFile");

            if(!ModelState.IsValid){
                return View(model);
            }

            
            model.Id=id; // direk atayarak hata riskini azaltmak maksat


            var results = await _advertService.UpdateAdvertAsync(model,newImageFile);//repo ve servisteki işlemler için gerekli

            if(results){

                TempData["SuccesMessage"] = "İlan başarı ile güncellendi";
                 return RedirectToAction("Dashboard","Admin");
            }else{

                ModelState.AddModelError("","Güncelleme sırasına bir hata meydana geldi");
                return View(model);
            }

           


            



        }












    }
 }