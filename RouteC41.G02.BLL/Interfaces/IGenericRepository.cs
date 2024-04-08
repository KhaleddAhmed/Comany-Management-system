using RouteC41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}
