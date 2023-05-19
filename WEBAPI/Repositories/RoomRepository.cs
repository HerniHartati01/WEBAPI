﻿using Microsoft.EntityFrameworkCore;
using System;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingMangementDbContext _context;
        public RoomRepository(BookingMangementDbContext context)
        {
            _context = context;
        }

        public Room Create(Room room)
        {
            try
            {
                _context.Set<Room>().Add(room);
                _context.SaveChanges();
                return room;
            }
            catch
            {
                return new Room();
            }
        }


        public bool Update(Room room)
        {
            try
            {
                _context.Set<Room>().Update(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Delete(Guid guid)
        {
            try
            {
                var room = GetByGuid(guid);
                if (room == null)
                {
                    return false;
                }

                _context.Set<Room>().Remove(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Set<Room>().ToList();
        }


        public Room? GetByGuid(Guid guid)
        {
            return _context.Set<Room>().Find(guid);
        }
    }
}
