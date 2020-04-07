using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using Skyra.Core.Utils;
using Skyra.Core.Utils.Urls;

namespace Skyra.Tests.Utils.Urls
{
	public sealed class UrlRegexTests
	{
		[Test]
		public void UrlRegex_MatchExactUrls([Values(
				"http://foo.com/blah_blah",
				"http://foo.com/blah_blah/",
				"http://foo.com/blah_blah_(wikipedia)",
				"http://foo.com/blah_blah_(wikipedia)_(again)",
				"http://www.example.com/wpstyle/?p=364",
				"https://www.example.com/foo/?bar=baz&inga=42&quux",
				"http://a.b.c.d.e.f.g.h.i.j.k.l.m.n.o.p.q.r.s.t.u.v.w.x.y.z.com",
				"http://a_b.z.com",
				"http://mw1.google.com/mw-earth-vectordb/kml-samples/gp/seattle/gigapxl/$[level]/r$[y]_c$[x].jpg",
				"http://user:pass@example.com:123/one/two.three?q1=a1&q2=a2#body",
				"http://www.microsoft.xn--comindex-g03d.html.irongeek.com",
				"http://✪df.ws/123",
				"http://localhost/",
				"http://userid:password@example.com:8080",
				"http://userid:password@example.com:8080/",
				"http://userid@example.com",
				"http://userid@example.com/",
				"http://userid@example.com:8080",
				"http://userid@example.com:8080/",
				"http://userid:password@example.com",
				"http://userid:password@example.com/",
				"http://142.42.1.1/",
				"http://142.42.1.1:8080/",
				"http://➡.ws/䨹",
				"http://⌘.ws",
				"http://⌘.ws/",
				"http://foo.com/blah_(wikipedia)#cite-1",
				"http://foo.com/blah_(wikipedia)_blah#cite-1",
				"http://foo.com/unicode_(✪)_in_parens",
				"http://foo.com/(something)?after=parens",
				"http://☺.damowmow.com/",
				"http://code.google.com/events/#&product=browser",
				"http://j.mp",
				"ftp://foo.bar/baz",
				"http://foo.bar/?q=Test%20URL-encoded%20stuff",
				// "http://مثال.إختبار",
				// "http://例子.测试",
				// "http://उदाहरण.परीक्षा",
				"http://-.~_!$&\"()*+\";=:%40:80%2f::::::@example.com",
				"http://1337.net",
				"http://a.b-c.de",
				"http://223.255.255.254",
				"http://example.com?foo=bar",
				"http://example.com#foo",
				"ws://localhost:8080",
				"ws://foo.ws",
				"ws://a.b-c.de",
				"ws://223.255.255.254",
				"ws://userid:password@example.com",
				"ws://➡.ws/䨹",
				"//localhost:8080",
				"//foo.ws",
				"//a.b-c.de",
				"//223.255.255.254",
				"//userid:password@example.com",
				"//➡.ws/䨹",
				"www.google.com/unicorn",
				"http://example.com."
			)]
			[NotNull]
			string argument)
		{
			var regex = UrlRegex.Create(true);
			Assert.IsTrue(regex.IsMatch(argument));
		}

		[Test]
		public void UrlRegex_MatchUrlsInText([Values(
				@"Lorem ipsum //dolor.sit
						<a href=""http://example.com"">example.com</a>
						<a href=""http://example.com/with-path"">with path</a>
						[and another](https://another.example.com) and
						Foo //bar.net/?q=Query with spaces"
			)]
			[NotNull]
			string argument)
		{
			var regex = UrlRegex.Create(tlds: false);
			Assert.AreEqual(new[]
			{
				"//dolor.sit",
				"http://example.com",
				"http://example.com/with-path",
				"https://another.example.com",
				"//bar.net/?q=Query"
			}, argument.Matches(regex).Select(x => x.Value).ToArray());
			Assert.IsTrue(regex.IsMatch(argument));
		}

		[Test]
		public void UrlRegex_DoNotMatchUrls([Values(
				"http://",
				"http://.",
				"http://..",
				"http://../",
				"http://?",
				"http://??",
				"http://??/",
				"http://#",
				"http://##",
				"http://##/",
				"http://foo.bar?q=Spaces should be encoded",
				"//",
				"//a",
				"///a",
				"///",
				"http:///a",
				"foo.com",
				"rdar://1234",
				"h://test",
				"http:// shouldfail.com",
				":// should fail",
				"http://foo.bar/foo(bar)baz quux",
				"http://-error-.invalid/",
				"http://-a.b.co",
				"http://a.b-.co",
				"http://123.123.123",
				"http://3628126748",
				"http://.www.foo.bar/",
				"http://.www.foo.bar./",
				"http://go/ogle.com",
				"http://foo.bar/ /",
				"http://a.b_z.com",
				"http://ab_.z.com",
				"http://google\\.com",
				"http://www(google.com",
				"http://www.example.xn--overly-long-punycode-test-string-test-tests-123-test-test123/",
				"http://www=google.com",
				"https://www.g.com/error\n/bleh/bleh",
				"rdar://1234",
				"/foo.bar/",
				"///www.foo.bar./"
			)]
			[NotNull]
			string argument)
		{
			var regex = UrlRegex.Create(true);
			Assert.IsFalse(regex.IsMatch(argument));
		}

		[Test]
		public void UrlRegex_MatchUsingListOfTlds([Values(
				"foo.com/blah_blah",
				"foo.com/blah_blah/",
				"foo.com/blah_blah_(wikipedia)",
				"foo.com/blah_blah_(wikipedia)_(again)",
				"www.example.com/wpstyle/?p=364",
				"www.example.com/foo/?bar=baz&inga=42&quux",
				"a.b.c.d.e.f.g.h.i.j.k.l.m.n.o.p.q.r.s.t.u.v.w.x.y.z.com",
				"mw1.google.com/mw-earth-vectordb/kml-samples/gp/seattle/gigapxl/$[level]/r$[y]_c$[x].jpg",
				"user:pass@example.com:123/one/two.three?q1=a1&q2=a2#body",
				"www.microsoft.xn--comindex-g03d.html.irongeek.com",
				"✪df.ws/123",
				"localhost/",
				"userid:password@example.com:8080",
				"userid:password@example.com:8080/",
				"userid@example.com",
				"userid@example.com/",
				"userid@example.com:8080",
				"userid@example.com:8080/",
				"userid:password@example.com",
				"userid:password@example.com/",
				"142.42.1.1/",
				"142.42.1.1:8080/",
				"➡.ws/䨹",
				"⌘.ws",
				"⌘.ws/",
				"foo.com/blah_(wikipedia)#cite-1",
				"foo.com/blah_(wikipedia)_blah#cite-1",
				"foo.com/unicode_(✪)_in_parens",
				"foo.com/(something)?after=parens",
				"☺.damowmow.com/",
				"code.google.com/events/#&product=browser",
				"j.mp",
				"foo.bar/baz",
				"foo.bar/?q=Test%20URL-encoded%20stuff",
				"-.~_!$&\"()*+\";=:%40:80%2f::::::@example.com",
				"1337.net",
				"a.b-c.de",
				"223.255.255.254",
				"example.com?foo=bar",
				"example.com#foo",
				"localhost:8080",
				"foo.ws",
				"a.b-c.de",
				"223.255.255.254",
				"userid:password@example.com",
				"➡.ws/䨹",
				"//localhost:8080",
				"//foo.ws",
				"//a.b-c.de",
				"//223.255.255.254",
				"//userid:password@example.com",
				"//➡.ws/䨹",
				"www.google.com/unicorn",
				"example.com."
			)]
			[NotNull]
			string argument)
		{
			var regex = UrlRegex.Create(true, false, false);
			Assert.IsTrue(regex.IsMatch(argument));
		}
	}
}
