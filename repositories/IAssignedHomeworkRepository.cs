using odev.dagitim.portali.models;
using System.Collections.Generic;

namespace odev.dagitim.portali.repositories
{
    public interface IAssignedHomeworkRepository
    {
        List<AssignedHomework> GetAll();
        void Add(AssignedHomework odev);
        void Delete(int id);
    }
}
