using System.ComponentModel.DataAnnotations;


namespace emregayrımenkul.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Lütfen isim giriniz.")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Lütfen email giriniz.")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir email giriniz.")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Lütfen parola giriniz.")]
       
        [StringLength(50,ErrorMessage ="Parola en fazla 50 karakterden oluşmalı.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password",ErrorMessage ="Şifre  eşleşmiyor.")]
        [Required(ErrorMessage ="Lütfen şifreyi tekrar giriniz.")]
        public string? ConfirmPassword { get; set; }

    }
}


