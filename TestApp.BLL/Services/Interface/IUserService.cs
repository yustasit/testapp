using System.Threading.Tasks;
using TestApp.BLL.DTO;
using TestApp.DAL.Models;

namespace TestApp.BLL.Services.Inteface
{
    public interface IUserService
    {
        Task<PagedData<User>> GetAllAsync(string sort, int take = 10, int index = 0);

        Task<User> GetUserAsync(long userId);

        Task<User> CreateUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task DeleteUserAsync(long id);
    }
}
