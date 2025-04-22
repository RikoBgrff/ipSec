using ipSec.Context;
using ipSec.Dto;
using ipSec.Entities;
using ipSec.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ipSec.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NetworkScanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public NetworkScanController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        [HttpPost("scan-ports")]
        public async Task<IActionResult> ScanPorts([FromBody] PortScanRequest request)
        {
            var tasks = request.Ports.Select(port => CheckPort.Execute(request.Ip, port));
            var results = await Task.WhenAll(tasks);
            return Ok(results.Where(r => r.IsOpen));
        }

        [HttpGet("scan")]
        public async Task<IActionResult> ScanNetwork([FromQuery] string subnet = "192.168.1")
        {
            var activeHosts = await ScanActiveSessions.Execute(subnet);
            return Ok(activeHosts);
        }
        [HttpPost("scan-port-range")]
        public async Task<IActionResult> ScanPortRange([FromBody] PortRangeScanRequest request)
        {
            var tasks = new List<Task<PortScanResult>>();

            for (int port = request.StartPort; port <= request.EndPort; port++)
            {
                tasks.Add(CheckPort.Execute(request.Ip, port));
            }

            var results = await Task.WhenAll(tasks);
            return Ok(results.Where(r => r.IsOpen));
        }
        [HttpGet("geoip/{ip}")]
        public async Task<IActionResult> GetGeoInfo(string ip)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://ip-api.com/json/{ip}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpPost("trap")]
        public async Task<IActionResult> LogVisitor([FromBody] TrapDto visitor)
        {
            visitor.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            // Geo lookup (optional)
            using var client = new HttpClient();
            try
            {
                var geo = await client.GetFromJsonAsync<GeoInfo>($"http://ip-api.com/json/{visitor.IPAddress}");
                if (geo != null && geo.Status == "success")
                {
                    visitor.Country = geo.Country;
                    visitor.Region = geo.RegionName;
                    visitor.City = geo.City;
                    visitor.ISP = geo.Isp;
                }
            }
            catch { }

            Console.WriteLine($"[TRAP] Caught user: {visitor.Name}, {visitor.Email}, IP: {visitor.IPAddress}");
            Console.WriteLine($"Location: {visitor.City}, {visitor.Region}, {visitor.Country}, ISP: {visitor.ISP}");
            Console.WriteLine($"Device: {visitor.Platform}, Res: {visitor.ScreenResolution}, RAM: {visitor.DeviceMemory}, Cores: {visitor.HardwareConcurrency}");
            Console.WriteLine($"Agent: {visitor.UserAgent}, Lang: {visitor.Language}, Time: {visitor.Time}, Referrer: {visitor.Referrer}");

            // Map to entity
            var log = new TrapLog
            {
                Name = visitor.Name,
                Email = visitor.Email,
                UserAgent = visitor.UserAgent,
                Platform = visitor.Platform,
                Language = visitor.Language,
                Time = visitor.Time,
                ScreenResolution = visitor.ScreenResolution,
                Timezone = visitor.Timezone,
                Referrer = visitor.Referrer,
                DeviceMemory = visitor.DeviceMemory,
                HardwareConcurrency = visitor.HardwareConcurrency,
                IPAddress = visitor.IPAddress,
                Country = visitor.Country,
                Region = visitor.Region,
                City = visitor.City,
                ISP = visitor.ISP,
            };

            _context.TrapLogs.Add(log);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Tracked successfully." });
        }

        [HttpGet("trap/logs")]
        public async Task<IActionResult> GetTrapLogs()
        {
            var logs = await _context.TrapLogs
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return Ok(logs);
        }

        [HttpGet("ip-info/{ip}")]
        public async Task<IActionResult> GetIPInfo(string ip)
        {
            using var http = new HttpClient();
            var response = await http.GetStringAsync($"http://ip-api.com/json/{ip}");
            return Content(response, "application/json");
        }
    }
}