using Microsoft.EntityFrameworkCore;
using Moq;
using SteamProject.Models;
using SteamProject.Models.DTO;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using SteamProject.DAL.Abstract;
using SteamProject.DAL.Concrete;
using System;

namespace NUnit_Tests.RepoTesting
{
    public class GameTests
    {

        private Game MakeValidGame()
        {
            Game newGame = new Game
            {
                Id = 1,
                AppId = 1,
                Name = "TestUser1",
                DescShort = "Short Description",
                PlayTime = 100,
                IconUrl = "bogusIconUrl",
                LastPlayed = 100
            };
            return newGame;
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Game_WithValidInfo_IsValid()
        {
            // Arrange
            Game game = new Game();

            // Act
            game = MakeValidGame();
            ModelValidator mv = new ModelValidator(game);

            // Assert
            Assert.That(mv.Valid, Is.True);
        }
    }
}