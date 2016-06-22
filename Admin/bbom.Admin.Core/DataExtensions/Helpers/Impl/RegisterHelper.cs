using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.ViewModels.Account;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials;
using bbom.Data.ModelPartials.Constants;
using Microsoft.AspNet.Identity.Owin;

namespace bbom.Admin.Core.DataExtensions.Helpers.Impl
{
    public class RegisterHelper : IRegisterHelper
    {
        private Dictionary<char, string> translation = new Dictionary<char, string>
        {
            {'а', "a"},
            {'б', "b"},
            {'в', "v"},
            {'г', "g"},
            {'д', "d"},
            {'е', "e"},
            {'ё', "jo"},
            {'ж', "je"},
            {'з', "the"},
            {'и', "i"},
            {'к', "k"},
            {'л', "l"},
            {'м', "m"},
            {'н', "n"},
            {'о', "o"},
            {'п', "p"},
            {'р', "r"},
            {'с', "s"},
            {'т', "t"},
            {'у', "u"},
            {'ф', "f"},
            {'х', "h"},
            {'ц', "c"},
            {'ч', "ch"},
            {'ш', "sh"},
            {'щ', "w"},
            {'ъ', ""},
            {'ы', "y"},
            {'ь', ""},
            {'э', "je"},
            {'ю', "ju"},
            {'я', "ya"}
        };
        public void FillParams(PersonalRegisterViewModel model, AspNetUser userBd, Template template,
            AspNetUser parentUser)
        {
            userBd.Name = model.Name;
            userBd.Suname = model.Suname;
            userBd.Altname = model.Altname;
            userBd.InvitedAspNetUser = parentUser;
            userBd.parent_id = parentUser.Id;
            userBd.DateRegistration = DateTime.Now;
            userBd.PhoneNumber = model.Phone;
            CoreFasade.UsersHelper.SetUserActiveTemplate(userBd, template.Id);
            if (model.VideoUrl != null)
            {
                CoreFasade.TemplateHelper.SetTemplateSetting(template, userBd, SettingType.VideoLink, model.VideoUrl);
            }
            if (model.SelectParamId != null)
            {
                var param = DataFasade.GetRepository<ExtraRegParam>().GetById(Convert.ToInt32(model.SelectParamId));
                DataFasade.GetRepository<ReceivedExtraRegParam>().Insert(new ReceivedExtraRegParam
                {
                    ExtraRegParam = param,
                    UserId = userBd.Id,
                    ExtraRegParamId = param.Id
                });
                DataFasade.GetRepository<ReceivedExtraRegParam>().SaveChanges();
            }
            if (model.imagePicker != null)
            {
                CoreFasade.TemplateHelper.SetTemplateSetting(template, userBd, SettingType.Background, model.imagePicker);
            }
            if (model.DiscountId != null && model.DiscountId.IsInt())
            {
                userBd.ReceiveDiscounts.Add(
                    DataFasade.GetRepository<Discount>().GetById(Convert.ToInt32(model.DiscountId)));
            }
            if (model.Skype != null)
            {
                var skype =
                    DataFasade.GetRepository<Communicatio>()
                        .GetAll()
                        .SingleOrDefault(communicatio => communicatio.Name == "Skype");
                if (
                    skype != null)
                    userBd.UserCommunications.Add(new UserCommunication
                    {
                        Value = model.Skype,
                        AspNetUser = userBd,
                        CommunicationId = skype.Id
                    });
            }
        }

        public async Task ExRegisterParamsAsync(PersonalRegisterViewModel model, string userId, int templateId,
            AspNetUser parentUser,
            ICollection<string> addRoles, ICollection<string> removeRoles, HttpContextBase httpContext)
        {
            var userManager = httpContext.GetOwinContext().Get<ApplicationUserManager>();
            var usersRepository = DataFasade.GetRepository<AspNetUser>();
            var user = usersRepository.GetById(userId);
            var template = DataFasade.GetRepository<Template>().GetById(templateId);
            FillParams(model, user, template, parentUser);
            if (addRoles.Count > 0)
                await CoreFasade.UsersHelper.ActionUserRolesAsync(model.UserName, userId,
                    addRoles.ToArray(),
                    httpContext, async (id, roles) => await userManager.AddToRolesAsync(id, roles));
            if (removeRoles.Count > 0)
                await CoreFasade.UsersHelper.ActionUserRolesAsync(model.UserName, userId,
                    removeRoles.ToArray(),
                    httpContext, async (id, roles2) => await userManager.RemoveFromRolesAsync(id, roles2));
            await usersRepository.SaveChangesAsync();
        }

        public void SendConfimMail(string email, string confimUrl)
        {
            // наш email с заголовком письма
            MailAddress from = new MailAddress("mail@doctor-c.ru", "Web Registration");
            // кому отправляем
            MailAddress to = new MailAddress(email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to)
            {
                Subject = "Email confirmation",
                Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                                     "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>", confimUrl),
                IsBodyHtml = true
            };
            // тема письма
            // текст письма - включаем в него ссылку
            // адрес smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587)
            {
                Credentials = new System.Net.NetworkCredential("mail@doctor-c.ru", "306418"),
                EnableSsl = true
            };
            // логин и пароль
            smtp.Send(m);
        }

        private string GenerateUserName(string userNameBase, ICollection<string> users)
        {
            string generateUserName;
            int num = users.Count;
            do
            {
                num ++;
                generateUserName = userNameBase + num;
            } while (users.Contains(generateUserName));
            return generateUserName;
        }

        public string GetNewUserName(string userNameBase)
        {
            userNameBase = ToTranslit(userNameBase.ToLower());
            var findUsers =
                DataFasade.GetRepository<AspNetUser>()
                    .GetAll().ToList()
                    .Where(user => user.UserName.ToLower().IndexOf(userNameBase, StringComparison.Ordinal) >= 0);
            if (!findUsers.Any())
                return userNameBase;
            string newUserName;
            do
            {
                newUserName = GenerateUserName(userNameBase, findUsers.Select(user => user.UserName).ToList());
            } while (DataFasade.GetRepository<AspNetUser>()
                    .GetAll()
                    .Count(user => user.UserName == newUserName) != 0);
            return newUserName;
        }

        string ToTranslit(char c)
        {
            string result;
            if (translation.TryGetValue(c, out result))
                return result;
            return "";
        }

        string ToTranslit(string src)
        {
            return string.Join("", src.Select(ToTranslit));
        }
    }
}