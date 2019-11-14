using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestApp.BLL.Services.Inteface;
using TestApp.DAL.Models;

namespace TestApp.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string sort, int itemsPerPage = 10, int page = 0)
        {
            var result = await _userService.GetAllAsync(sort, itemsPerPage, page);

            return SuccessPagination(result.Total, result.DataResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var newUser = await _userService.CreateUserAsync(user);

            return Success(newUser);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(user);

                return Success(updatedUser);
            }
            catch (Exception exc)
            {
                return Error(System.Net.HttpStatusCode.NotFound, exc.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);

                return Success(id);
            }
            catch (Exception exc)
            {
                return Error(System.Net.HttpStatusCode.NotFound, exc.Message);
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Get(long Id)
        {
            try
            {
                var user = await _userService.GetUserAsync(Id);

                return Success(user);
            }
            catch (Exception exc)
            {
                return Error(System.Net.HttpStatusCode.NotFound, exc.Message);
            }
        }
    }
}