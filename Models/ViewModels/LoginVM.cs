using System.ComponentModel.DataAnnotations;

namespace emregayrımenkul.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adınızı giriniz.")] // bu hata yani giriş olmazsa bu hatayı döncek
        [StringLength(100)]//zorunluluk koşuyoruz
        [MaxLength(100)]//Gerekli olan max uzunluk
        public string? Username {  get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [StringLength(30)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string? Password { get; set; } // burada hatırlama işlemini gösteriyoruz.
        [Required(ErrorMessage ="Robot değilim")]
        [Display(Name ="Beni Hatırla")]
        public bool RememberMe { get; set; }



    }
}
