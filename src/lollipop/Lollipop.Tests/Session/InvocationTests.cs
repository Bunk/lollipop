using System;
using System.Threading;
using System.Threading.Tasks;
using FluorineFx.Net;
using Lollipop.Session;
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
        [ExpectedException(typeof (InvalidOperationException))]
        public async void Can_return_faults_when_a_service_call_fails()
        {
            var connection = new MockRtmpConnection {ShouldSucceed = false};

            var failure = new Func<Fault, Exception>(fault => new InvalidOperationException());
            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method", null, failure);
            await invocation.Execute(connection);
        }

        [Test]
        [ExpectedException(typeof (TaskCanceledException))]
        public async void Will_cancel_when_disconnected()
        {
            var connection = new MockRtmpConnection {ShouldDisconnect = true};
            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method");
            await invocation.Execute(connection);
        }

        [Test]
        [ExpectedException(typeof (TaskCanceledException))]
        public async void Can_manually_cancel_the_invocation()
        {
            var connection = new MockRtmpConnection {ShouldTimeout = true};

            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method");
            using (var tcs = new CancellationTokenSource())
            {
                var task = invocation.Execute(connection, tcs.Token);

                // immediately cancel
                tcs.Cancel();

                await task;
            }
        }

        [Test]
        [ExpectedException(typeof (TaskCanceledException))]
        public async void Can_automatically_cancel_the_invocation_after_a_default_timespan()
        {
            var connection = new MockRtmpConnection {ShouldTimeout = true};

            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method")
            {
                DefaultTimeout = TimeSpan.FromSeconds(1)
            };

            await invocation.Execute(connection);
        }

        [Test]
        [ExpectedException(typeof (TaskCanceledException))]
        public async void Can_automatically_cancel_the_invocation_after_a_fixed_timespan()
        {
            var connection = new MockRtmpConnection {ShouldTimeout = true};

            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method");

            await invocation.Execute(connection, TimeSpan.FromSeconds(1));
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public async void Will_throw_when_an_error_happens_during_success_parsing()
        {
            var connection = new MockRtmpConnection {ShouldSucceed = true};

            var success = new Action<object>(o => { throw new InvalidOperationException(); });
            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method", success, null);
            await invocation.Execute(connection);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public async void Will_throw_when_an_error_happens_during_failure_parsing()
        {
            var connection = new MockRtmpConnection {ShouldSucceed = false};

            var failure = new Func<Fault, Exception>(o => { throw new InvalidOperationException(); });
            var invocation = new Invocation<object>(Invocation.EndpointName, "service", "method", null, failure);
            await invocation.Execute(connection);
        }
    }
}