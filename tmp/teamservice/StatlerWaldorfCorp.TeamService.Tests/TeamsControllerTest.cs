using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Controllers;
using StatlerWaldorfCorp.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StatlerWaldorfCorp.TeamService.Tests
{
    public class TeamsControllerTest
    {
        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            var ctrl = new TeamsController(new TestMemoryTeamRepository());
            var rawTeams = (IEnumerable<Team>)(ctrl.GetAllTeams() as ObjectResult).Value;
            var allTeams = new List<Team>(rawTeams);

            Assert.Equal(2, allTeams.Count);
            Assert.Equal("one", allTeams[0].Name);
            Assert.Equal("two", allTeams[1].Name);
        }

        [Fact]
        public void GetTeamRetrievesTeam()
        {
            var ctrl = new TeamsController(new TestMemoryTeamRepository());
            var testTeam = new Team("test", Guid.NewGuid());
            ctrl.CreateTeam(testTeam);

            var retrivedTeam = (ctrl.GetTeam(testTeam.Id) as ObjectResult).Value as Team;

            Assert.Equal(testTeam.Id, retrivedTeam.Id);
            Assert.Equal(testTeam.Name, retrivedTeam.Name);
        }

        [Fact]
        public void GetNonExistentTeamReturnsNotFound()
        {
            var ctrl = new TeamsController(new TestMemoryTeamRepository());

            Guid id = Guid.NewGuid();
            IActionResult result = ctrl.GetTeam(id);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void CreateTeamAddsTeamToList()
        {
            var ctrl = new TeamsController(new TestMemoryTeamRepository());
            var rawAllTeams = (ctrl.GetAllTeams() as ObjectResult).Value as IEnumerable<Team>;
            var originalAllTeams = new List<Team>(rawAllTeams);

            var newTeam = new Team("sample", Guid.Empty);
            var createdTeamResult = ctrl.CreateTeam(newTeam) as CreatedResult;

            Assert.Equal(201, createdTeamResult.StatusCode);
            Assert.Equal($"/teams/{newTeam.Id}", createdTeamResult.Location);

            var rawTeams = (IEnumerable<Team>)(ctrl.GetAllTeams() as ObjectResult).Value;
            var allTeams = new List<Team>(rawTeams);

            Assert.Equal(allTeams.Count, originalAllTeams.Count + 1);
            Assert.NotNull(allTeams.SingleOrDefault(x => x.Name == "sample"));
        }
    }
}