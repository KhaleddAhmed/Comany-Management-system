using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.BLL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }

        public IDepartmentReposotry DepartmentRepository { get; set; }

        int Complete();
    }
}
