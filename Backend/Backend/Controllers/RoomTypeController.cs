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
    public class RoomTypeController: ControllerBase
    {

        private readonly ILogger<RoomTypeController> _logger;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeController(ILogger<RoomTypeController> logger, IRoomTypeRepository roomTypeRepository)
        {
            _logger = logger;
            _roomTypeRepository = roomTypeRepository;
        }


        [HttpPost]
        public async Task<ActionResult<RoomType>> Add(RoomTypePost roomType)
        {
            var newRoomType = await _roomTypeRepository.CreateRoomTypeAsync(roomType.ToRoomTypeModel());
            return Created(newRoomType.Id.ToString(), newRoomType);
        }

        [HttpGet]   
        public Task<ApiResponse<RoomType>> Get()
        {
            return _roomTypeRepository.GetRoomTypesAsync();
        }

        [HttpGet("{roomTypeId}")]
        public async Task<ActionResult<RoomType>> GetById(int roomTypeId)
        {
            var existingRoomType = await _roomTypeRepository.GetRoomTypeByIdAsync(roomTypeId);
            return existingRoomType == null ? NotFound() : Ok(existingRoomType);
        }

        [HttpPut("{roomTypeId}")]
        public async Task<ActionResult<RoomType?>> Update(RoomTypePut roomType, int roomTypeId)
        {
            var updated = await _roomTypeRepository.UpdateRoomTypeAsync(roomType.ToRoomTypeModel(roomTypeId));
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{roomTypeId}")]
        public async Task<ActionResult<IEnumerable<RoomType>>> DeleteRoomType(int roomTypeId)
        {
            var deleted = await _roomTypeRepository.DeleteRoomTypeAsync(roomTypeId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
