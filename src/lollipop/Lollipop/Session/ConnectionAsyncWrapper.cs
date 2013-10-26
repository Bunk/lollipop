using System;
using System.Threading.Tasks;
using FluorineFx.Net;

namespace Lollipop.Session
{
    public class ConnectionTask
    {
        private readonly NetConnection _connection;
        private TaskCompletionSource<bool> _completion;

        public ConnectionTask(NetConnection connection)
        {
            _connection = connection;
        }

        public Task<bool> Initiate(Uri server)
        {
            if (_completion != null)
                throw new InvalidOperationException("A connection has already been initiated.");

            _completion = new TaskCompletionSource<bool>();

            try
            {
                _connection.OnConnect += ConnectHandler;
                _connection.OnDisconnect += DisconnectHandler;
                _connection.Connect(server.ToString());
            }
            catch (Exception ex)
            {
                _completion.SetException(ex);
            }

            return _completion.Task;
        }

        private void ConnectHandler(object sender, EventArgs args)
        {
            try
            {
                _completion.TrySetResult(true);
            }
            catch (Exception ex)
            {
                _completion.TrySetException(ex);
            }
            finally
            {
                _connection.OnDisconnect -= DisconnectHandler;
                _connection.OnConnect -= ConnectHandler;
            }
        }

        private void DisconnectHandler(object sender, EventArgs args)
        {
            try
            {
                _completion.TrySetResult(false);
            }
            catch (Exception ex)
            {
                _completion.TrySetException(ex);
            }
            finally
            {
                _connection.OnDisconnect -= DisconnectHandler;
                _connection.OnConnect -= ConnectHandler;
            }
        }
    }
}