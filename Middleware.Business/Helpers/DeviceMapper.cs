using Microsoft.AspNetCore.Http;
using Middleware.Business.Commons;
using Middleware.Business.Models;
using Middleware.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware.Business.Helpers
{
    public class DeviceMapper
    {
        public static Device MapToSpecificDevice(HttpContext httpContext, DeviceBusinessModel deviceBusinessModel)
        {
            deviceBusinessModel.DateReceived = DateTime.UtcNow;
            deviceBusinessModel.RequestMethod = httpContext.Request.Method;
            deviceBusinessModel.RequestOrigin = GetRequestOrigin(httpContext);

            var deviceResult = new Device();

            if (deviceBusinessModel.RequestMethod.Equals(BusinessConstants.RequestType.Post))
            {
                switch (deviceBusinessModel.DelegateName)
                {
                    case BusinessConstants.DelegateNames.PostmanDelegate:
                        deviceResult = MapToPostmanDevice(deviceBusinessModel);
                        break;
                    case BusinessConstants.DelegateNames.HostDelegate:
                        deviceResult = MapToHostDevice(deviceBusinessModel);
                        break;
                    default:
                        deviceResult = MapToDefaultDevice(deviceBusinessModel);
                        break;
                }
            }

            return deviceResult;
        }
        private static string GetRequestOrigin(HttpContext httpContext)
        {
            var requestOrigin = string.Empty;

            if (httpContext.Request.Headers.ContainsKey(BusinessConstants.RequestOrigin.PostmanToken))
            {
                requestOrigin = BusinessConstants.RequestOrigin.PostmanToken;
            }
            else if (httpContext.Request.Headers.ContainsKey(BusinessConstants.RequestOrigin.HostToken))
            {
                requestOrigin = BusinessConstants.RequestOrigin.HostToken;
            }

            return requestOrigin;
        }

        public static Device MapToPostmanDevice(DeviceBusinessModel deviceBusinessModel)
        {
            var deviceToReturn = MapIncomingModelToDevice(deviceBusinessModel);
            // perform specifc actions/bindings for this type of device
            deviceToReturn.Type = (int)TypeEnum.Postman;
            return deviceToReturn;
        }

        public static Device MapToHostDevice(DeviceBusinessModel deviceBusinessModel)
        {
            var deviceToReturn = MapIncomingModelToDevice(deviceBusinessModel);
            // perform specifc actions/bindings for this type of device
            deviceToReturn.Type = (int)TypeEnum.Host;
            return deviceToReturn;
        }

        public static Device MapToDefaultDevice(DeviceBusinessModel deviceBusinessModel)
        {
            var deviceToReturn = MapIncomingModelToDevice(deviceBusinessModel);
            // perform specifc actions/bindings for this type of device
            deviceToReturn.Type = (int)TypeEnum.Default;
            return deviceToReturn;
        }

        private static Device MapIncomingModelToDevice(DeviceBusinessModel deviceBusinessModel)
        {
            deviceBusinessModel.Location ??= new Location();
            deviceBusinessModel.DeviceType ??= new DeviceType();
            deviceBusinessModel.Group ??= new Group();
            deviceBusinessModel.Token ??= new Token();
            deviceBusinessModel.Contract ??= new Contract();
            deviceBusinessModel.ModemCertificate ??= new ModemCertificate();
            deviceBusinessModel.ProductCertificate ??= new ProductCertificate();

            return new Device
            {
                DeviceId = deviceBusinessModel.DeviceId,
                DeviceName = deviceBusinessModel.DeviceName,
                SatelliteCapable = deviceBusinessModel.SatelliteCapable,
                SequenceNumber = deviceBusinessModel.SequenceNumber,
                LastCom = deviceBusinessModel.LastCom,
                State = deviceBusinessModel.State,
                ComState = deviceBusinessModel.ComState,
                Pac = deviceBusinessModel.Pac,
                Latitude = deviceBusinessModel.Location.Latitude,
                Longitude = deviceBusinessModel.Location.Longitude,
                DeviceTypeId = deviceBusinessModel.DeviceType.DeviceTypeId,
                GroupId = deviceBusinessModel.Group.GroupId,
                Lqi = deviceBusinessModel.Lqi,
                ActivationTime = deviceBusinessModel.ActivationTime,
                TokenState = deviceBusinessModel.Token.State,
                TokenDetailMessage = deviceBusinessModel.Token.DetailMessage,
                TokenEnd = deviceBusinessModel.Token.End,
                ContractId = deviceBusinessModel.Contract.ContractId,
                CreationTime = deviceBusinessModel.CreationTime,
                ModemCertificateId = deviceBusinessModel.ModemCertificate.ModemCertificateId,
                Prototype = deviceBusinessModel.Prototype,
                ProductCertificateId = deviceBusinessModel.ProductCertificate.ProductCertificateId,
                AutomaticRenewal = deviceBusinessModel.AutomaticRenewal,
                AutomaticRenewalStatus = deviceBusinessModel.AutomaticRenewalStatus,
                CreatedBy = deviceBusinessModel.CreatedBy,
                LastEditionTime = deviceBusinessModel.LastEditionTime,
                LastEditedBy = deviceBusinessModel.LastEditedBy,
                Activable = deviceBusinessModel.Activable,
                Payload = deviceBusinessModel.Payload,
                DateReceived = deviceBusinessModel.DateReceived,
                RequestOrigin = deviceBusinessModel.RequestOrigin,
                Data = deviceBusinessModel.Data,
                SeqNumber = deviceBusinessModel.SeqNumber,
                SigfoxDeviceTypeId = deviceBusinessModel.SigfoxDeviceTypeId,
                Acknowledgment = deviceBusinessModel.Acknowledgment,
                LongPolling = deviceBusinessModel.LongPolling
            };
        }
    }
}
