
using System.Collections.Generic;
using System.Linq;

namespace genshinwebsite.Services
{
    public interface IRepository<T> where T: class // 泛型约束
    {
        IEnumerable<T> Get_all();

        public T Get_by_id(int id);

        public T Add(T new_model);

        public T Delete(T old_model);
        public T Update(int id, T new_model);

        public T Find_by_account(string account);
    }
}
