namespace ipSec.Dto
{
    public class TrapDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        // Passive & active data
        public string UserAgent { get; set; }
        public string Platform { get; set; }
        public string Language { get; set; }
        public string Time { get; set; }
        public string ScreenResolution { get; set; }
        public string Timezone { get; set; }
        public string Referrer { get; set; }
        public string DeviceMemory { get; set; }
        public string HardwareConcurrency { get; set; }
        // Geo
        public string IPAddress { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ISP { get; set; }
    }

}
