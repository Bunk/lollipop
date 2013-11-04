using System;
using FluorineFx.Net;

namespace Lollipop.Session
{
    public interface IRtmpConnection
    {
        event NetStatusHandler NetStatus;
        event ConnectHandler OnConnect;
        event DisconnectHandler OnDisconnect;

        object Client { get; set; }

        string ClientId { get; }

        bool Connected { get; }

        void AddHeader(string operation, bool mustUnderstand, object param);

        void Connect(string command, params object[] arguments);

        void Close();

        void Call<T>(string command, Responder<T> responder, params object[] arguments);

        void Call<T>(string endpoint, string destination, string source, string operation, Responder<T> responder,
                     params object[] arguments);
    }

    public class NetConnectionWrapper : IRtmpConnection
    {
        private readonly NetConnection _connection;

        public NetConnectionWrapper(NetConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            _connection = connection;
        }

        public event NetStatusHandler NetStatus
        {
            add { _connection.NetStatus += value; }
            remove { _connection.NetStatus -= value; }
        }

        public event ConnectHandler OnConnect
        {
            add { _connection.OnConnect += value; }
            remove { _connection.OnConnect -= value; }
        }

        public event DisconnectHandler OnDisconnect
        {
            add { _connection.OnDisconnect += value; }
            remove { _connection.OnDisconnect -= value; }
        }

        public object Client
        {
            get { return _connection.Client; }
            set { _connection.Client = value; }
        }

        public string ClientId
        {
            get { return _connection.ClientId; }
        }

        public bool Connected
        {
            get { return _connection.Connected; }
        }

        public void AddHeader(string operation, bool mustUnderstand, object param)
        {
            _connection.AddHeader(operation, mustUnderstand, param);
        }

        public void Connect(string command, params object[] arguments)
        {
            _connection.Connect(command, arguments);
        }

        public void Close()
        {
            _connection.Close();
        }

        public void Call<T>(string command, Responder<T> responder, params object[] arguments)
        {
            _connection.Call(command, responder, arguments);
        }

        public void Call<T>(string endpoint, string destination, string source, string operation, Responder<T> responder,
                            params object[] arguments)
        {
            _connection.Call(endpoint, destination, source, operation, responder, arguments);
        }
    }
}
