namespace tasks_ASPdotnetAPI
{
    public class RequestLimiterService
    {
        private readonly SemaphoreSlim _semaphore;

        public RequestLimiterService(int parallelLimit)
        {
            _semaphore = new SemaphoreSlim(parallelLimit, parallelLimit);
        }

        public async Task<bool> TryEnterAsync()
        {
            return await _semaphore.WaitAsync(0);
        }

        public void Exit()
        {
            _semaphore.Release();
        }
    }
}
