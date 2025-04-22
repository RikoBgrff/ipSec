namespace ipSec.Dto
{
    public class PortRangeScanRequest
    {
        public string Ip { get; set; } = "";
        public int StartPort { get; set; } = 1;
        public int EndPort { get; set; } = 1024;
    }

}
