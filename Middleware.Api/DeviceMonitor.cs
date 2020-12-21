using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class DeviceMonitor : IObservable<Device>
    {
        public readonly List<IObserver<Device>> observers;
        public DeviceMonitor()
        {
            observers = new List<IObserver<Device>>();
        }
        public IDisposable Subscribe(IObserver<Device> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers, observer);
        }

        public void UpdateDeviceInfo(Device device)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(device);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Device>> _observers;
            private IObserver<Device> _observer;
            public Unsubscriber(List<IObserver<Device>> observers, IObserver<Device> observer)
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
