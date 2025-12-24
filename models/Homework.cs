using System.ComponentModel.DataAnnotations.Schema;

namespace odev.dagitim.portali.models
{
    [Table("Odevler")]  // Database tablo adı
    public class Homework
    {
        public int Id { get; set; }
        
        [Column("OgrenciId")]
        public int StudentId { get; set; }
        
        [Column("DagitilanOdevId")]
        public int AssignedHomeworkId { get; set; }
        
        [Column("DosyaYolu")]
        public string? FilePath { get; set; }
        
        [Column("YuklemeTarihi")]
        public DateTime UploadDate { get; set; }

        // Navigation properties
        public Student? Student { get; set; }
        public AssignedHomework? AssignedHomework { get; set; }
    }
}
