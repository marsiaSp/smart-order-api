namespace SmartOrder.API.DTOs
{    
    public class MenuItemDTO
    {
        public MenuItemDTO()
        {
            
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}