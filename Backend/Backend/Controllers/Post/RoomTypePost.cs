
using Backend.Models;
namespace Backend.Controllers.Post
{
    public class RoomTypePost
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
