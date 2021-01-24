using Middleware.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class DeviceMonitor : IObservable<DeviceModel>
    {
        public readonly List<IObserver<DeviceModel>> observers;
        public DeviceMonitor()
        {
            observers = new List<IObserver<DeviceModel>>();
        }
        public IDisposable Subscribe(IObserver<DeviceModel> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers, observer);
        }

        public void UpdateDeviceInfo(DeviceModel device)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(device);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<DeviceModel>> _observers;
            private IObserver<DeviceModel> _observer;
            public Unsubscriber(List<IObserver<DeviceModel>> observers, IObserver<DeviceModel> observer)
            {
                _observers = observers;
                _observer = observer;
            }
            public void Dispose()
            {
                if (_observer != null)
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}
