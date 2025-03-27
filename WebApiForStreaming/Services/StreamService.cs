using Stream = WebApiForStreaming.Models.Stream;

namespace WebApiForStreaming.Services
{
    public class StreamService
    {
        private readonly List<Stream> _streams = new List<Stream>();

        public void AddStream(Stream stream)
        {
            lock (_streams)
            {
                if (_streams.Any(s => s.StreamerId == stream.StreamerId))
                    throw new InvalidOperationException("Stream with this StreamerId already exists.");
                _streams.Add(stream);
            }
        }

        public List<Stream> GetStreams()
        {
            lock (_streams)
            {
                return _streams.ToList();
            }
        }

        public void RemoveStream(string streamerId)
        {
            lock (_streams)
            {
                var stream = _streams.FirstOrDefault(s => s.StreamerId == streamerId);
                if (stream != null)
                    _streams.Remove(stream);
            }
        }
    }
}
