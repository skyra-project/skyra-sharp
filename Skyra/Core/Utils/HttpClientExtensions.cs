using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Utils
{
	public static class HttpClientExtensions
	{
		[NotNull]
		public static Task<TReturn> GetJsonAsync<TReturn>(this HttpClient client, string uri)
		{
			return client.GetJsonAsync<TReturn>(new Uri(uri));
		}

		public static async Task<TReturn> GetJsonAsync<TReturn>([NotNull] this HttpClient client, Uri uri)
		{
			var result = await client.GetAsync(uri);
			result.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<TReturn>(await result.Content.ReadAsStringAsync());
		}

		[NotNull]
		public static Task<string> PostJsonAsync<TValue>(this HttpClient client, string uri, TValue value)
		{
			return client.PostJsonAsync(new Uri(uri), value);
		}

		public static async Task<string> PostJsonAsync<TValue>([NotNull] this HttpClient client, Uri uri, TValue value)
		{
			var content = new StringContent(SerializeValue(value), Encoding.UTF8, "application/json");
			var result = await client.PostAsync(uri, content);
			result.EnsureSuccessStatusCode();
			return await result.Content.ReadAsStringAsync();
		}

		[NotNull]
		public static Task<TReturn> PostJsonAsync<TReturn, TValue>(this HttpClient client, string uri, TValue value)
		{
			return client.PostJsonAsync<TReturn, TValue>(new Uri(uri), value);
		}

		public static async Task<TReturn> PostJsonAsync<TReturn, TValue>([NotNull] this HttpClient client, Uri uri,
			TValue value)
		{
			return JsonConvert.DeserializeObject<TReturn>(await client.PostJsonAsync(uri, value));
		}

		[NotNull]
		public static Task<string> PostTextAsync(this HttpClient client, string uri, string value)
		{
			return client.PostTextAsync(new Uri(uri), value);
		}

		public static async Task<string> PostTextAsync([NotNull] this HttpClient client, Uri uri, string value)
		{
			var content = new StringContent(value, Encoding.UTF8, "plain/text");
			var result = await client.PostAsync(uri, content);
			result.EnsureSuccessStatusCode();
			return await result.Content.ReadAsStringAsync();
		}

		[NotNull]
		public static Task<TReturn> PostTextAsync<TReturn>(this HttpClient client, string uri, string value)
		{
			return client.PostTextAsync<TReturn>(new Uri(uri), value);
		}

		public static async Task<TReturn> PostTextAsync<TReturn>([NotNull] this HttpClient client, Uri uri,
			string value)
		{
			return JsonConvert.DeserializeObject<TReturn>(await client.PostTextAsync(uri, value));
		}

		[NotNull]
		private static string SerializeValue<TValue>(TValue value)
		{
			return JsonConvert.SerializeObject(value, Formatting.None,
				new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
		}
	}
}
