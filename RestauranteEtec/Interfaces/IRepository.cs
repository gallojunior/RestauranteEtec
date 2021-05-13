using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEtec.Interfaces
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void Add(T model);
        void Update(T model);
        void Delete(int? id);
    }
}
