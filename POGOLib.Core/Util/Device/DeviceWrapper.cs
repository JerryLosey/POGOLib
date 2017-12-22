using static POGOProtos.Networking.Envelopes.Signature.Types;

namespace POGOLib.Official.Util.Device
{
    public class DeviceWrapper
    {
        public string UserAgent { get; set; }
        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyPassWord { get; set; }

        public DeviceInfo DeviceInfo { get; set; }
    }
}
