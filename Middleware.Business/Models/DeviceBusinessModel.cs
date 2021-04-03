using Middleware.Business.Commons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Business.Models
{
    /// <summary>
    /// This class has all the properties of all types
    /// of possible devices that may come via a request
    /// </summary>
    public class DeviceBusinessModel
    {
        public int Id { get; set; }
        [JsonProperty("id")]
        public string DeviceId { get; set; }
        [JsonProperty("name")]
        public string DeviceName { get; set; }
        [JsonProperty("satelliteCapable")]
        public bool SatelliteCapable { get; set; }
        [JsonProperty("sequenceNumber")]
        public double SequenceNumber { get; set; }
        [JsonProperty("lastCom")]
        public double LastCom { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
        [JsonProperty("comState")]
        public int ComState { get; set; }
        [JsonProperty("pac")]
        public string Pac { get; set; }
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("deviceType")]
        public DeviceType DeviceType { get; set; }
        [JsonProperty("group")]
        public Group Group { get; set; }
        [JsonProperty("lqi")]
        public int Lqi { get; set; }
        [JsonProperty("activationTime")]
        public double ActivationTime { get; set; }
        [JsonProperty("token")]
        public Token Token { get; set; }
        [JsonProperty("contract")]
        public Contract Contract { get; set; }
        [JsonProperty("creationTime")]
        public double CreationTime { get; set; }
        [JsonProperty("modemCertificate")]
        public ModemCertificate ModemCertificate { get; set; }
        [JsonProperty("prototype")]
        public bool Prototype { get; set; }
        [JsonProperty("productCertificate")]
        public ProductCertificate ProductCertificate { get; set; }
        [JsonProperty("automaticRenewal")]
        public bool AutomaticRenewal { get; set; }
        [JsonProperty("automaticRenewalStatus")]
        public int AutomaticRenewalStatus { get; set; }
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("lastEditionTime")]
        public double LastEditionTime { get; set; }
        [JsonProperty("lastEditedBy")]
        public string LastEditedBy { get; set; }
        [JsonProperty("activable")]
        public bool Activable { get; set; }


        public byte[] Payload { get; set; }
        public string RequestOrigin { get; set; }
        public string RequestMethod { get; set; }
        public string DelegateName { get; set; }
        [JsonProperty("time")]
        public DateTime DateReceived { get; set; }
        public TypeEnum Type { get; set; }


        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("seqNumber")]
        public double SeqNumber { get; set; }
        [JsonProperty("deviceTypeId")]
        public int SigfoxDeviceTypeId { get; set; }
        [JsonProperty("ack")]
        public string Acknowledgment { get; set; }
        [JsonProperty("longPolling")]
        public int LongPolling { get; set; }
    }

    
    public class Location
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }

    public class DeviceType
    {
        [JsonProperty("id")]
        public string DeviceTypeId { get; set; }
    }

    public class Group
    {
        [JsonProperty("id")]
        public string GroupId { get; set; }
    }

    public class Token
    {
        [JsonProperty("state")]
        public int State { get; set; }
        [JsonProperty("detailMessage")]
        public string DetailMessage { get; set; }
        [JsonProperty("end")]
        public int End { get; set; }
    }

    public class Contract
    {
        [JsonProperty("id")]
        public string ContractId { get; set; }
    }

    public class ModemCertificate
    {
        [JsonProperty("id")]
        public string ModemCertificateId { get; set; }
    }

    public class ProductCertificate
    {
        [JsonProperty("id")]
        public string ProductCertificateId { get; set; }
    }
}
