using WEBAPI.Models;

namespace WEBAPI.Contracts
{
    public interface IRoomRepository : IRepositoryGeneric<Room>
    {
        IEnumerable<Room> GetByName(string name);
    }
}
