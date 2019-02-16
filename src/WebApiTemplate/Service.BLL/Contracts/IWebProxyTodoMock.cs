using Service.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.BLL.Contracts
{
    public interface ITodosMockProxyService
    {
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> GetTodoById(int id);
    }
}
