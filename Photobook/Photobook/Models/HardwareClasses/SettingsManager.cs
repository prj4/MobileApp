using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class SettingsManager
    {
        private static string UserList = "userlist";
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
            IFolder saveFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await saveFolder.CreateFolderAsync("photobookSaves",
                CreationCollisionOption.OpenIfExists);
            IFolder cookieFolder = await folder.CreateFolderAsync("Cookies", 
                CreationCollisionOption.OpenIfExists);
            IFile file = await cookieFolder.CreateFileAsync(username, CreationCollisionOption.ReplaceExisting);
            
            Debug.WriteLine(file.Path, "Cookiefile path");
            

            string cookies = "";
            for (int i = 0; i < c.Count; ++i)
            {
                cookies += $"{c[i].Name}|{c[i].Value}|{c[i].Path}|{c[i].Domain}";
                cookies += ';';
            }

            await file.WriteAllTextAsync(cookies.Remove(cookies.Length -1));
            Debug.WriteLine(cookies, "SavedCookie");
        }

        public static async void SaveInstance(string id, object instance)
        {
            if (id.Contains("Cookie"))
            {
                CookieCollection cc = (CookieCollection) instance;
                var name = id.Substring(id.IndexOf("Cookie") + "Cookie".Length);
                SaveCookie(cc, name);
            }

            //IFolder saveFolder = FileSystem.Current.LocalStorage;
            //IFolder folder = await saveFolder.CreateFolderAsync("photobookSaves",
            //    CreationCollisionOption.OpenIfExists);
            //IFile file = await  folder.CreateFileAsync("")
        }

        public static List<Guest> GetAllActiveUsers()
        {
            List<Guest> list = null;
            try
            {
                list = (List<Guest>)Application.Current.Properties[UserList];
            }
            catch (KeyNotFoundException e)
            {
                list = new List<Guest>();
                SaveActiveUserList(list);
                return list;
            }

            return list;
        }

        public static async Task<CookieCollection> GetCookies(string id)
        {
            IFolder saveFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await saveFolder.CreateFolderAsync("photobookSaves",
                CreationCollisionOption.OpenIfExists);
            IFolder cookieFolder = await folder.CreateFolderAsync("Cookies",
                CreationCollisionOption.OpenIfExists);
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

        public static async void SaveActiveUserList(List<Guest> current)
        {
            Application.Current.Properties[UserList] = current;
            await Application.Current.SavePropertiesAsync();
           
        }
        
    }
}
