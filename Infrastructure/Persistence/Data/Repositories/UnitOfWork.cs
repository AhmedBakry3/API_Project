using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data.DbContexts.StoredDbContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories
{
    public class UnitOfWork(StoredDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositories=[];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Get Type Name
            var TypeName = typeof(TEntity).Name;

            //Dict<string,object> ==> String Key [Name Of Type] --- Object [Object from Generic Repository] 
            //if (_Repositories.ContainsKey(TypeName))
            //    return (IGenericRepository<TEntity, TKey>)_Repositories[TypeName];
            if (_Repositories.TryGetValue(TypeName , out object? Value))
                return (IGenericRepository<TEntity, TKey>)Value;
            else
            {
                //Create Object
                var Repo = new GenericRepository<TEntity,TKey>(_dbContext);
                //Store object in Dict
                _Repositories["TypeName"] = Repo;
                //Return Object
                return Repo;
            }
        }

        public async Task<int> saveChanges() => await _dbContext.SaveChangesAsync();
    }
}
