using LibraryProject.Configurations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LibraryProject.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
       


        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = ConfigurationData._ADMIN_ROLE };
            var role2 = new IdentityRole { Name = ConfigurationData._USER_ROLE };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = ConfigurationData._ADMIN_EMAIL, UserName = ConfigurationData._ADMIN_EMAIL };
            string password = ConfigurationData._ADMIN_PASSWORD;
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }
            base.Seed(context);
        }
    }
}
