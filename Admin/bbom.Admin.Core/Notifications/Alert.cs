namespace bbom.Admin.Core.Notifications
{
    public class Alert
    {
        public string message { get; set; }

        public string type { get; set; }

        public static Alert Success
        {
            get
            {
                return new Alert
                {
                    message = "Успешно.",
                    type = AlertStyles.Success
                };
            }
        }

        public static Alert Error
        {
            get
            {
                return new Alert
                {
                    message = "Ошибка.",
                    type = AlertStyles.Danger
                };
            }
        }

        public static Alert ShowError(string text)
        {
            return new Alert
            {
                message = "Ошибка." + "\n" + text,
                type = AlertStyles.Danger
            };
        }

        public static Alert ShowSuccess(string text)
        {
            return new Alert
            {
                message = "Успешно." + "\n" + text,
                type = AlertStyles.Success
            };
        }

        public static Alert ShowInfo(string text)
        {
            return new Alert
            {
                message = text,
                type = AlertStyles.Information
            };
        }
    }
}