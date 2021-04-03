using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Data
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public bool? SatelliteCapable { get; set; }
        public double SequenceNumber { get; set; }
        public double LastCom { get; set; }
        public int State { get; set; }
        public int ComState { get; set; }
        public string Pac { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string DeviceTypeId { get; set; }
        public string GroupId { get; set; }
        public int Lqi { get; set; }
        public double ActivationTime { get; set; }
        public int TokenState { get; set; }
        public string TokenDetailMessage { get; set; }
        public int TokenEnd { get; set; }
        public string ContractId { get; set; }
        public double CreationTime { get; set; }
        public string ModemCertificateId { get; set; }
        public bool? Prototype { get; set; }
        public string ProductCertificateId { get; set; }
        public bool? AutomaticRenewal { get; set; }
        public int AutomaticRenewalStatus { get; set; }
        public string CreatedBy { get; set; }
        public double LastEditionTime { get; set; }
        public string LastEditedBy { get; set; }
        public bool? Activable { get; set; }

        public byte[] Payload { get; set; }
        public DateTime DateReceived { get; set; }
        public string RequestOrigin { get; set; }
        public int Type { get; set; }

        public string Data { get; set; }
        public double SeqNumber { get; set; }
        public int SigfoxDeviceTypeId { get; set; }
        public string Acknowledgment { get; set; }
        public int LongPolling { get; set; }
    }
}
