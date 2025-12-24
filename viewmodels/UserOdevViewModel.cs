using odev.dagitim.portali.models;
using System.Collections.Generic;

namespace odev.dagitim.portali.viewmodels
{
    public class UserOdevViewModel
    {
        public AssignedHomework DagitilanOdev { get; set; }
        public Homework YuklenenOdev { get; set; } // henuz yuklenmemisse null
        
        // Convenience properties for view
        public int DagitilanOdevId => DagitilanOdev?.Id ?? 0;
        public string Baslik => DagitilanOdev?.Title ?? "";
        public string Aciklama => DagitilanOdev?.Description ?? "";
        public DateTime SonTarih => DagitilanOdev?.DueDate ?? DateTime.MinValue;
        public bool Yuklendi => YuklenenOdev != null;
        public bool YuklendiMi => YuklenenOdev != null;
    }
}
