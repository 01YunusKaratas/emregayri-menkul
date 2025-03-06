using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emregayrımenkul.Models
{
    public class Advert
    {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string AdvertTitle { get; set; } // ilan Başlıgı 

        [Required, MaxLength(1000)] // Burayı değiştirmek lazım uzunluk olarka
        public string Explain { get; set; }// İlan açıklama

        [Required]
        public string City { get; set; }//il
        [Required]
        public string District{ get; set; }//ilçe
        
        public string? UrlImage { get; set; } // resim2 opsiyone

        [Required]
        public int Cost { get; set; }

        public string? Property { get; set; } // Emlak Tipi - For Sale, For Rent, etc.

        public int? NetArea { get; set; } // m² (Net)

        public string? RoomCount { get; set; } // Example: 3+1

        public string? BuildingAge { get; set; } // 21-25 years, etc.

        public int? FloorNumber { get; set; } // Floor number

        public int? TotalFloors { get; set; } // Total number of floors in the building

        public string? heating { get; set; } // Central Heating (Natural Gas), etc.

        public int? BathroomCount { get; set; }

        public  string? Kitchen { get; set; } // Closed, Open

        public string? Balcony { get; set; } // Yes, No

        public string? Elevator { get; set; } // Yes, No

        public string? Parking { get; set; } // Yes, No
        
    }
}
