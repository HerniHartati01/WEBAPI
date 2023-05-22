﻿using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Repositories;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRepositoryGeneric<Room> _roomRepository;

        public RoomController(IRepositoryGeneric<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound();
            }

            return Ok(rooms);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var rooms = _roomRepository.GetByGuid(guid);
            if (rooms is null)
            {
                return NotFound();
            }

            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            var result = _roomRepository.Create(room);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Room room)
        {
            var isUpdated = _roomRepository.Update(room);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }

}