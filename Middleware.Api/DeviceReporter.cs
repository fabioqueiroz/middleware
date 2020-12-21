using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class DeviceReporter : IObserver<Device>
    {
        private IDisposable _unsubscriber;
        public virtual void Subscribe(IObservable<Device> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }
        public virtual void Unsubscribe()
        {
           _unsubscriber.Dispose();
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Device value)
        {
            // send to the data access
        }
    }
}
