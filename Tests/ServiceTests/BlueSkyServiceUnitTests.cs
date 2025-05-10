using Xunit;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text;
using RestAPI.Services;
using CommonCore.Models.BlueSky;
using Microsoft.Extensions.Configuration;

public class BlueskyServiceTests
{
    [Fact]
    public async Task GetProfileFeedAsync_ReturnsDeserializedResponse()
    {
        // Arrange
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Bluesky:Handle"]).Returns("test-handle");
        configMock.Setup(c => c["Bluesky:Password"]).Returns("test-password");
        var mockLogger = new Mock<ILogger<BlueSkyService>>();
        var expectedJson = @"{
            ""feed"": [
                {
                    ""post"": {
                        ""uri"": ""at://did:xyz/app.bsky.feed.post/123"",
                        ""cid"": ""cid"",
                        ""author"": {
                            ""did"": ""did:xyz"",
                            ""handle"": ""jheldridge.com"",
                            ""displayName"": ""Jeff Heldridge""
                        },
                        ""record"": {
                            ""$type"": ""app.bsky.feed.post"",
                            ""createdAt"": ""2025-05-06T22:25:37.945Z"",
                            ""text"": ""Test post""
                        }
                    }
                }
            ],
            ""cursor"": ""some-cursor""
        }";

        var mockHttp = new HttpMessageHandlerStub((request, cancellationToken) =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return Task.FromResult(response);
        });

        var httpClient = new HttpClient(mockHttp)
        {
            BaseAddress = new Uri("https://example.com")
        };

        var service = new BlueSkyService(httpClient, mockLogger.Object, configMock.Object);

        // Act
        var result = await service.GetProfileAsync("jheldridge.com");

        // Assert
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Contains("did:xyz/app.bsky.feed.post/123", result);
    }

    public class HttpMessageHandlerStub : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

        public HttpMessageHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => _handlerFunc(request, cancellationToken);
    }
}