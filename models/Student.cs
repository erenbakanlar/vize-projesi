using System.ComponentModel.DataAnnotations.Schema;

namespace odev.dagitim.portali.models
{
    [Table("Ogrenciler")]
    public class Student
    {
        public int Id { get; set; }
        
        [Column("AdSoyad")]
        public string? FullName { get; set; }
        
        [Column("Numara")]
        public string? Email { get; set; }
        
        [Column("Sifre")]
        public string? Password { get; set; }
    }
}
