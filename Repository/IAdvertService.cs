using System;
using emregayrımenkul.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace emregayrımenkul.Repository;
using Microsoft.EntityFrameworkCore;

public interface IAdvertService
{
    Task<ICollection<Advert>> GetAdvertListAsync(); // tüm ilanları getirir
    Task<Advert> GetAdvertByIdAsync(int id); // tek bir ilan döndür

    Task<bool> AddAdvertAsync(Advert advert,IFormFile imageFile); //burada eklemeye bakcaz 
    Task<bool> DeleteAdvertAsync(int id);//silme işlemine bakcaz
    Task<bool> UpdateAdvertAsync(Advert advert,IFormFile newImageFile); //güncellemeye bakcaz

    Task<int> GetTotalAdvertCountsAsync();//genel bilgileri alcaz
}
