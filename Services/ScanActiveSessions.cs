using ipSec.Dto;

namespace ipSec.Services
{
    public static class ScanActiveSessions
    {
        public async static Task<List<string>> Execute(string subnet)
        {
            var tasks = new List<Task<PingResult>>();

            for (int i = 1; i <= 254; i++)
            {
                string ip = $"{subnet}.{i}";
                tasks.Add(PingHost.Execute(ip));
            }

            var results = await Task.WhenAll(tasks);
            var activeHosts = results.Where(r => r.IsAlive).Select(r => r.Ip).ToList();
            return activeHosts;
        }
    }
}
