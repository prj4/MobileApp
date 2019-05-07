namespace Photobook.Models
{
    public class UrlFactory
    {
        private static readonly string NewHostServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Host";
        private static readonly string NewEventUrl = "https://photobookwebapi1.azurewebsites.net/api/Event";
        private static readonly string GuestServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Guest";
        private static readonly string HostLoginUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Login";
        private static readonly string PictureUrl = "https://photobookwebapi1.azurewebsites.net/api/Picture";
        private static readonly string DeleteUrl = "https://photobookwebapi1.azurewebsites.net/api/Event";
        private static readonly string GetPictureUrl = "https://photobookwebapi1.azurewebsites.net/api/Picture";
        private static readonly string GetPreviewUrl = "https://photobookwebapi1.azurewebsites.net/api/Picture/Preview";

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
                case DataType.DeleteEvent:
                {
                    return DeleteUrl;
                }
                case DataType.GetPicture:
                {
                    return GetPictureUrl;
                }
                case DataType.GetPreview:
                {
                    return GetPreviewUrl;
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}