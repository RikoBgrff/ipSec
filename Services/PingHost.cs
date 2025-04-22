using ipSec.Dto;
using System.Net.NetworkInformation;

namespace ipSec.Services
{
    public static class PingHost
    {
        public static async Task<PingResult> Execute(string ip)
        {
            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync(ip, 100);
                return new PingResult(ip, reply.Status == IPStatus.Success);
            }
            catch
            {
                return new PingResult(ip, false);
            }
        }
    }
}
