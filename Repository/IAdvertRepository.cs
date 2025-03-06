using System;
using emregayrımenkul.Data;
using emregayrımenkul.Models;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace emregayrımenkul.Repository;

public interface IAdvertRepository
{

    Task<ICollection<Advert>> GetAllAdvertAsync();  //tüm ilanları getirir
    Task<Advert> GetAdvertByIdAsync(int id); // ilan ıdsine göre getirir
    Task AddAdvertAsync(Advert advert,IFormFile imageFile);  //resim ve ilan tüm özellikleri 
    Task DeleteAdvertAsync(int id); //ilan siler
    Task UpdateAdvertAsync(Advert advert,IFormFile imageFile); //ilan günceller.
    Task<int> GetTotalAdvertAsync();//toplam ilan bilgisini alcaz
    
 


}
