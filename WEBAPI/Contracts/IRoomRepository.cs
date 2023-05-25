using WEBAPI.Models;
using WEBAPI.ViewModels.Rooms;

namespace WEBAPI.Contracts
{
    public interface IRoomRepository : IRepositoryGeneric<Room>
    {
        IEnumerable<Room> GetByName(string name);
        IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
        IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();
        IEnumerable<RoomBookedTodayVM> GetRoomByDate();
    }
}
