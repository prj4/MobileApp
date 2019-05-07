using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;

namespace Photobook.Models
{
    public class MemoryManager
    {
        private static readonly string GuestFileName = "savedGuests";
        private static readonly string CookieFolderName = "Cookies";
        private static readonly string SaveFolderName = "photobookSaves";
        private static readonly string UserFolderName = "Users";
        public static CookieCollection CurrentCookies { get; private set; }

        public static async void SaveCookie(CookieCollection c, string username)
        {
            CurrentCookies = c;
            var cookieFolder = await GetToCookieFolder();
            var file = await cookieFolder.CreateFileAsync(username, CreationCollisionOption.OpenIfExists);


            var cookies = "";
            for (var i = 0; i < c.Count; ++i)
            {
                cookies += $"{c[i].Name}|{c[i].Value}|{c[i].Path}|{c[i].Domain}";
                cookies += ';';
            }

            await file.WriteAllTextAsync(cookies.Remove(cookies.Length - 1));
        }


        private static async Task<IFolder> GetToUserFolder()
        {
            var saveFolder = FileSystem.Current.LocalStorage;
            var folder = await saveFolder.CreateFolderAsync(SaveFolderName,
                CreationCollisionOption.OpenIfExists);
            return await folder.CreateFolderAsync(UserFolderName,
                CreationCollisionOption.OpenIfExists);
        }

        public static async void Purge()
        {

            var userFolder = await GetToUserFolder();
            await userFolder.DeleteAsync();
            var cookieFolder = await GetToCookieFolder();
            await cookieFolder.DeleteAsync();
        }

        public static async Task<List<GuestAtEvent>> GetAllActiveUsers()
        {
            var userFolder = await GetToUserFolder();

            var file = await userFolder.CreateFileAsync(GuestFileName,
                CreationCollisionOption.OpenIfExists);

            var guestString = await file.ReadAllTextAsync();

            if (guestString == string.Empty)
                return new List<GuestAtEvent>();

            var guests = guestString.Split('|');

            var list = new List<GuestAtEvent>();

            foreach (var guest in guests)
            {
                var tmp = JsonConvert.DeserializeObject<GuestAtEvent>(guest);
                //if (EventIsActive(tmp)) list.Add(tmp);
                list.Add(tmp);
            }

            return list;
        }

        private static bool EventIsActive(GuestAtEvent e)
        {
            return !(e.EventInfo.EndDate < DateTime.Now);
        }

        public static async Task<CookieCollection> GetCookies(string id)
        {
            var cookieFolder = await GetToCookieFolder();
            var file = await cookieFolder.CreateFileAsync(id, CreationCollisionOption.OpenIfExists);

            var cookies = await file.ReadAllTextAsync();
            var cookieStrings = cookies.Split(';');
            var specificCookies = new List<string[]>();

            foreach (var cString in cookieStrings) specificCookies.Add(cString.Split('|'));

            var cc = new CookieCollection();
            foreach (var cookie in specificCookies) cc.Add(new Cookie(cookie[0], cookie[1], cookie[2], cookie[3]));

            CurrentCookies = cc;
            return cc;
        }

        private static async Task<IFolder> GetToCookieFolder()
        {
            var saveFolder = FileSystem.Current.LocalStorage;
            var folder = await saveFolder.CreateFolderAsync(SaveFolderName,
                CreationCollisionOption.OpenIfExists);
            return await folder.CreateFolderAsync(CookieFolderName,
                CreationCollisionOption.OpenIfExists);
        }

        public static async void SaveActiveGuestList(List<GuestAtEvent> current)
        {
            var guests = string.Empty;

            foreach (var guest in current) guests += JsonConvert.SerializeObject(guest) + '|';

            var userFolder = await GetToUserFolder();

            var guestFile = await userFolder.CreateFileAsync(GuestFileName,
                CreationCollisionOption.OpenIfExists);

            await guestFile.WriteAllTextAsync(guests.Remove(guests.Length - 1));
        }
    }
}