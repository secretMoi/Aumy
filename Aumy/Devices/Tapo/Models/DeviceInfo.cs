﻿namespace Aumy.Devices.Tapo.Models
{
    public class DeviceInfo
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public string SessionId { get; set; }
        public string Token { get; set; }
    }
}
