using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Autofac;

namespace Algorithms.ConsoleApp.Ioc
{
    public class WorkersBuilder
    {
        private readonly ContainerBuilder _containerBuilder;
        private IContainer _container;

        readonly Type _workersBaseInterfaceType = typeof(IWorker);

        private IEnumerable<Type> GetAllConsoleRunnableWorkersTypes()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(_workersBaseInterfaceType.IsAssignableFrom)
                .Where(t => !t.IsInterface)
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetCustomAttributes(true).Any(a => a is ConsoleCommandAttribute))
                .ToArray();
        }

        public WorkersBuilder ()
        {
            _containerBuilder = new ContainerBuilder();
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            var types = GetAllConsoleRunnableWorkersTypes();
            foreach (var type in types)
            {
                _containerBuilder.RegisterType(type);
            }
            _container = _containerBuilder.Build();
        }

        public IWorker Resolve(string wokerName)
        {
            var types = GetAllConsoleRunnableWorkersTypes();
            foreach (var type in types)
            {
                var attributes = type.GetCustomAttributes(true);
                if (
                    attributes.Any(
                        attribute =>
                            attribute is ConsoleCommandAttribute &&
                            ((ConsoleCommandAttribute) attribute).Name == wokerName))
                {
                    return (IWorker) _container.Resolve(type);
                }
            }
            return null;
        }
    }
}
