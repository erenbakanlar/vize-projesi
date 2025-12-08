using odev.dagitim.portali.models;
using System.Collections.Generic;

namespace odev.dagitim.portali.repositories
{
    public interface IDagitilanOdevRepository
    {
        List<DagitilanOdev> GetAll();
        void Add(DagitilanOdev odev);
    }
}
