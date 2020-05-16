using Newtonsoft.Json;
using Server.Interfaces;
using Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class DataManager : IDataManager
    {
        const string FILE_PATH = @"c:\temp\data.txt";

        public async Task<bool> Add(RecordModel model)
        {
            var records = await Get();
            if (records.Where(x => x.Title.ToUpper().Equals(model.Title.ToUpper())).Count() > 0)
                return false;

            using (StreamWriter sw = File.AppendText(FILE_PATH))
            {
                var recordStr = JsonConvert.SerializeObject(model);
                await sw.WriteLineAsync(recordStr);
            }
            return true;
        }

        public async Task<bool> Delete(string Title)
        {
            bool didDelete = false;

            var records = await Get();
            StringBuilder sb = new StringBuilder();

            foreach (var record in records)
            {
                if (record.Title.ToUpper().Equals(Title.ToUpper()))
                {
                    didDelete = true;
                    continue;
                }
                var recordJson = JsonConvert.SerializeObject(record);
                sb.Append(recordJson);
            }

            File.WriteAllText(FILE_PATH, sb.ToString());
            return didDelete;
        }

        public async Task<List<RecordModel>> Get()
        {
            List<RecordModel> currentRecords = new List<RecordModel>();

            if (!File.Exists(FILE_PATH))
                using (var file = File.Create(FILE_PATH)) { }

            using (StreamReader sr = File.OpenText(FILE_PATH))
            {
                while (!sr.EndOfStream)
                {
                    var recordStr = await sr.ReadLineAsync();
                    currentRecords.Add(JsonConvert.DeserializeObject<RecordModel>(recordStr));
                }
            }
            return currentRecords;
        }

        public async Task<RecordModel> Get(string Title)
        {
            var records = await Get();
            return records.Where(x => x.Title.ToUpper().Equals(Title.ToUpper())).FirstOrDefault();
        }

        public async Task<List<RecordModel>> GetByType(string Type)
        {
            var records = await Get();
            return records.Where(x => x.Type.ToUpper().Contains(Type.ToUpper())).ToList();
        }

        public async Task<bool> UpdateComment(string Title, string NewComment)
        {
            bool didUpdate = false;

            var records = await Get();
            StringBuilder sb = new StringBuilder();

            foreach (var record in records)
            {
                if (record.Title.ToUpper().Equals(Title.ToUpper()))
                {
                    record.Comment = NewComment;
                    didUpdate = true;
                }
                var recordJson = JsonConvert.SerializeObject(record);
                sb.Append(recordJson);
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(FILE_PATH, sb.ToString());
            return didUpdate;
        }
    }
}
