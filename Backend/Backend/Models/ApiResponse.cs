namespace Backend.Models
{
    public class ApiResponse<T>
    {
        public int TotalCount { get; set; }
        public required IEnumerable<T> items { get; set; }
    }
}
