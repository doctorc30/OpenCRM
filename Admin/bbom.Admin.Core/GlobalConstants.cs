using System;

namespace bbom.Admin.Core
{
    public static class GlobalConstants
    {
        public const string Www = "www";
        public const string ImageBackgroundPath = "~/Content/img/bg";
        public const string ImageEventsPath = "../../Content/img/";
        public const string ImageUserProfilePath = "/Manage/GetProfileUserImage?userId=";
        public const string ImageUserProfilePathByName = "/Manage/GetProfileUserImageByName?userName=";
        public const string EmptyNodePostfix = "empt";
        public const int InviteDiscountAmount = 5;
        public const int DefaultTemplate = 3;
        //todo генерировать пароль для регистрациии без пароля
        public const string PasswordPostfix = "qQwe1*";
        public const string NewUserPrefix = "newuser";
        public static readonly DateTime RoleCasheTimeOut = DateTime.Now.AddMinutes(3);
    }
}