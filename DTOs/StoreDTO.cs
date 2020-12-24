namespace SmartOrder.API.DTOs
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string AddressNumber { get; set; }
        public string TK { get; set; }
        public string City { get; set; }

        public StoreDTO()
        {
            
        }
    }
}