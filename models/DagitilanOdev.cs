using System;

namespace odev.dagitim.portali.models
{
    public class DagitilanOdev
    {
        public int Id { get; set; }

        public string Baslik { get; set; }

        public string Aciklama { get; set; }

        public DateTime SonTeslimTarihi { get; set; }
    }
}
