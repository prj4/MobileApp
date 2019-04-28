﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Photobook.Models
{
    public interface IFromJSONParser
    {
        Task<object> DeserializedData(HttpResponseMessage msg);
        Task<T> DeserializedData<T>(HttpResponseMessage msg);
    }

    public class FromJsonParser : IFromJSONParser
    {
        public async Task<object> DeserializedData(HttpResponseMessage msg)
        {
            return await DeserializedData<object>(msg);
        }

        public async Task<T> DeserializedData<T>(HttpResponseMessage msg)
        {
            var data = await msg.Content.ReadAsStringAsync();

            T result = default(T);

            try
            {
                result = JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception e)
            {
                
                Debug.WriteLine(e.Message, "From Json Error");
                Environment.Exit(0);
            }

            return result;
        }
    }

 
}
