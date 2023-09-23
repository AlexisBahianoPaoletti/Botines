using Botines.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Botines.Datos;

namespace Botines.Web.Helpers
{
    public class UsersHelper : IDisposable
    {
        private static readonly ApplicationDbContext UserContext = new ApplicationDbContext();
        private static readonly BotinesDbContext Db = new BotinesDbContext();

        public static void CheckRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(UserContext));

            // Check to see if Role Exists, if not create it
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        public static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassWord"];
            var userAsp = userManager.FindByName(email);
            if (userAsp == null)
            {
                CreateUserAsp(email, "Admin", password);
                return;
            }

            userManager.AddToRole(userAsp.Id, "Admin");
        }
        public static void CreateUserAsp(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));

            var userAsp = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userAsp, email);
            userManager.AddToRole(userAsp.Id, roleName);
        }

        public static void CreateUserAsp(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));

            var userAsp = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userAsp, password);
            userManager.AddToRole(userAsp.Id, roleName);
        }

        public static async Task PasswordRecovery(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));
            var userAsp = userManager.FindByEmail(email);
            if (userAsp == null)
            {
                return;
            }

            var user = Db.Clientes.FirstOrDefault(tp => tp.CorreoElectronico == email);
            if (user == null)
            {
                return;
            }

            var random = new Random();
            var newPassword = string.Format("{0}{1}{2:04}*",
                user.Nombre.Trim().ToUpper().Substring(0, 1),
                user.Direccion.Trim().ToLower(),
                random.Next(10000));

            userManager.RemovePassword(userAsp.Id);
            userManager.AddPassword(userAsp.Id, newPassword);

            var subject = "ECommerce Password Recovery";
            var body = string.Format(@"
                <h1>ECommerce Password Recovery</h1>
                <p>Yor new password is: <strong>{0}</strong></p>
                <p>Please change it for one, that you remember easyly",
                newPassword);

            await MailHelper.SendMail(email, subject, body);
        }
        public static bool UpdateUserName(string currentUserName, string newUserName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));
            var userAsp = userManager.FindByEmail(currentUserName);
            if (userAsp == null)
            {
                return false;
            }

            userAsp.UserName = newUserName;
            userAsp.Email = newUserName;
            var response = userManager.Update(userAsp);
            return true;
        }

        public void Dispose()
        {
            UserContext.Dispose();
            Db.Dispose();
        }
    }
}