using System.Diagnostics;
using System.Threading.Tasks;
using emregayrımenkul.Models;
using emregayrımenkul.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emregayrımenkul.Controllers
{   
    public class HomeController : Controller
    {   

        private readonly IAdvertService _advertService;

        public HomeController(IAdvertService advertService)
        {
            _advertService = advertService;
        }


        //HomePages
        public IActionResult Index()
        {
            

            return View();
        }

        //About Page
        public IActionResult About()
        {
            return View();
        }


        //İlanlar
       [HttpGet]
        public async Task<IActionResult> AllDetails(string property, string city, string district, int page = 1)
        {
            int pageSize = 6; // Her sayfada gösterilecek ilan sayısı
            
            var query = await _advertService.GetAdvertListAsync();

            // Filtreleme işlemleri
            if (!string.IsNullOrEmpty(property))
            {
                query = query.Where(y => y.Property == property).ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(y => y.City == city).ToList();
            }

            if (!string.IsNullOrEmpty(district))
            {
                query = query.Where(y => y.District == district).ToList();
            }

            // Pagination işlemleri
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            
            var paginatedQuery = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (!paginatedQuery.Any())
            {
                ViewBag.message = "İlan bulunamadı";
            }

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Property = property;
            ViewBag.City = city;
            ViewBag.District = district;

            return View(paginatedQuery);
        }
        
        //Contact Page
        public IActionResult Contact()
        {   
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id){

            var result  = await _advertService.GetAdvertByIdAsync(id);

            if(result ==null){
                return NotFound();
            }
            

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }


  


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
