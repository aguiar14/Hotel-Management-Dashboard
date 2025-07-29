using Backend.Models;

namespace Backend.Controllers.Put
{
    public class RoomTypePut
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public RoomType ToRoomTypeModel()
        {
            return new RoomType
            {
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}
