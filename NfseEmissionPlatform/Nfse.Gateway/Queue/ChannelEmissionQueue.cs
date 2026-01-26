using System.Threading.Channels;

namespace Nfse.Gateway.Queue
{
    public sealed class ChannelEmissionQueue : IEmissionQueue
    {
        private readonly Channel<EmissionJob> _channel;

        public ChannelEmissionQueue()
        {
            _channel = Channel.CreateBounded<EmissionJob>(new BoundedChannelOptions(200)
            {
                SingleReader = true,
                SingleWriter = false,
                FullMode = BoundedChannelFullMode.Wait
            });
        }

        public async ValueTask EnqueueAsync(EmissionJob job, CancellationToken ct)
            => await _channel.Writer.WriteAsync(job, ct);

        public async ValueTask<EmissionJob> DequeueAsync(CancellationToken ct)
            => await _channel.Reader.ReadAsync(ct);

    }
}
