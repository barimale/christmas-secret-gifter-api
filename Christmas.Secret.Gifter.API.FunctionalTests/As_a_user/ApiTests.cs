using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Asp.Versioning;
using Asp.Versioning.Http;
using Christmas.Secret.Gifter.Application.Queries;
using Christmas.Secret.Gifter.Domain;
using eShop.Ordering.FunctionalTests;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Christmas.Secret.Gifter.API.FunctionalTests.As_a_user;

public sealed class OrderingApiTests : IClassFixture<OrderingApiFixture>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;

    public OrderingApiTests(OrderingApiFixture fixture)
    {
        var handler = new ApiVersionHandler(new QueryStringApiVersionWriter(), new ApiVersion(1.0));

        _webApplicationFactory = fixture;
        _httpClient = _webApplicationFactory.CreateDefaultClient(handler);
    }

    [Fact(Skip = "not compatible with github action")]
    public async Task CreateEvent()
    {
        //given

        //when
        var response = await _httpClient.PostAsync("api/v1/events/create", null);
        var s = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        //then
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact(Skip = "not compatible with github action")]
    public async Task ExecuteWithoutParticipants()
    {
        // given
        var eventResponse = await _httpClient.PostAsync("api/v1/events/create", null);
        var eventId = await eventResponse.Content.ReadAsAsync<GiftEvent>();

        // when
        var content = new StringContent(eventId.Id, UTF8Encoding.UTF8, "application/json")
        {
            Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
        };
        var response = await _httpClient.PostAsync("/api/v1/events/" + eventId.Id + "/execute", content);
        var s = await response.Content.ReadAsStringAsync();

        // then
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(Skip = "not compatible with github action")]
    public async Task ExecuteWithParticipants()
    {
        // given
            // event
            var eventResponse = await _httpClient.PostAsync("api/v1/events/create", null);
            var eventId = await eventResponse.Content.ReadAsAsync<GiftEvent>();
            
            // and three participants
            var participantA = await _httpClient.PostAsync(
                "api/v1/events/" + eventId.Id + "/participants/register",
                GetParticipant(Guid.NewGuid().ToString(), eventId.Id));
            var participantB = await _httpClient.PostAsync(
                "api/v1/events/" + eventId.Id + "/participants/register",
                GetParticipant(Guid.NewGuid().ToString(), eventId.Id));
            var participantC = await _httpClient.PostAsync(
                "api/v1/events/" + eventId.Id + "/participants/register",
                GetParticipant(Guid.NewGuid().ToString(), eventId.Id));

        // when
        var content = new StringContent(eventId.Id, UTF8Encoding.UTF8, "application/json")
        {
            Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
        };
        var response = await _httpClient.PostAsync(
            "/api/v1/events/" + eventId.Id + "/execute",
            content);

        var s = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        // then
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private StringContent GetParticipant(string id, string eventId)
    {
        var newParticipant = new Participant()
        {
            Id = id,
            EventId = eventId,
            Name = "name" + id,
            Email = id + "email@email.com",
            ExcludedOrderIds = [0]
        };

        var serialized = JsonSerializer.Serialize(newParticipant);

        var content = new StringContent(
            serialized)
        {
            Headers = {
                { "x-requestid", Guid.NewGuid().ToString() },
            { "x-api-version", "1.0" }}
        };

        content.Headers.Remove("content-type");
        content.Headers.Add("content-type", "application/json");

        return content;
    }
}
