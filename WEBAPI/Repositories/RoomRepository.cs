using Microsoft.EntityFrameworkCore;
using System;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class RoomRepository : RepositoryGeneric<Room>, IRoomRepository
    {
        public RoomRepository(BookingMangementDbContext context) : base(context) { }

        public IEnumerable<Room> GetByName(string name)
        {
            return _context.Set<Room>().Where(r => r.Name.Contains(name));
        }
    }
}
