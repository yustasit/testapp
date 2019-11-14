using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestApp.BLL.DTO;
using TestApp.BLL.Extensitons;
using TestApp.BLL.Services.Inteface;
using TestApp.DAL.Models;
using TestApp.DAL.Repository;

namespace TestApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, long> _repository;
        public UserService(
            IRepository<User, long> repository)
        {
            _repository = repository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _repository.CreateAsync(user);

            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existedUser = await _repository.GetByIdAsync(user.Id);

            if (existedUser == null)
                throw new Exception($"user with id={user.Id} not found");

            existedUser.EMail = user.EMail;
            existedUser.FirstName = user.FirstName;
            existedUser.LastName = user.LastName;
            existedUser.Password = user.Password;
            existedUser.Username = user.Username;
            existedUser.Phone = user.Phone;

            return await _repository.UpdateAsync(existedUser);
        }

        public async Task DeleteUserAsync(long id)
        {
            var existedUser = await _repository.GetByIdAsync(id);

            if (existedUser == null)
                throw new Exception($"user with id={id} not found");

            await _repository.DeleteAsync(existedUser);
        }

        public async Task<PagedData<User>> GetAllAsync(string sort, int take = 10, int index = 0)
        {
            string sortField = "Id";
            bool isDesc = false;

            if (!string.IsNullOrEmpty(sort))
            {
                string[] sortValues = sort.Split(new char[] { ',' });
                sortField = sortValues[0];
                isDesc = sortValues[1] == "desc";
            }

            int total = 0;
            var result = _repository.GetQueryable(out total, calculateCount: true);

            if (isDesc)
            {
                result = result.OrderByDescending(sortField);
            }
            else
            {
                result = result.OrderBy(sortField);
            }
            var skip = index > 0 ? index * take : 0;
            result = result.Skip(skip).Take(take);

            var resultViewModel = new PagedData<User>
            {
                Total = total,
                DataResult = await result.ToListAsync()
            };

            return resultViewModel;
        }

        public async Task<User> GetUserAsync(long userId)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (user == null)
                throw new Exception($"user with id={userId} not found");

            return user;
        }
    }
}

