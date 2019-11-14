using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TestApp.API.Controllers
{
    [ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class BaseController : Controller
	{
		public BaseController()
		{

		}

		protected IActionResult Success(object response = null) => Json(new
		{
			StatusCode = HttpStatusCode.OK,
			Data = response
		});

		protected IActionResult SuccessPagination(int count, object response = null) => Json(new
		{
			StatusCode = HttpStatusCode.OK,
			Data = new
			{
				response,
				count
			}
		});

		protected IActionResult Error(HttpStatusCode statusCode, string errorMessage = null) => Json(new
		{
			StatusCode = statusCode,
			Message = errorMessage
		});
	}
}
