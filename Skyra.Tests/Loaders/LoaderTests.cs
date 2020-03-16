using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Base;

namespace Skyra.Tests.Loaders
{
	public class LoaderTests
	{
		[Monitor]
		private class MonitorStub : StructureBase, IMonitor
		{
			public Task RunAsync(CoreMessage message)
			{
				throw new System.NotImplementedException();
			}

			public MonitorStub(IClient client) : base(client)
			{
			}
		}

		[Test]
		public void Loader_CanFindMonitor()
		{
			// assign
			var assembly = Substitute.For<Assembly>();
			var client = Substitute.For<IClient>();
			var loader = new Loader(assembly);
			var collection = new ServiceCollection();

			//act

			collection.AddSingleton(client);
			client.ServiceProvider.Returns(collection.BuildServiceProvider());
			assembly.ExportedTypes.Returns(new[] {typeof(MonitorStub)});
			var monitorTypes = loader.LoadMonitors(client).Values.Select(x => x.Instance.GetType()).ToArray();

			//assert

			Assert.IsNotEmpty(monitorTypes, "the loader returned an empty collection for LoadMonitors");
			Assert.AreEqual(1, monitorTypes.Length, "the loader has found more then one type in LoadMonitors");
			Assert.Contains(typeof(MonitorStub), monitorTypes);
		}
	}
}
