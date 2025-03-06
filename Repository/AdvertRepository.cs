using emregayrımenkul.Data;
using emregayrımenkul.Models;
using Microsoft.EntityFrameworkCore;  
using System.Collections.Generic;
using System.Threading.Tasks;


namespace emregayrımenkul.Repository;

public class AdvertRepository :IAdvertRepository  // burada extends ettik yazdıgımız interface göre methodları doldurduk
{
    private readonly ApplicationDbContext _context; //burada yani repoyu db ye bagladık ve işlemleri yapcaz

    public AdvertRepository(ApplicationDbContext context) //burada constructora aldık
    {
        _context=context;
    }


    public async Task<ICollection<Advert>> GetAllAdvertAsync()
    {
        return await _context.Advert.ToListAsync(); //tüm ilanları getirir
    }


     
    public async Task<Advert> GetAdvertByIdAsync(int id)
    {
        return await _context.Advert.FirstOrDefaultAsync(y=>y.Id == id); //ilanı idye göre getirir
        
    }

    public async Task DeleteAdvertAsync(int id){
        var advert = await _context.Advert.FindAsync(id);
        if (advert == null)
        {
            throw new KeyNotFoundException("İlan bulunamadı.");
        }

        
        _context.Advert.Remove(advert);
        await _context.SaveChangesAsync();
    }

   


    // İlan Ekleme (Resim Dahil)
    public async Task AddAdvertAsync(Advert advert, IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            string uniqueFileName = null;

            // Eğer resim seçildiyse, sunucuya kaydet
            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder); // Eğer klasör yoksa oluştur

                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Resim dosyasının yolunu ayarla
                advert.UrlImage = "/images/" + uniqueFileName;
            } else
            {
                advert.UrlImage = "/images/default.png"; // Default resim
            }
        }

        await _context.Advert.AddAsync(advert);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAdvertAsync(Advert advert, IFormFile imageFile)
{
    var existingAdvert = await _context.Advert.FindAsync(advert.Id);
    if (existingAdvert != null)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(existingAdvert.UrlImage))
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingAdvert.UrlImage.TrimStart('/'));
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            existingAdvert.UrlImage = "/images/" + uniqueFileName;
        }

        existingAdvert.AdvertTitle = advert.AdvertTitle;
        existingAdvert.Explain = advert.Explain;
        existingAdvert.City = advert.City;
        existingAdvert.District = advert.District;
        existingAdvert.Cost = advert.Cost;
        existingAdvert.Property = advert.Property;
        existingAdvert.NetArea = advert.NetArea;
        existingAdvert.RoomCount = advert.RoomCount;
        existingAdvert.BuildingAge = advert.BuildingAge;
        existingAdvert.FloorNumber = advert.FloorNumber;
        existingAdvert.TotalFloors = advert.TotalFloors;
        existingAdvert.heating = advert.heating;
        existingAdvert.BathroomCount = advert.BathroomCount;
        existingAdvert.Kitchen = advert.Kitchen;
        existingAdvert.Balcony = advert.Balcony;
        existingAdvert.Elevator = advert.Elevator;
        existingAdvert.Parking = advert.Parking;

        _context.Advert.Update(existingAdvert);
        await _context.SaveChangesAsync();
    }
}

    public async Task<int> GetTotalAdvertAsync()
    {
        return await _context.Advert.CountAsync();//toplam kaç tane eleman oldugunu söyler;
    }
}
