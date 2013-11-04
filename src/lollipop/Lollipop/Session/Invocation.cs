using System;
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
        private IRtmpConnection _connection;

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

        public Invocation(string service, string method, Action<T> success, params object[] parameters)
            : this(null, service, method, success, null, parameters)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (method == null) throw new ArgumentNullException("method");
            if (success == null) throw new ArgumentNullException("success");
        }

        public Invocation(string service, string method, Func<Fault, Exception> failure, params object[] parameters)
            : this(null, service, method, null, failure, parameters)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (method == null) throw new ArgumentNullException("method");
            if (failure == null) throw new ArgumentNullException("success");
        }

        public Invocation(string service, string method, Action<T> success, Func<Fault, Exception> failure, params object[] parameters)
            : this(null, service, method, success, null, parameters)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (method == null) throw new ArgumentNullException("method");
            if (success == null) throw new ArgumentNullException("success");
            if (failure == null) throw new ArgumentNullException("failure");
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

        public Task<T> Execute(IRtmpConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (_connection != null)
                throw new InvalidOperationException("There is already a connection associated with this invocation.");

            _connection = connection;
            _connection.OnDisconnect += Disconnected;

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

        private void Disconnected(object sender, EventArgs args)
        {
            _connection.OnDisconnect -= Disconnected;

            _completionSource.TrySetCanceled();
            //_completionSource.TrySetException(new LeagueException("The connection to the server has been disconnected."));
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
            finally
            {
                _connection.OnDisconnect -= Disconnected;
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
            finally
            {
                _connection.OnDisconnect -= Disconnected;
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
                    case "org.springframework.security.authentication.AuthenticationCredentialsNotFoundException":
                        return new LeagueException((string) rootCause["message"]);
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