using $ext_safeprojectname$.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.BLL.Contracts
{
    public interface ITodosMockProxyService
    {
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> GetTodoById(int id);
    }
}
