using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.DAL.Models
{
    public partial class User
	{
		public User()
		{
		}

		public long Id { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string EMail { get; set; }

		public string Phone { get; set; }

		[NotMapped]
		public string FullName { get { return $"{FirstName} {LastName}"; } }

	}
}
