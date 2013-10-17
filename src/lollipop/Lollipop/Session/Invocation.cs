using System;
using System.Threading;
using System.Threading.Tasks;
using FluorineFx;
using FluorineFx.Net;

namespace Lollipop.Session
{
    public class Invocation<T>
    {
        private readonly string _endpoint;
        private readonly string _service;
        private readonly string _method;
        private readonly Action<T> _success;
        private readonly Func<Fault, Exception> _failure;
        private readonly object[] _parameters;
        private TaskCompletionSource<T> _completionSource;

        public Invocation(params object[] parameters)
            : this(null, null, null, null, null, parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
        }

        public Invocation(string service, string method, params object[] parameters)
            : this(null, service, method, null, null, parameters)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (method == null) throw new ArgumentNullException("method");
        }

        public Invocation(string method, params object[] parameters)
            : this(null, null, method, null, null, parameters)
        {
            if (method == null) throw new ArgumentNullException("method");
        }

        public Invocation(string method, Action<T> success, params object[] parameters)
            : this(null, null, method, success, null, parameters)
        {
            if (method == null) throw new ArgumentNullException("method");
            if (success == null) throw new ArgumentNullException("success");
        }

        public Invocation(string method, Action<T> success, Func<Fault, Exception> failure, params object[] parameters)
            : this(null, null, method, success, failure, parameters)
        {
            if (method == null) throw new ArgumentNullException("method");
            if (success == null) throw new ArgumentNullException("success");
            if (failure == null) throw new ArgumentNullException("failure");
        }

        public Invocation(string endpoint, string service, string method, Action<T> success,
                          Func<Fault, Exception> failure, params object[] parameters)
        {
            _endpoint = endpoint;
            _service = service;
            _method = method;
            _success = success;
            _failure = failure;
            _parameters = parameters;

            // defaults
            if (_endpoint == null)
                _endpoint = Invocation.EndpointName;
        }

        public Task<T> Execute(NetConnection connection)
        {
            _completionSource = new TaskCompletionSource<T>();

            try
            {
                var responder = new Responder<T>(Success, Failure);
                if (_endpoint == null || _service == null)
                    connection.Call(_method, responder, _parameters);
                else
                    connection.Call(_endpoint, _service, null, _method, responder, _parameters);
            }
            catch (Exception ex)
            {
                _completionSource.TrySetException(ex);
            }

            return _completionSource.Task;
        }

        public Task<T> Execute(NetConnection connection, TimeSpan timeout)
        {
            var source = new CancellationTokenSource();
            var task = Task.Run(() => Execute(connection), source.Token);

            var completed = task.Wait(timeout);
            if (!completed)
                source.Cancel();

            return task;
        }

        private void Success(T obj)
        {
            try
            {
                if (_success != null)
                    _success(obj);

                _completionSource.TrySetResult(obj);
            }
            catch (Exception ex)
            {
                _completionSource.TrySetException(ex);
            }
        }

        private void Failure(Fault fault)
        {
            try
            {
                var encoded = _failure != null ? _failure(fault) : EncodeException(fault);
                _completionSource.TrySetException(encoded);
            }
            catch (Exception ex)
            {
                _completionSource.TrySetException(ex);
            }
        }

        private static Exception EncodeException(Fault fault)
        {
            var rootCause = fault.RootCause as ASObject;
            if (rootCause != null && rootCause.ContainsKey("rootCauseClassname"))
            {
                switch ((string) rootCause["rootCauseClassname"])
                {
                    case "com.riotgames.platform.login.LoginFailedException":
                        return new LoginFailedException();
                    case "com.riotgames.platform.game.GameNotFoundException":
                        return new GameNotFoundException((string) rootCause["message"]);
                    case "com.riotgames.platform.game.GameObserverModeNotEnabledException":
                        return new GameNotObservableException((string) rootCause["message"]);
                }
            }

            return new LeagueException(fault.FaultString);
        }
    }

    public class Invocation
    {
        public const string EndpointName = "my-rtmps";
    }
}