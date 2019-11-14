using System.Collections.Generic;

namespace TestApp.BLL.DTO
{
	public class PagedData<T>
	{
		public int Total { get; set; }

		public int? PageIndex { get; set; }

		public List<T> DataResult { get; set; }
	}
}
