using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Base;

namespace Skyra.Tests.Loaders
{
	public sealed class LoaderTests
	{
		[Test]
		public void Loader_CanFindMonitor()
		{
			// Assign
			var assembly = Substitute.For<Assembly>();
			var client = Substitute.For<IClient>();
			var loader = new Loader(client, assembly);
			var collection = new ServiceCollection();

			// Act
			collection.AddSingleton(client);
			client.ServiceProvider.Returns(collection.BuildServiceProvider());
			assembly.ExportedTypes.Returns(new[] {typeof(MonitorStub)});
			var monitorTypes = loader.LoadMonitors().Values.Select(x => x.Instance.GetType()).ToArray();

			// Assert
			Assert.IsNotEmpty(monitorTypes, "the loader returned an empty collection for LoadMonitors");
			Assert.AreEqual(1, monitorTypes.Length, "the loader has found more then one type in LoadMonitors");
			Assert.Contains(typeof(MonitorStub), monitorTypes);
		}

		[Monitor]
		private sealed class MonitorStub : StructureBase, IMonitor
		{
			public MonitorStub(IClient client) : base(client)
			{
			}

			public Task RunAsync(CoreMessage message)
			{
				throw new NotImplementedException();
			}
		}
	}
}
