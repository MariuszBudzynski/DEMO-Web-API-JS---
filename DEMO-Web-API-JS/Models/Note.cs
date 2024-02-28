namespace DEMO_Web_API_JS.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
    }
}
