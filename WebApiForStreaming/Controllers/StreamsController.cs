using Microsoft.AspNetCore.Mvc;
using WebApiForStreaming.Services;
using Stream = WebApiForStreaming.Models.Stream;

namespace WebApiForStreaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamsController : ControllerBase
    {
        private readonly StreamService _streamService;

        public StreamsController(StreamService streamService)
        {
            _streamService = streamService;
        }

        // Получить список трансляций (для зрителей)
        [HttpGet]
        public ActionResult<List<Stream>> GetStreams()
        {
            return Ok(_streamService.GetStreams());
        }

        // Создать новую трансляцию (от стримера)
        [HttpPost]
        public ActionResult CreateStream([FromBody] CreateStreamRequest request)
        {
            try
            {
                var stream = new Stream
                {
                    StreamerId = request.StreamerId,
                    Title = request.Title,
                    SrtPath = $"srt://89.169.135.34:9998?streamid={request.StreamerId}", // Для зрителей
                    Status = "active",
                    CreatedAt = DateTime.UtcNow
                };
                _streamService.AddStream(stream);
                return Ok(new { Message = "Stream created", StreamerUrl = "srt://89.169.135.34:9999", Stream = stream });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Удалить трансляцию (вызывается при завершении)
        [HttpDelete("{streamerId}")]
        public ActionResult DeleteStream(string streamerId)
        {
                _streamService.RemoveStream(streamerId);
                return Ok(new { Message = $"Stream {streamerId} removed" });
            }
        }

        // DTO для запроса на создание трансляции
        public class CreateStreamRequest
        {
            public string StreamerId { get; set; }
            public string Title { get; set; }
        }
    }
