//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Net.Http;
//using Newtonsoft.Json;

//namespace Photobook.Models
//{
//    public class LoadDataTest
//    {
//        private ObservableCollection<TestUser> _testUsers;
//        public LoadDataTest()
//        {

//        }


//        private const string UserURL = "https://jsonplaceholder.typicode.com/users";
//        private HttpClient _client = new HttpClient();

//        public async void GetUsers()
//        {
//            var content = await _client.GetStringAsync(UserURL);
//            var testUsers = JsonConvert.DeserializeObject<List<TestUser>>(content);

//            _testUsers = new ObservableCollection<TestUser>(testUsers);
//        }
//    }
//}

