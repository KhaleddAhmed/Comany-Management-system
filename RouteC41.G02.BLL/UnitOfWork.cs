using RouteC41.G02.BLL.Interfaces;
using RouteC41.G02.BLL.Repositries;
using RouteC41.G02.DAL.Data;
using RouteC41.G02.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        //private Dictionary<string, IGenericRepository<ModelBase>> repositries;
        private Hashtable repositories;


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            repositories = new Hashtable();


        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;

            if(!repositories.ContainsKey(key))
            {

                if (key == nameof(Employee))
                {
                    var repository = new EmployeeRepository(dbContext);
                    repositories.Add(key, repository);
                }
                else
                {
                  var  repository = new GenericRepository<T>(dbContext);
                    repositories.Add(key, repository);

                }




            }

            return repositories[key] as IGenericRepository<T>;

           
        }

        public async Task<int> Complete()
        {
           return await dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();//close connection

        }




    }
}
