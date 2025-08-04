using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces;
using Backend.Controllers.Post;
using Backend.Models;
using Backend.Controllers.Put;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController: ControllerBase
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly IRoomRepository _roomRepository;
        
        public RoomsController(ILogger<RoomsController> logger, IRoomRepository roomRepository)
        {
            _logger = logger;
            _roomRepository = roomRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Add(RoomPost room)
        {
            var newRoom = await _roomRepository.CreateRoomAsync(room.ToRoomModel());

            return Created(newRoom.Id.ToString(), newRoom);
        }


        [HttpGet]
        public Task<ApiResponse<Room>> Get()
        {
         
            return _roomRepository.GetRoomsAsync();
        }


        [HttpGet("{roomId}")]
        public async Task<ActionResult<Room>> GetById(int roomId)
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(roomId);

            return existingRoom == null ? NotFound() : Ok(existingRoom);
        }


        [HttpPut("{roomId}")]
        public async Task<ActionResult<Room?>> Update(RoomPut room, int roomId)
        {
            var updated = await _roomRepository.UpdateRoomAsync(room.ToRoomModel(roomId));

            return updated == null ? NotFound() : Ok(updated);
        }


        [HttpDelete("{roomId}")]
        public async Task<ActionResult<IEnumerable<Room>>> DeleteRoom(int roomId)
        {
            var deleted = await _roomRepository.DeleteRoomAsync(roomId);

            return Accepted(deleted); 
        }

    }
}
