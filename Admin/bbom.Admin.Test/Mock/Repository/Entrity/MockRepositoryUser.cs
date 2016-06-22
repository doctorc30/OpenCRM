using System.Collections.Generic;
using System.Linq;
using bbom.Admin.Test.Controllers;
using bbom.Data.IdentityModel;
using Moq;

namespace bbom.Admin.Test.Mock.Repository.Entrity
{
    public class MockRepositoryUser : MockRepository<AspNetUser>
    {
        protected override void Generate()
        {
            base.Generate();
            var userAdmin = new AspNetUser
            {
                Id = UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.Admin].Id,
                UserName = UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.Admin].Name
            };

            userAdmin.AspNetRoles.Add(new AspNetRole
            {
                Id = "7138ef18-c696-450f-aac4-06a692e5f75c",
                Name = "admin"
            });

            //userAdmin.ActiveTemplate = MockRepositoryTemplate.template;

            //todo получить настройки шаблона
            //userAdmin.UsersTemplateSettings.Add(new UsersTemplateSetting {Template = MockRepositoryTemplate.template, VideoLink = "", TemplateId = 1 });

            var user = new AspNetUser
            {
                Id = UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.User].Id,
                UserName = UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.User].Name,
                Email = "",
                InvitedAspNetUser = userAdmin
            };

            user.AspNetRoles.Add(new AspNetRole
            {
                Id = "87f990cc-cbf5-4db2-8d5d-a8ad93782b0b",
                Name = "user"
            });

            //user.ActiveTemplate = MockRepositoryTemplate.template;

            //todo получить настройки шаблона
            //user.UsersTemplateSettings.Add(new UsersTemplateSetting { Template = MockRepositoryTemplate.template, VideoLink = "" , TemplateId = 1});

            List = new List<AspNetUser> {userAdmin, user};
            Setup(repository => repository.GetAll()).Returns(List.AsQueryable());
            Setup(repository => repository.GetById(It.Is<string>(s => s == UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.Admin].Id))).Returns(userAdmin);
            Setup(repository => repository.GetById(It.Is<string>(s => s == UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.User].Id))).Returns(user);
            Setup(repository => repository.GetById(It.IsAny<string>())).Returns(user);
            
        }
    }
}