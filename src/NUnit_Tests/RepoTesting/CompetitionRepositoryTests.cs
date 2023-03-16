using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using SteamProject.DAL.Abstract;
using SteamProject.Models;

namespace NUnit_Tests.RepoTesting
{
    public class CompetitionRepositoryTests
    {
        private Mock<SteamInfoDbContext> _mockContext;
        private Mock<DbSet<Competition>> _mockCompetitionDbSet;

        private List<Competition> _competitions; 

        [SetUp]
        public void Setup()
        {
            _competitions = new List<Competition>
            {
                new Competition { Id = 1, GameId = 1, StartDate = new DateTime(2001, 03, 08), EndDate = new DateTime(2001, 03, 09) },
                new Competition { Id = 2, GameId = 2, StartDate = new DateTime(2001, 03, 10), EndDate = new DateTime(2001, 03, 11) },
                new Competition { Id = 3, GameId = 3, StartDate = new DateTime(2001, 03, 12), EndDate = new DateTime(2001, 03, 13) }
            };

            _mockContext = new Mock<SteamInfoDbContext>();
            _mockCompetitionDbSet = MockHelpers.GetMockDbSet(_competitions.AsQueryable());
            _mockContext.Setup(ctx => ctx.Competitions).Returns(_mockCompetitionDbSet.Object);
            _mockContext.Setup(ctx => ctx.Set<Competition>()).Returns(_mockCompetitionDbSet.Object);
        }

        public void GetCompetitionById_IfNoMatch_ReturnsNull()
        {
            throw new NotImplementedException();
        }
    }
}