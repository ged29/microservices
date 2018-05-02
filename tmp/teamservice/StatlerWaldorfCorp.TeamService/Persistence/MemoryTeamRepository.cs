using System;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected static ICollection<Team> teams;

        public MemoryTeamRepository()
        {
            if (teams == null)
            {
                teams = new List<Team>();
            }
        }

        public MemoryTeamRepository(ICollection<Team> teams)
        {
            MemoryTeamRepository.teams = teams;
        }

        public Team Add(Team team)
        {
            teams.Add(team);
            return team;
        }

        public Team Get(Guid id)
        {
            return teams.FirstOrDefault(t => t.Id == id);
        }

        public Team Update(Team team)
        {
            if (this.Delete(team.Id) != null)
            {
                return this.Add(team);
            }

            return null;
        }

        public Team Delete(Guid id)
        {
            var teamToDelete = this.Get(id);
            if (teamToDelete != null)
            {
                teams.Remove(teamToDelete);
                return teamToDelete;
            }

            return null;
        }

        public IEnumerable<Team> List()
        {
            return teams;
        }
    }
}
