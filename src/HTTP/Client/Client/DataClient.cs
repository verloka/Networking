using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class DataClient: IDisposable
    {
        readonly HttpClient Client;

        private DataClient() { }

        public DataClient(string Host, int Port)
        {
            Client = new HttpClient
            {
                BaseAddress = new UriBuilder("http", Host, Port).Uri
            };
        }

        public async Task<bool> Add(RecordModel newRecord)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "/api/record");
            var requestJson = JsonConvert.SerializeObject(newRecord);
            message.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await Client.SendAsync(message);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<RecordModel>> GetAll()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "/api/record");

            var response = await Client.SendAsync(message);

            if (!response.IsSuccessStatusCode)
                return new List<RecordModel>();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<RecordModel>>(json);
            return result;
        }

        public async Task<List<RecordModel>> GetByType(string Type)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"/api/record/type/{Type}");

            var response = await Client.SendAsync(message);

            if (!response.IsSuccessStatusCode)
                return new List<RecordModel>();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<RecordModel>>(json);
            return result;
        }

        public async Task<RecordModel> GetByTitle(string Title)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"/api/record/{Title}");

            var response = await Client.SendAsync(message);

            if (!response.IsSuccessStatusCode)
                return new RecordModel();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RecordModel>(json);
            return result;
        }

        public async Task<bool> UpdateComment(string Title, string NewComment)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Patch, $"/api/record/{Title}/comments")
            {
                Content = new StringContent($"\"{NewComment}\"", Encoding.UTF8, "application/json")
            };

            var response = await Client.SendAsync(message);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string Title)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, $"/api/record/{Title}");

            var response = await Client.SendAsync(message);
            return response.IsSuccessStatusCode;
        }

        public void Dispose()
        {
            if (Client != null)
                Client.Dispose();
        }
    }
}
