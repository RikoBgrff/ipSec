namespace ipSec.Dto
{
    public class PortScanRequest
    {
        public string Ip { get; set; } = "";
        public List<int> Ports { get; set; } = new();
    }
}
