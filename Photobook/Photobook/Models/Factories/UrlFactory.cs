namespace Photobook.Models
{
    public class UrlFactory
    {
        private static readonly string NewHostServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Host";
        private static readonly string NewEventUrl = "https://photobookwebapi1.azurewebsites.net/api/Event";
        private static readonly string GuestServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Guest";
        private static readonly string HostLoginUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Login";
        private static readonly string PictureUrl = "https://photobookwebapi1.azurewebsites.net/api/Picture";

        public static string Generate(DataType d)
        {
            switch (d)
            {
                case DataType.NewUser:
                {
                    return NewHostServerUrl;
                }
                case DataType.Guest:
                {
                    return GuestServerUrl;
                }
                case DataType.NewEvent:
                {
                    return NewEventUrl;
                }
                case DataType.Host:
                {
                    return HostLoginUrl;
                }
                case DataType.Picture:
                {
                    return PictureUrl;
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}