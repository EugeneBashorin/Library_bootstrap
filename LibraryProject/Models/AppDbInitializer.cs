using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LibraryProject.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        private const string _USER_ROLE = "user";
        private const string _ADMIN_ROLE = "admin";
        private const string _ADMIN_EMAIL = "qwe@qwe.qwe";
        private const string _ADMIN_PASSWORD = "1_Qwerty";


        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = _ADMIN_ROLE };
            var role2 = new IdentityRole { Name = _USER_ROLE };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = _ADMIN_EMAIL, UserName = _ADMIN_EMAIL };
            string password = _ADMIN_PASSWORD;
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
