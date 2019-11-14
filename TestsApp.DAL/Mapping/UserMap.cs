using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.DAL.Models;

namespace TestApp.DAL
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Username).HasMaxLength(100);
            tb.Property(o => o.Password).HasMaxLength(100);
            tb.Property(o => o.FirstName).HasMaxLength(100);
            tb.Property(o => o.LastName).HasMaxLength(100);
		} 
    }
}
