using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TestWebSite.Database
{
    public class Database
    {
        public async static Task Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var db = serviceScope.ServiceProvider.GetService<Context>();

            //migration here
            try
            {
                if (!db.Database.EnsureCreated()) 
                    db.Database.Migrate();
            }
            catch (Exception e)
            {
            }

            var admin = await db.Users.FirstOrDefaultAsync(x => x.Username == "admin");
            if (admin == null)
            {
                db.Users.Add(new Models.User
                {
                    Username = "admin",
                    Password = "111"
                });

                await db.SaveChangesAsync();
            }
        }
    }
}
