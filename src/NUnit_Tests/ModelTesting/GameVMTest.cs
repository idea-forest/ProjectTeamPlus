using SteamProject.Models;
using SteamProject.ViewModels;
using System.Text.Json;
using SteamProject.Models.DTO;

namespace NUnit_Tests.ModelTesting
{
    public class GameVMTests
    {
        private GameInfoPOCO _poco;
        private GameVM MakeValidGameVM()
        {
            GameVM gameVm = new GameVM
            {
                _appId = 1,
                _userGame = new UserGameInfo(),
                _game = new Game
                {
                    Id = 1,
                    AppId = 1,
                    Name = "A Game"
                },
                _poco = new GameInfoPOCO
                {
                    response = new GameResponse
                    {
                        data = null
                    }
                },
                playTime = 100
            };
            return gameVm;
        }
        [SetUp]
        public void Setup()
        {
            // I'm sorry for how stupidly long this is, I tried time and time again to cut this up in note pad with no luck.
            _poco = JsonSerializer.Deserialize<GameInfoPOCO>("{\"response\":{\"success\":true,\"data\":{\"type\":\"game\",\"name\":\"Vlad the Impaler\",\"steam_appid\":295850,\"required_age\":0,\"is_free\":false,\"detailed_description\":\"Vlad is an interactive graphic-novel with RPG elements where your decisions shape the outcome of your story.  Each playthrough offers a different experience and immersive story totalling two hours of content.<h2 class=\\\"bb_tag\\\">Features<\\/h2><ul class=\\\"bb_ul\\\"><li> Three different classes to choose from.<br><\\/li><li> A morality system that unlocks special classes based on your decisions.<br><\\/li><li> The choices you make will make or break your stats, effecting how your story ends.<br><\\/li><li> An engrossing, gritty art style that immerses you in the world of Vlad the Impaler.<br><\\/li><li> Music by Kevin Riepl, composer for Gears of War and many other titles.<br><\\/li><li> Over 50 unique illustrated stories.<br><\\/li><li> Over 100 unique illustrations including weapons, characters, stories and more.<br><\\/li><li> Collectable Trading cards, badges and achievements.<\\/li><\\/ul><h2 class=\\\"bb_tag\\\">Story<\\/h2><br>The scent of murder hangs in the air.  It is the year 1452 and a city under siege is desperate.  You receive a letter, bearing the king’s seal, begging for your help.  A once beautiful, peaceful place is now a tormentor’s paradise; dark, bleak and heavy with fear.  <br><br>You venture along the road to your destination, determined to solve the mystery terrorizing the citizens of Istanbul.  Thousands of bodies line the roads, impaled on bloody poles, ravaged &amp; torn, lying limp; faces contorted in anguish.  If this is just the path to the city, what lies within its walls?  <br><br>A gruesome scene welcomes you; hollow sockets live where eyes once saw their murderer coming for them.  Torsos of young children ripped open, their bodies hung like criminals; blood filled pools in the Cistern house rotting bodies, throats cut, fingers missing.  Dank Catacombs lined with broken bones containing new members, heads daggered, burn marks over eyes.  Blood streaked Basilica walls, disembowelments and beheadings.  The brutality is too much to bear. What – or who -is murdering the innocent lives of these people!?  You hunt through the night, intent on finding your foe, bringing an end to the grisly massacre. <br><br>As the Explorer, you have knowledge of the land; you are fast, on target and adventurous.  You leave no stone unturned in your pursuit. Will you be the enemy Spy or will you be the Emissary?  <br><br>The Mage is skilled, wise and supernatural.  Become the the high Priest who can be counted on to cast spells to protect the people of Istanbul or choose to be the Sorcerer and release your inner demon fueling your thirst for destruction.<br><br>The Soldier; fierce, strong, and none better with a blade.  Protecting people is your Knightly duty, with your feminine intuition &amp; compassionate heart leading you to do the right thing for the kingdom.  But perhaps you would rather seek to unlock the dark side and become the Assassin.  The choice is yours; will you seek justice or wickedness?<br><br>Each character unlocks unique levels and secret weapons.  Feel the weight of your decisions and change the story of your game with each turn.  Your actions will determine the future of your people, saving their souls or surrendering them to evil.<h2 class=\\\"bb_tag\\\">About the Developers<\\/h2><br>Section Studios brings you this action packed adventure, where your wits are just as important as your strength.  We wanted to create an experience that combined a unique visual style with thoughtful narrative and game play.  We chose a world and character that our team would feel passionate about reinventing with Vlad.  By creating everything from story to artwork, we were allowed to let our imaginations run wild and create something we all felt connected to.<br><br>As a studio that originated in the world of video games, we know what our audience wants because we ARE the audience.  Supernatural powers, an abundance of undead abominations and plenty of secret expeditions throughout the game, along with awesome, gothic graphics, illustrations &amp; challenges are what made this game something we were proud to work on – and very excited to play.\",\"about_the_game\":\"Vlad is an interactive graphic-novel with RPG elements where your decisions shape the outcome of your story.  Each playthrough offers a different experience and immersive story totalling two hours of content.<h2 class=\\\"bb_tag\\\">Features<\\/h2><ul class=\\\"bb_ul\\\"><li> Three different classes to choose from.<br><\\/li><li> A morality system that unlocks special classes based on your decisions.<br><\\/li><li> The choices you make will make or break your stats, effecting how your story ends.<br><\\/li><li> An engrossing, gritty art style that immerses you in the world of Vlad the Impaler.<br><\\/li><li> Music by Kevin Riepl, composer for Gears of War and many other titles.<br><\\/li><li> Over 50 unique illustrated stories.<br><\\/li><li> Over 100 unique illustrations including weapons, characters, stories and more.<br><\\/li><li> Collectable Trading cards, badges and achievements.<\\/li><\\/ul><h2 class=\\\"bb_tag\\\">Story<\\/h2><br>The scent of murder hangs in the air.  It is the year 1452 and a city under siege is desperate.  You receive a letter, bearing the king’s seal, begging for your help.  A once beautiful, peaceful place is now a tormentor’s paradise; dark, bleak and heavy with fear.  <br><br>You venture along the road to your destination, determined to solve the mystery terrorizing the citizens of Istanbul.  Thousands of bodies line the roads, impaled on bloody poles, ravaged &amp; torn, lying limp; faces contorted in anguish.  If this is just the path to the city, what lies within its walls?  <br><br>A gruesome scene welcomes you; hollow sockets live where eyes once saw their murderer coming for them.  Torsos of young children ripped open, their bodies hung like criminals; blood filled pools in the Cistern house rotting bodies, throats cut, fingers missing.  Dank Catacombs lined with broken bones containing new members, heads daggered, burn marks over eyes.  Blood streaked Basilica walls, disembowelments and beheadings.  The brutality is too much to bear. What – or who -is murdering the innocent lives of these people!?  You hunt through the night, intent on finding your foe, bringing an end to the grisly massacre. <br><br>As the Explorer, you have knowledge of the land; you are fast, on target and adventurous.  You leave no stone unturned in your pursuit. Will you be the enemy Spy or will you be the Emissary?  <br><br>The Mage is skilled, wise and supernatural.  Become the the high Priest who can be counted on to cast spells to protect the people of Istanbul or choose to be the Sorcerer and release your inner demon fueling your thirst for destruction.<br><br>The Soldier; fierce, strong, and none better with a blade.  Protecting people is your Knightly duty, with your feminine intuition &amp; compassionate heart leading you to do the right thing for the kingdom.  But perhaps you would rather seek to unlock the dark side and become the Assassin.  The choice is yours; will you seek justice or wickedness?<br><br>Each character unlocks unique levels and secret weapons.  Feel the weight of your decisions and change the story of your game with each turn.  Your actions will determine the future of your people, saving their souls or surrendering them to evil.<h2 class=\\\"bb_tag\\\">About the Developers<\\/h2><br>Section Studios brings you this action packed adventure, where your wits are just as important as your strength.  We wanted to create an experience that combined a unique visual style with thoughtful narrative and game play.  We chose a world and character that our team would feel passionate about reinventing with Vlad.  By creating everything from story to artwork, we were allowed to let our imaginations run wild and create something we all felt connected to.<br><br>As a studio that originated in the world of video games, we know what our audience wants because we ARE the audience.  Supernatural powers, an abundance of undead abominations and plenty of secret expeditions throughout the game, along with awesome, gothic graphics, illustrations &amp; challenges are what made this game something we were proud to work on – and very excited to play.\",\"short_description\":\"A city gone mad with violence and the mass murder of its people. In this interactive graphic-novel, your choices can either lead you to the ultimate destruction of this fallen metropolis, or the demise of a vicious butcher. Will you save this city as promised or will you fall prey to the killer within?\",\"supported_languages\":\"English<strong>*<\\/strong><br><strong>*<\\/strong>languages with full audio support\",\"header_image\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/header.jpg?t=1610719187\",\"website\":\"http:\\/\\/vlad.sectionstudios.com\",\"pc_requirements\":{\"minimum\":\"<strong>Minimum:<\\/strong><br><ul class=\\\"bb_ul\\\"><li><strong>OS:<\\/strong> Windows XP or Later<br><\\/li><li><strong>Processor:<\\/strong> Core 2 Duo<br><\\/li><li><strong>Memory:<\\/strong> 2 GB RAM<br><\\/li><li><strong>Graphics:<\\/strong> 256 MB graphics memory and directx 9.0c compatible gpu<br><\\/li><li><strong>DirectX:<\\/strong> Version 9.0c<br><\\/li><li><strong>Storage:<\\/strong> 512 MB available space<\\/li><\\/ul>\",\"recommended\":\"<strong>Recommended:<\\/strong><br><ul class=\\\"bb_ul\\\"><li><strong>OS:<\\/strong> Windows XP or Later<br><\\/li><li><strong>Processor:<\\/strong> Core i3<br><\\/li><li><strong>Memory:<\\/strong> 4 GB RAM<br><\\/li><li><strong>Graphics:<\\/strong> 256 MB graphics memory and directx 9.0c compatible gpu<br><\\/li><li><strong>DirectX:<\\/strong> Version 9.0c<br><\\/li><li><strong>Storage:<\\/strong> 512 MB available space<\\/li><\\/ul>\"},\"mac_requirements\":{\"minimum\":\"<strong>Minimum:<\\/strong><br><ul class=\\\"bb_ul\\\"><li><strong>OS:<\\/strong> Os X 10.7 or later<br><\\/li><li><strong>Memory:<\\/strong> 2 GB RAM<br><\\/li><li><strong>Storage:<\\/strong> 512 MB available space<\\/li><\\/ul>\",\"recommended\":\"<strong>Recommended:<\\/strong><br><ul class=\\\"bb_ul\\\"><li><strong>OS:<\\/strong> Os X 10.7 or later<br><\\/li><li><strong>Memory:<\\/strong> 4 GB RAM<br><\\/li><li><strong>Storage:<\\/strong> 512 MB available space<\\/li><\\/ul>\"},\"linux_requirements\":{},\"legal_notice\":\"Copyright © 2014 Section Studios\",\"developers\":[\"Section Studios\"],\"publishers\":[\"Section Studios\"],\"package_groups\":[],\"platforms\":{\"windows\":true,\"mac\":true,\"linux\":false},\"categories\":[{\"id\":2,\"description\":\"Single-player\"},{\"id\":22,\"description\":\"Steam Achievements\"},{\"id\":29,\"description\":\"Steam Trading Cards\"}],\"genres\":[{\"id\":\"25\",\"description\":\"Adventure\"},{\"id\":\"4\",\"description\":\"Casual\"},{\"id\":\"23\",\"description\":\"Indie\"},{\"id\":\"3\",\"description\":\"RPG\"}],\"screenshots\":[{\"id\":0,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_6d347387af2bc841b9d21e9d5bea25e5e2778de5.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_6d347387af2bc841b9d21e9d5bea25e5e2778de5.1920x1080.jpg?t=1610719187\"},{\"id\":1,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_f77679b82ee562335ed1b1d1d9262e5849c84940.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_f77679b82ee562335ed1b1d1d9262e5849c84940.1920x1080.jpg?t=1610719187\"},{\"id\":2,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_4bea38d4070d306292b57cc9a6690b3b29c89bfc.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_4bea38d4070d306292b57cc9a6690b3b29c89bfc.1920x1080.jpg?t=1610719187\"},{\"id\":3,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_88ebb44a5824e98fe343a5a7a4fd500db39d3d41.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_88ebb44a5824e98fe343a5a7a4fd500db39d3d41.1920x1080.jpg?t=1610719187\"},{\"id\":4,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_1e48660d74c4a77cb7dabf847bfd050108297f95.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_1e48660d74c4a77cb7dabf847bfd050108297f95.1920x1080.jpg?t=1610719187\"},{\"id\":5,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_b99ec843ce76c9f71729daea025a4962194f60b9.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_b99ec843ce76c9f71729daea025a4962194f60b9.1920x1080.jpg?t=1610719187\"},{\"id\":6,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_549b59f0a30712800a8c473a6ea3b7e5c6dd21c3.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_549b59f0a30712800a8c473a6ea3b7e5c6dd21c3.1920x1080.jpg?t=1610719187\"},{\"id\":7,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_2e53b2cc14de4f7ca85899ce2d8a2adb361c09e5.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_2e53b2cc14de4f7ca85899ce2d8a2adb361c09e5.1920x1080.jpg?t=1610719187\"},{\"id\":8,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_2847d52df2f2626426a8659e0d1b47bb8746d759.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_2847d52df2f2626426a8659e0d1b47bb8746d759.1920x1080.jpg?t=1610719187\"},{\"id\":9,\"path_thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_aa897e4dfb6ee90d8f0fea3f1ab9e661c36d97aa.600x338.jpg?t=1610719187\",\"path_full\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/ss_aa897e4dfb6ee90d8f0fea3f1ab9e661c36d97aa.1920x1080.jpg?t=1610719187\"}],\"movies\":[{\"id\":2033113,\"name\":\"Vlad the Impaler Teaser Trailer\",\"thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033113\\/movie.293x165.jpg?t=1447362471\",\"webm\":{\"480\":\"http:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033113\\/movie480.webm?t=1447362471\",\"max\":\"http:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033113\\/movie_max.webm?t=1447362471\"},\"mp4\":{\"480\":\"http:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033113\\/movie480.mp4?t=1447362471\",\"max\":\"http:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033113\\/movie_max.mp4?t=1447362471\"},\"highlight\":true},{\"id\":2033528,\"name\":\"Vlad Gameplay Trailer\",\"thumbnail\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033528\\/movie.293x165.jpg?t=1447362775\",\"webm\":{\"480\":\"http:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033528\\/movie480.webm?t=1447362775\",\"max\":\"http:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033528\\/movie_max.webm?t=1447362775\"},\"mp4\":{\"480\":\"http:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033528\\/movie480.mp4?t=1447362775\",\"max\":\"http:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/2033528\\/movie_max.mp4?t=1447362775\"},\"highlight\":true}],\"recommendations\":{\"total\":333},\"achievements\":{\"total\":30,\"highlighted\":[{\"name\":\"Executioner\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/555fbca1898db2df4c0b18b30b8e579962ba4628.jpg\"},{\"name\":\"Paladin\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/ffa3415a7ca74794026712d246735868ef9138df.jpg\"},{\"name\":\"Warlock\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/d053774dd4cf6c1595fecaa6cfd962ae826e0d40.jpg\"},{\"name\":\"Cleric\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/52e79c04e16531b95f862b08d621b7a334e7e0f1.jpg\"},{\"name\":\"Infiltrator\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/57047d6d3395eb13e928f51bc5f1d7fe93b896ec.jpg\"},{\"name\":\"Envoy\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/3febaebf44666d4842a2e8b6e871ea32724c4496.jpg\"},{\"name\":\"Menacing Flames\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/856e1236675b5574877f538b93e2280eea33037a.jpg\"},{\"name\":\"Primal Power\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/3eaaef86ad8030106ed623a570d0065b2e7dc4f2.jpg\"},{\"name\":\"Legendary Oak\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/a1b66973537d4b01d57c460474e515560afe54f1.jpg\"},{\"name\":\"Petrified\",\"path\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steamcommunity\\/public\\/images\\/apps\\/295850\\/c4f700341e1f03120f070a117c7be0713fcb1485.jpg\"}]},\"release_date\":{\"coming_soon\":false,\"date\":\"Jul 16, 2014\"},\"support_info\":{\"url\":\"http:\\/\\/sectionstudios.com\",\"email\":\"info@sectionstudios.com\"},\"background\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/page_bg_generated_v6b.jpg?t=1610719187\",\"background_raw\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/295850\\/page.bg.jpg?t=1610719187\",\"content_descriptors\":{\"ids\":[],\"notes\":null}}}}");
        }

        [Test]
        public void GameVM_HasGame_isValid()
        {
            // Arrange
            GameVM gameVm = MakeValidGameVM();

            // Act
            ModelValidator mv = new ModelValidator(gameVm);

            // Assert
            Assert.That(mv.Valid, Is.True);
        }

        [Test]
        public void GameVMcleanDescriptions_HasNoData_ReturnsWithNullForPOCOData()
        {
            // Arrange
            GameVM gameVm = MakeValidGameVM();

            // Act
            gameVm.cleanDescriptions();

            // Assert
            Assert.True(gameVm._poco.response.data == null);
        }

        [Test]
        public void GameVMcleanRequirements_HasNoData_ReturnsWithNullForPOCOData()
        {
            // Arrange
            GameVM gameVm = MakeValidGameVM();

            // Act
            gameVm.cleanRequirements();

            // Assert
            Assert.True(gameVm._poco.response.data == null);
        }

        [Test]
        public void GameVMcleanDescription_HasDetailedDescriptionData_ReturnsWithCleanedShortDescription()
        {
            // Arrange
            GameVM gameVm = MakeValidGameVM();
            gameVm._poco = _poco;
            string expected = "A city gone mad with violence and the mass murder of its people. In this interactive graphic-novel, your choices can either lead you to the ultimate destruction of this fallen metropolis, or the demise of a vicious butcher. Will you save this city as promised or will you fall prey to the killer within?";

            // Act
            gameVm.cleanDescriptions();

            // Assert
            Assert.AreEqual(expected, gameVm._poco.response.data.short_description);
        }

        [Test]
        public void GameVMcleanRequirements_HasDetailedDescriptionData_ReturnsWithCleanedRequirements()
        {
            // Arrange
            GameVM gameVm = MakeValidGameVM();
            gameVm._poco = _poco;
            string expected = " OS: Windows XP or Later Processor: Core 2 Duo Memory: 2 GB RAM Graphics: 256 MB graphics memory and directx 9.0c compatible gpu DirectX: Version 9.0c Storage: 512 MB available space";

            // Act
            gameVm.cleanRequirements();

            // Assert
            Assert.AreEqual(expected, gameVm._poco.response.data.pc_requirements.minimum);
        }

    }
}