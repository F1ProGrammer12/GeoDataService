using Dadata;
using GeoDataService.Domain.Models;
using GeoDataService.Logic.Exceptions;
using GeoDataService.Logic.Interfaces;
using Newtonsoft.Json;
using System.Globalization;

namespace GeoDataService.Logic.Services;

public class GeographicDataService(HttpClient httpClient) : IGeoDataService, IValidableRequest
{
    public async Task<GeoData> GetLongitudeAndLattitude(Address address)
    {
        await IValidableRequest.IsValidRequest(address);

        var response = await GetResponseFromOpenStreetMap(address);

        if (response == null || response == "[]")
            throw new NotFoundException($"Adress with data: country: {address.Country}, city: {address.City}, street: {address.Street}, house: {address.BuildingNumber} not found");

        var geoData = DeserializationResponseFromOenStreetMap(response);

        return geoData;
    }

    public async Task<IEnumerable<Address>> GetTenNearestAddresses(GeoData geoData)
    {
        const string TOKEN = "6fcfed38e2eb5605d2d49f7b5b4cd6778e0628de";

        await IValidableRequest.IsValidRequest(geoData);

        var dadataApi = new SuggestClientAsync(TOKEN);
        var responseFromDadataApi = await dadataApi.Geolocate(geoData.Latitude, geoData.Longitude, radius_meters: 500, count: 10);

        if (responseFromDadataApi.suggestions.Count < 10)
            throw new NotFoundException($"Top 10 addresses by latitude {geoData.Latitude} and longitude {geoData.Longitude} not found.");

        var tenNearestAddresses = new List<Address>();

        foreach (var responseAddress in responseFromDadataApi.suggestions)
        {
            var address = new Address { Country = responseAddress.data.country, City = responseAddress.data.city, Street = responseAddress.data.street, BuildingNumber = responseAddress.data.house };
            tenNearestAddresses.Add(address);
        }

        return tenNearestAddresses;
    }

    private async Task<string> GetResponseFromOpenStreetMap(Address address)
    {
        const string HEADERS_DESCRIPTOR = "User-Agent";
        const string VALUE = "User-Agent-Here";

        var requestString = $"https://nominatim.openstreetmap.org/search?country={address.Country}&city={address.City}&street={address.Street} {address.BuildingNumber}&format=json&limit=1";

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
        requestMessage.Headers.Add(HEADERS_DESCRIPTOR, VALUE);
        var response = await httpClient.SendAsync(requestMessage);

        return await response.Content.ReadAsStringAsync();
    }

    private GeoData DeserializationResponseFromOenStreetMap(string json)
    {
        var definition = new[] { new { lat = "", lon = "" } };

        var responseAddresses = JsonConvert.DeserializeAnonymousType(json, definition);

        var provider = new NumberFormatInfo { NumberGroupSeparator = "." };

        var responseLatitude = Math.Round(Convert.ToDouble(responseAddresses![0].lat, provider), 5);
        var responseLongitude = Math.Round(Convert.ToDouble(responseAddresses![0].lon, provider), 5);

        return new GeoData { Latitude = responseLatitude, Longitude = responseLongitude };
    }
}
