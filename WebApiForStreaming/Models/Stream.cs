namespace WebApiForStreaming.Models
{
    public class Stream
    {
        public string StreamerId { get; set; }
        public string Title { get; set; }
        public string SrtPath { get; set; } // Путь для подключения к SRT-серверу
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
