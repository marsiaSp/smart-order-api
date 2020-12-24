namespace SmartOrder.API.DTOs
{    
    public class CategoryDTO
    {
        public CategoryDTO()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}