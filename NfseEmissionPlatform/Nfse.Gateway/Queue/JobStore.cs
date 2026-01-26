namespace Nfse.Gateway.Queue
{
    public enum JobStatus { Queued, Running, Succeeded, Failed }

    public sealed record JobInfo(Guid JobId, JobStatus Status, string? Message, DateTime CreatedAtUtc);

    public interface IJobStore
    {
        void Create(JobInfo info);
        JobInfo? Get(Guid jobId);
        void MarkRunning(Guid jobId);
        void MarkSucceeded(Guid jobId, string? message = null);
        void MarkFailed(Guid jobId, string message);
    }

    public sealed class InMemoryJobStore : IJobStore
    {
        private readonly Dictionary<Guid, JobInfo> _jobs = new();
        private readonly object _lock = new();
        
        public void Create(JobInfo info)
        {
            lock (_lock) _jobs[info.JobId] = info;
        }

        public JobInfo? Get(Guid jobId)
        {
            lock (_lock) return _jobs.TryGetValue(jobId, out var info) ? info : null;
        }

        public void MarkRunning(Guid jobId)
        {
            lock (_lock)
            {
                if (_jobs.TryGetValue(jobId,out var info))
                    _jobs[jobId] = info with { Status = JobStatus.Running };
            }
        }

        public void MarkSucceeded(Guid jobId, string? message = null)
        {
            lock (_lock)
            {
                if (_jobs.TryGetValue(jobId, out var info))
                    _jobs[jobId] = info with {  Status = JobStatus.Succeeded, Message = message };
            }
        }

        public void MarkFailed(Guid jobId, string message)
        {
            lock (_lock)
            {
                if (_jobs.TryGetValue(jobId, out var info))
                    _jobs[jobId] = info with { Status = JobStatus.Failed, Message = message };
            }
        }
    }

    public class JobStore
    {
    }
}
