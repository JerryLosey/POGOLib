﻿using System.Net;
using static POGOProtos.Networking.Envelopes.Signature.Types;

namespace POGOLib.Official.Util.Device
{
    public class DeviceWrapper
    {
        public string UserAgent { get; set; }
        public IWebProxy Proxy { get; set; }
        public DeviceInfo DeviceInfo { get; set; }
    }
}
