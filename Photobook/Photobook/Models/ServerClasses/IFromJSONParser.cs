using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Photobook.Models
{
    public interface IFromJSONParser
    {
        Task<object> DeserialisedData(HttpResponseMessage msg);
    }

    public class EventFromJSONParser : IFromJSONParser
    {
        public async Task<object> DeserialisedData(HttpResponseMessage msg)
        {
            var data = await msg.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ServerEvent>(data);
        }
    }

    public class HostFromJSONParser : IFromJSONParser
    {
        public async Task<object> DeserialisedData(HttpResponseMessage msg)
        {
            var data = await msg.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ServerHostResponse>(data);
        }
    }
}
