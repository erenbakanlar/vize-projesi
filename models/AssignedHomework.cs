using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace odev.dagitim.portali.models
{
    [Table("DagitilanOdevler")]
    public class AssignedHomework
    {
        public int Id { get; set; }

        [Column("Baslik")]
        public string? Title { get; set; }

        [Column("Aciklama")]
        public string? Description { get; set; }

        [Column("SonTeslimTarihi")]
        public DateTime DueDate { get; set; }
    }
}
