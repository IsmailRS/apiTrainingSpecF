using FluentAssertions;
using Newtonsoft.Json;
using reqres.Data;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace reqres
{
    [Binding]
    public class ReqresSteps
    {
        private static HttpClient _httpClient;
        private User user;
        private UserList userList;

        [Given(@"I have connected to reqres")]
        public void GivenIHaveConnectedToReqres()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://reqres.in/");
            _httpClient.Timeout = new TimeSpan(0,0,30);
        }
        
        [When(@"I get a user (.*)")]
        public async Task WhenIGetAUser(int userNumber)
        {
            var response = await _httpClient.GetAsync("/api/users/" + userNumber);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            user = JsonConvert.DeserializeObject<User>(content);
        }
        
        [Then(@"the first name is ""(.*)""")]
        public void ThenTheFirstNameIs(string firstname)
        {
            user.data.first_name.Should().Be(firstname);
        }

        [When(@"I am on page (.*)")]
        public async Task WhenIAmOnPageAsync(int pageNumber)
        {
            var response = await _httpClient.GetAsync("/api/users?page=" + pageNumber);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            userList = JsonConvert.DeserializeObject<UserList>(content);
        }

        [Then(@"there are (.*) items listed")]
        public void ThenThereAreItemsListed(int itemsOnPage)
        {
            userList.data.Count.Should().Be(itemsOnPage);
        }

    }
}
