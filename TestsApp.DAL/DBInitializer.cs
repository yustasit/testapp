using System;
using System.Linq;
using TestApp.DAL.Models;

namespace TestApp.DAL
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            try
            {
                bool connected = context.Database.EnsureCreated();
                if (!connected)
                {
                    return;
                }

                if (context.Users.Any())
                {
                    return;
                }

                InitUsers(context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void InitUsers(ApplicationContext context)
        {
            for (int i = 1; i < 100; i++)
            {
                context.Users.Add(new User()
                {
                    Username = $"Login {i}",
                    FirstName = $"Name {i}",
                    LastName = $"Last Name {i}",
                    EMail = $"email{i}@.com",
                    Password = "Pass",
                    Phone = "000-000"
                });
            }

            context.SaveChanges();
        }
    }
}
