using System;
using System.Linq;
using bbom.Admin.Core;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bbom.Admin.Test.Tools
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var users = DataFasade.GetRepository<AspNetUser>().GetAll();
            foreach (var user in users)
            {
                Console.WriteLine("insert into RegisterInfo (AspNetUserId, Date) values ('{0}', '{1}')", user.Id, DateTime.Now.ToString("G"));
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var user = DataFasade.GetRepository<AspNetUser>().GetById("9e547fdd-460e-4262-b25a-eb7fbc2aeee5");
            var users = CoreFasade.UsersHelper.GetAllChildren(user, 1);
            foreach (var netUser in users)
            {
                Console.WriteLine(netUser.UserName);
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var users = DataFasade.GetRepository<AspNetUser>().GetAll();
            foreach (var netUser in users)
            {
                if (netUser.AspNetRoles.Any(role => role.Name == UserRole.NotWatch))
                {
                    Console.WriteLine("delete from AspNetUserRoles where UserId = {0} and RoleId = 18eaf7ba-e46b-40b7-9ec5-e5574ad908be", netUser.Id, "18eaf7ba-e46b-40b7-9ec5-e5574ad908be");
                }
            }
        }

        [TestMethod]
        public void TestMethod4()
        {
            var users = DataFasade.GetRepository<AspNetUser>().GetAll();
            foreach (var netUser in users)
            {
                Console.WriteLine("localhost {0}.example.com\n", netUser.UserName);
            }
        }

        [TestMethod]
        public void TestMethod6()
        {
            var events =
                DataFasade.GetRepository<Event>()
                    .GetAll()
                    .ToList()
                    .Where(
                        e =>
                            e.StartDate >= DateTime.Parse("2016-04-11 14:00:00.000") &&
                            e.StartDate <= DateTime.Parse("2016-04-17 20:00:00.000"));
            foreach (var ev in events)
            {
                Console.WriteLine(
                    "INSERT INTO bbomDb2.dbo.Events" +
                    "(Title, StartDate, EndDate, Url, TypeId, UserId, Spiker, Icon, Description, Stats) " +
                    $"VALUES('{ev.Title}', '{ev.StartDate.AddDays(7)}', '{ev.EndDate.AddDays(7)}', '{ev.Url}', {ev.TypeId}, '{ev.UserId}', '{ev.Spiker}', '', null, null);");
            }
        }

        [TestMethod]
        public void TestMethod7()
        {
            var users = DataFasade.GetRepository<AspNetUser>().GetAll();
            foreach (var u in users)
            {
                Console.WriteLine($"INSERT INTO identityDb.dbo.AspNetUserClaims (UserId, ClaimType, ClaimValue) VALUES ('{u.Id}', 'ActiveTemplate', '3');");
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            var menus = DataFasade.GetRepository<Menu>().GetAll();
            //Admin
            var roles = DataFasade.GetRepository<AspNetRole>().GetAll().Where(role => role.Name == UserRole.Admin);
            foreach (var role in roles)
            {
                foreach (var menu in menus)
                {
                    Console.WriteLine("insert into AccessToMenu(MenuId, RoleId) values({0}, '{1}')", menu.Id, role.Id);
                }
            }
            //user
            roles = DataFasade.GetRepository<AspNetRole>().GetAll().Where(role => role.Name == UserRole.User);
            foreach (var role in roles)
            {
                foreach (var menu in menus)
                {
                    if (menu.Name != "Тренинги"
                        && menu.Name != "Создать сайт!")
                        Console.WriteLine("insert into AccessToMenu(MenuId, RoleId) values({0}, '{1}')", menu.Id, role.Id);
                }
            }
            //notUser
            roles = DataFasade.GetRepository<AspNetRole>().GetAll().Where(role => role.Name == UserRole.NotUser);
            foreach (var role in roles)
            {
                foreach (var menu in menus)
                {
                    if (menu.Name == "Мероприятия")
                        Console.WriteLine("insert into AccessToMenu(MenuId, RoleId) values({0}, '{1}')", menu.Id, role.Id);
                }
            }
            //payFirm
            roles = DataFasade.GetRepository<AspNetRole>().GetAll().Where(role => role.Name == UserRole.PayFirm);
            foreach (var role in roles)
            {
                foreach (var menu in menus)
                {
                    if (menu.Name == "Тренинги"
                        || menu.Name == "Создать сайт!")
                        Console.WriteLine("insert into AccessToMenu(MenuId, RoleId) values({0}, '{1}')", menu.Id, role.Id);
                }
            }
        }
    }
}
