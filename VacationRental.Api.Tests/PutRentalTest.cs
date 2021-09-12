using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Api.Domain.Commons;
using VacationRental.Api.Domain.Rental;
using Xunit;

namespace VacationRental.Api.Tests
{
    public class PutRentalTest
    {
        private readonly HttpClient _client;

        public PutRentalTest(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPutRental_ThenAGetReturnsTheCreatedRental()
        {
            var request = new RentalBindingDTO
            {
                Units = 25
            };

            ResourceIdViewModel putResult;
            using (var postResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", request))
            {
                Assert.True(postResponse.IsSuccessStatusCode);
                putResult = await postResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            using (var getResponse = await _client.GetAsync($"/api/v1/rentals/{putResult.Id}"))
            {
                Assert.True(getResponse.IsSuccessStatusCode);

                var getResult = await getResponse.Content.ReadAsAsync<RentalViewDTO>();
                Assert.Equal(request.Units, getResult.Units);
            }
        }
    }
}
