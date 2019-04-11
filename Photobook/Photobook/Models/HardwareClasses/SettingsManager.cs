using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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

        public static async void SaveInstance(string id, object instance)
        {
            Application.Current.Properties[id] = instance;
            await Application.Current.SavePropertiesAsync();
        }

        public static List<GuestUser> GetAllActiveUsers()
        {
            List<GuestUser> list = null;
            try
            {
                list = (List<GuestUser>)Application.Current.Properties[UserList];
            }
            catch (KeyNotFoundException e)
            {
                list = new List<GuestUser>();
                SaveActiveUserList(list);
                return list;
            }

            return list;
        }

        public static async void SaveActiveUserList(List<GuestUser> current)
        {
            Application.Current.Properties[UserList] = current;
            await Application.Current.SavePropertiesAsync();
        }
        
    }
}
