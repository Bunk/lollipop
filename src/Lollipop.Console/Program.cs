using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using Lollipop.Services;
using Lollipop.Session;
using Newtonsoft.Json;

namespace Lollipop.Console
{
    public class Program
    {
        private static ILeagueAccount _account;

        public static Dictionary<string, object> ServiceMap = new Dictionary<string, object>();
        public static Dictionary<string, MethodInfo> MethodMap = new Dictionary<string, MethodInfo>();

        public static void Main(string[] args)
        {
            Initialize();

            System.Console.WriteLine("LOLLIPOP :: Enter a command, pretty please: ");
            var currentLine = System.Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(currentLine))
            {
                try
                {
                    // do something with it
                    var command = Parse(currentLine);
                    if (command.CanExecute())
                    {
                        var result = command.Execute();
                        var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                        System.Console.WriteLine("Request: " + command);
                        System.Console.WriteLine("Result: " + json);
                    }
                    else
                    {
                        System.Console.WriteLine("Unable to execute the command: " + command);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Error: " + ex);
                }

                System.Console.WriteLine();

                currentLine = System.Console.ReadLine();
            }
        }

        private static void Initialize()
        {
            var accounts = InitAccounts();
            var composite = new CompositeLeagueAccount();
            foreach (var account in accounts)
                composite.AddAccount(account);

            var errors = composite.ConnectAll();
            foreach (var error in errors)
                System.Console.WriteLine("Error Connecting: " + error.Key.Region + " >> " + error.Value);

            _account = composite;

            var services = InitializeServices(_account);
            InitializeCommandMaps(services);
        }

        private static void InitializeCommandMaps(IEnumerable<object> services)
        {
            foreach (var service in services)
            {
                var type = service.GetType();
                ServiceMap[type.Name.Replace("Service", "").ToLower()] = service;

                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    MethodMap[method.Name.ToLower()] = method;
                }
            }
        }

        private static IEnumerable<object> InitializeServices(ILeagueAccount account)
        {
            yield return new SummonerService(account);
        }

        private static IEnumerable<LeagueAccount> InitAccounts()
        {
            var ipService = new LocateServerIP();

            var client = new LeagueClient(ipService, new AuthorizationService(), new LeagueConnection());
            yield return new LeagueAccount(client, LeagueRegion.NorthAmerica, "BunkTester", "leaguetester1");

            var client2 = new LeagueClient(ipService, new AuthorizationService(), new LeagueConnection());
            yield return new LeagueAccount(client2, LeagueRegion.NorthAmerica, "BunkTester2", "leaguetester2");
        }

        private static Command Parse(string line)
        {
            var parts = line.Split(' ');
            var serviceName = parts[0].Trim();
            var commandName = parts[1].Trim();
            var service = ServiceMap[serviceName.ToLower()];
            var method = MethodMap[commandName.ToLower()];

            if (service == null)
                throw new InvalidOperationException("Could not find a service mapped to '" + commandName + "'");
            if (method == null)
                throw new InvalidOperationException("Could not find a method mapped to '" + commandName + "'");

            var commandArgs = ParseArgs(method, parts.Skip(2).ToArray()).ToArray();

            return new Command(service, method, commandArgs);
        }

        private static IEnumerable<object> ParseArgs(MethodInfo method, string[] args)
        {
            var parms = method.GetParameters();
            for (var i = 0; i < parms.Length && i < args.Length; i++)
            {
                var parm = parms[i];
                var parmType = parm.ParameterType;
                var argVal = args[i];

                yield return Convert.ChangeType(argVal, parmType);
            }
        }
    }

    public class Command
    {
        public object Service { get; private set; }

        public MethodInfo Method { get; private set; }

        public object[] Arguments { get; private set; }

        public Command(object service, MethodInfo method, params object[] arguments)
        {
            Service = service;
            Method = method;
            Arguments = arguments;
        }

        public bool CanExecute()
        {
            return Service != null &&
                   Method != null &&
                   Method.GetParameters().Length == Arguments.Length;
        }

        public object Execute()
        {
            return Method.Invoke(Service, Arguments);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1} :: {2}",
                                 Service.GetType().Name,
                                 Method.Name,
                                 JsonConvert.SerializeObject(Arguments));
        }
    }
}
