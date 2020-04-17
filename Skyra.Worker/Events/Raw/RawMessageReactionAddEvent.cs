using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Worker.Events.Raw
{
	[Event]
	public sealed class RawMessageReactionAddEvent : StructureBase
	{
		public RawMessageReactionAddEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessageReactionAddAsync += RunAsync;
		}

		private async Task RunAsync(MessageReactionAddPayload state)
		{
			await RunReactionPrompts(state);
		}

		private async Task RunReactionPrompts([NotNull] MessageReactionAddPayload payload)
		{
			var key = ICorePromptStateReaction.ToKey(payload);
			var result = await Client.Cache.Prompts.GetAsync(key);
			if (result is null) return;

			var state = (result.State as ICorePromptStateReaction)!;
			// ReSharper disable once PossibleNullReferenceException
			var delay = await state.RunAsync(payload);
			if (delay is null)
			{
				await Client.Cache.Prompts.DeleteAsync(key);
			}
			else if (delay != TimeSpan.Zero)
			{
				await Client.Cache.Prompts.SetAsync(result, (TimeSpan) delay);
			}
		}
	}
}
