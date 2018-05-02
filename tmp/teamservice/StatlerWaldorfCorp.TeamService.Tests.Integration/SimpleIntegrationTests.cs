using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using StatlerWaldorfCorp.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace StatlerWaldorfCorp.TeamService.Tests.Integration
{
    public class SimpleIntegrationTests
    {
        private readonly TestServer testServer;
        private readonly HttpClient testClient;
        private readonly Team teamZombie;

        public SimpleIntegrationTests()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            testClient = testServer.CreateClient();
            teamZombie = new Team("Zombie", Guid.NewGuid());
        }

        [Fact]
        public async void TestTeamPostAndGet()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(teamZombie), Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await testClient.PostAsync("/teams", stringContent);
            postResponse.EnsureSuccessStatusCode();

            HttpResponseMessage getResponse = await testClient.GetAsync("/teams");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();
            IList<Team> teams = JsonConvert.DeserializeObject<List<Team>>(raw);

            Assert.Equal(1, teams.Count);
            Assert.Equal("Zombie", teams[0].Name);
            Assert.Equal(teamZombie.Id, teams[0].Id);
        }

        [Fact]
        public async void TestMemberPostAndGet()
        {
            var teamContent = new StringContent(JsonConvert.SerializeObject(teamZombie), Encoding.UTF8, "application/json");
            var result = await testClient.PostAsync("/teams", teamContent);
            result.EnsureSuccessStatusCode();

            var member = new Member(Guid.NewGuid(), "Pitter", "Penn");
            var memberContent = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8, "application/json");
            result = await testClient.PostAsync($"/teams/{teamZombie.Id}/members", memberContent);
            result.EnsureSuccessStatusCode();
        }
    }
}
