using System;
using FluorineFx.Net;
using Lollipop.Session;

namespace Lollipop.Tests.Session
{
    public class MockRtmpConnection : IRtmpConnection
    {
        public bool ShouldSucceed { get; set; }

        public bool ShouldDisconnect { get; set; }

        public bool ShouldTimeout { get; set; }

        public Action Respond { get; set; }

        public event NetStatusHandler NetStatus;

        public event ConnectHandler OnConnect;

        public event DisconnectHandler OnDisconnect;

        public object Client { get; set; }

        public string ClientId { get; set; }

        public bool Connected { get; private set; }

        public void AddHeader(string operation, bool mustUnderstand, object param)
        {
            // noop
        }

        public void Connect(string command, params object[] arguments)
        {
            Connected = true;
        }

        public void Close()
        {
            Connected = false;
        }

        public void Call<T>(string command, Responder<T> responder, params object[] arguments)
        {
            Handle(responder);
        }

        public void Call<T>(string endpoint, string destination, string source, string operation, Responder<T> responder,
                            params object[] arguments)
        {
            Handle(responder);
        }

        private void Handle<T>(Responder<T> responder)
        {
            if (ShouldTimeout)
            {
                // never return a real value!
            }
            else if (ShouldDisconnect)
            {
                Connected = false;
                if (OnDisconnect != null)
                    OnDisconnect(this, EventArgs.Empty);
            }
            else if (ShouldSucceed)
            {
                responder.Result(Activator.CreateInstance<T>());
            }
            else
            {
                responder.Status(null);
            }
        }
    }
}