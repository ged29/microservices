using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using System;

namespace StatlerWaldorfCorp.TeamService.Controllers
{
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        private ITeamRepository repository;

        public TeamsController(ITeamRepository teamRepository)
        {
            repository = teamRepository;
        }

        [HttpGet]
        public IActionResult GetAllTeams()
        {
            return this.Ok(repository.List());
        }

        [HttpGet("{id}")] // get -> /teams/id
        public IActionResult GetTeam(Guid id)
        {
            var team = repository.Get(id);
            if (team != null)
            {
                return this.Ok(team);
            }

            return this.NotFound();
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody]Team newTeam)
        {
            repository.Add(newTeam);
            return this.Created($"/teams/{newTeam.Id}", newTeam);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam([FromBody]Team team, Guid id)
        {
            var updatedTeam = repository.Update(team);
            if (updatedTeam != null)
            {
                return this.Ok(updatedTeam);
            }

            return this.NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(Guid id)
        {
            var deletedTeam = repository.Delete(id);
            if (deletedTeam != null)
            {
                return this.Ok(deletedTeam.Id);
            }

            return this.NotFound();
        }
    }
}
