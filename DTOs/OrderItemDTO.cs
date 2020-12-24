namespace SmartOrder.API.DTOs
{

    public class OrderItemDTO
    {
        public int Quantity { get; set; }
        public MenuItemDTO Menuitem { get; set; }
    }
}