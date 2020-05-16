using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IDataManager
    {
        Task<List<RecordModel>> Get();
        Task<RecordModel> Get(string Title);
        Task<List<RecordModel>> GetByType(string Type);
        Task<bool> Add(RecordModel model);
        Task<bool> UpdateComment(string Title, string NewComment);
        Task<bool> Delete(string Title);
    }
}
