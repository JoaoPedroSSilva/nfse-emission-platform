using Microsoft.Extensions.Options;
using Nfse.Gateway.Adn;
using Nfse.Gateway.Queue;

namespace Nfse.Gateway.Workers
{
    public sealed class EmissionWorker : BackgroundService
    {
        private readonly ILogger<EmissionWorker> _logger;
        private readonly IEmissionQueue _queue;
        private readonly NfseAdnClient _adn;
        private readonly IOptions<GatewayOptions> _options;
        private readonly IJobStore _jobStore;

        public EmissionWorker(
            ILogger<EmissionWorker> logger,
            IEmissionQueue queue, 
            NfseAdnClient adn, 
            IOptions<GatewayOptions> options, 
            IJobStore jobStore)
        {
            _logger = logger;
            _queue = queue;
            _adn = adn;
            _options = options;
            _jobStore = jobStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("EmissionWorker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                EmissionJob job = await _queue.DequeueAsync(stoppingToken);
                _logger.LogInformation("Processing job {JobId} with {Count} drafts", job.JobId, job.DraftIds.Count);

                _jobStore.MarkRunning(job.JobId);

                try
                {
                    _jobStore.MarkFailed(job.JobId, "MVP worker not wired to XML batch yet.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Job {JobId} failed", job.JobId);
                    _jobStore.MarkFailed(job.JobId, ex.Message);
                }
            }
        }
    }
}
