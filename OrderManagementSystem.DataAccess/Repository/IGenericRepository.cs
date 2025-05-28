using OrderManagementSystem.Entity;
using System.Linq.Expressions;

namespace OrderManagementSystem.DataAccess.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Insert(T entity);
        void Delete(T entity);
        List<T> GetAll();
        T GetById(int id);
        List<T> GetByFilter(Expression<Func<T, bool>> predicate);
    }
}
