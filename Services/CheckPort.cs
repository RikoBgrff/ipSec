using ipSec.Dto;
using System.Net.Sockets;

namespace ipSec.Services
{
    public static class CheckPort
    {
        public async static Task<PortScanResult> Execute(string ip, int port)
        {
            using var client = new TcpClient();
            try
            {
                var connectTask = client.ConnectAsync(ip, port);
                var timeoutTask = Task.Delay(1200);
                var completed = await Task.WhenAny(connectTask, timeoutTask);

                if (completed == connectTask && client.Connected)
                {
                    return new PortScanResult { Port = port, IsOpen = true };
                }
            }
            catch { }

            return new PortScanResult { Port = port, IsOpen = false };
        }

    }
}
