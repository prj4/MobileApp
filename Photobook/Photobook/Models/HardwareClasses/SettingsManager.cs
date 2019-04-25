using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class SettingsManager
    {
        private static readonly string GuestFileName = "savedGuests";
        private static readonly string CookieFolderName = "Cookies";
        private static readonly string SaveFolderName = "photobookSaves";
        private static readonly string UserFolderName = "Users";
        public static object GetSavedInstance(string id)
        {
            object o = null;
            try
            {
                o = Application.Current.Properties[id];
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return o;
        }

        public static async void SaveCookie(CookieCollection c, string username)
        {

            IFolder cookieFolder = await GetToCookieFolder();
            IFile file = await cookieFolder.CreateFileAsync(username, CreationCollisionOption.ReplaceExisting);
            
            

            string cookies = "";
            for (int i = 0; i < c.Count; ++i)
            {
                cookies += $"{c[i].Name}|{c[i].Value}|{c[i].Path}|{c[i].Domain}";
                cookies += ';';
            }

            await file.WriteAllTextAsync(cookies.Remove(cookies.Length -1));
        }

        public static async void SaveInstance(string id, object instance)
        {
            if (id.Contains("Cookie"))
            {
                CookieCollection cc = (CookieCollection) instance;
                var name = id.Substring(id.IndexOf(CookieFolderName) + CookieFolderName.Length);
                SaveCookie(cc, name);
            }
            
        }

        private static async Task<IFolder> GetToUserFolder()
        {
            IFolder saveFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await saveFolder.CreateFolderAsync(SaveFolderName,
                CreationCollisionOption.OpenIfExists);
            return await folder.CreateFolderAsync(UserFolderName,
                CreationCollisionOption.OpenIfExists);
        }

        public static async Task<List<Guest>> GetAllActiveUsers()
        {

            IFolder userFolder = await GetToUserFolder();

            IFile file = await userFolder.CreateFileAsync(GuestFileName,
                CreationCollisionOption.OpenIfExists);

            string guestString = await file.ReadAllTextAsync();

            if(guestString == String.Empty)
                return new List<Guest>();

            var guests = guestString.Split('|');
            
            var list = new List<Guest>();

            foreach (var guest in guests)
            {
                Guest tmp = JsonConvert.DeserializeObject<Guest>(guest);
                list.Add(tmp);
            }

            return list;
        }

        public static async Task<CookieCollection> GetCookies(string id)
        {
            IFolder cookieFolder = await GetToCookieFolder();
            IFile file = await cookieFolder.CreateFileAsync(id, CreationCollisionOption.OpenIfExists);

            var cookies = await file.ReadAllTextAsync();
            var cookieStrings = cookies.Split(';');
            List<string[]> specificCookies = new List<string[]>();

            foreach (var cString in cookieStrings)
            {
                specificCookies.Add(cString.Split('|'));
            }

            CookieCollection cc = new CookieCollection();
            foreach (var cookie in specificCookies)
            {
                cc.Add(new Cookie(cookie[0], cookie[1], cookie[2], cookie[3]));
            }

            return cc;
        }

        private static async Task<IFolder> GetToCookieFolder()
        {
            IFolder saveFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await saveFolder.CreateFolderAsync(SaveFolderName,
                CreationCollisionOption.OpenIfExists);
            return await folder.CreateFolderAsync(CookieFolderName,
                CreationCollisionOption.OpenIfExists);
        }

        public static async void SaveActiveGuestList(List<Guest> current)
        {
            string guests = String.Empty;

            foreach (var guest in current)
            {
                guests += JsonConvert.SerializeObject(guest) + '|';
            }

            IFolder userFolder = await GetToUserFolder();

            IFile guestFile = await userFolder.CreateFileAsync(GuestFileName, 
                CreationCollisionOption.OpenIfExists);

            await guestFile.WriteAllTextAsync(guests);

        }
        
    }
}
