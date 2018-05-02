using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService.Controllers
{
    [Route("/teams/{teamId}/[controller]")]
    public class MembersController : Controller
    {
        private ITeamRepository repository;

        public MembersController(ITeamRepository teamRepository)
        {
            repository = teamRepository;
        }

        [HttpGet]
        public IActionResult GetAllMembers(Guid teamId)
        {
            var team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }

            return this.Ok(team.Members);
        }

        [HttpGet("/teams/{teamId}/[controller]/{memberId}")]
        public IActionResult GetMember(Guid teamId, Guid memberId)
        {
            var team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }

            var member = team.Members.SingleOrDefault(m => m.Id == memberId);
            if (member == null)
            {
                return this.NotFound();
            }

            return this.Ok(member);
        }

        [HttpPost]
        public IActionResult CreateMember([FromBody]Member newMember, Guid teamId)
        {
            var team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }

            //team.Members.Add(newMember);
            //var teamMember = new { TeamId = teamId, MemberId = newMember.Id };
            //return this.Created($"/teams/{teamMember.TeamId}/[controller]/{teamMember.MemberId}", teamMember);
            return this.Ok();
        }

        [HttpPut("/teams/{teamId}/[controller]/{memberId}")]
        public IActionResult UpdateMember([FromBody]Member updatedMember, Guid teamId, Guid memberId)
        {
            var team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }

            var member = team.Members.SingleOrDefault(m => m.Id == memberId);
            if (member == null)
            {
                return this.NotFound();
            }

            team.Members.Remove(member);
            team.Members.Add(updatedMember);
            return this.Ok();
        }

        [HttpDelete("/teams/{teamId}/[controller]/{memberId}")]
        public IActionResult DeleteMember(Guid teamId, Guid memberId)
        {
            var team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }

            var member = team.Members.SingleOrDefault(m => m.Id == memberId);
            if (member == null)
            {
                return this.NotFound();
            }

            team.Members.Remove(member);
            return this.Ok(member.Id);
        }

        [HttpGet("/members/{memberId}/team")]
        public IActionResult GetTeamForMember(Guid memberId)
        {
            foreach (var team in repository.List())
            {
                var member = team.Members.SingleOrDefault(m => m.Id == memberId);
                if (member != null)
                {
                    return this.Ok(team);
                }
            }

            return this.NotFound();
        }
    }
}
