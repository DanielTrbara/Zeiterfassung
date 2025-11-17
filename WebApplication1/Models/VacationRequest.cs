namespace WebApplication1.Models
{
    public class VacationRequest
    {
        public int Id { get; set; }  // Primärschlüssel
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}