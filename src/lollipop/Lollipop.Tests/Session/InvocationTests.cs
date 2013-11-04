using System;
using System.Runtime.Serialization.Formatters;
using FluorineFx.Net;
using Lollipop.Session;
using Moq;
using NUnit.Framework;

namespace Lollipop.Tests.Session
{
    [TestFixture]
    public class InvocationTests
    {
        [Test]
        public async void Can_execute_a_service_call()
        {
            var connection = new MockRtmpConnection {ShouldSucceed = true};

            var successful = false;
            var success = new Action<object>(o => successful = true);
            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method", success, null);
            var result = await invocation.Execute(connection);

            Assert.That(result, Is.Not.Null);
            Assert.That(successful, Is.True);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public async void Can_return_faults_when_a_service_call_fails()
        {
            var connection = new MockRtmpConnection { ShouldSucceed = false };

            var failure = new Func<Fault, Exception>(fault => new InvalidOperationException());
            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method", null, failure);
            await invocation.Execute(connection);
        }
    }

    public class MockRtmpConnection : IRtmpConnection
    {
        public bool ShouldSucceed { get; set; }

        public event NetStatusHandler NetStatus;
        
        public event ConnectHandler OnConnect;

        public event DisconnectHandler OnDisconnect;

        public object Client { get; set; }

        public string ClientId { get; private set; }

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
            if (ShouldSucceed)
            {
                responder.Result(Activator.CreateInstance<T>());
            }
            else
            {
                responder.Status(null);
            }
        }

        public void Call<T>(string endpoint, string destination, string source, string operation, Responder<T> responder,
                            params object[] arguments)
        {
            if (ShouldSucceed)
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
