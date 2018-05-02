using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Tests
{
    public class TestMemoryTeamRepository : MemoryTeamRepository
    {
        public TestMemoryTeamRepository()
            : base(InitFakeRepo())
        {
        }

        private static ICollection<Team> InitFakeRepo()
        {
            return new List<Team> { new Team("one"), new Team("two") };
        }
    }
}
