﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace BDD_Tests.PageObjects
{
    public class LibraryPageObject : PageObject
    {
        public LibraryPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Library";
        }

        public IWebElement RegisterButton => _webDriver.FindElement(By.Id("register-link"));
        public IWebElement NavBarHelloLink => _webDriver.FindElement(By.CssSelector("a[href=\"/Identity/Account/Manage\"]"));
        public IWebElement SteamLinkButton => _webDriver.FindElement(By.Id("link-login-button-Steam"));
        public IWebElement SteamAvatarImg => _webDriver.FindElement(By.ClassName("user-avatar"));
        public ReadOnlyCollection<IWebElement> FollowGamesButtons => _webDriver.FindElements(By.ClassName("follow-btn"));

        public IWebElement LinkingMessage => _webDriver.FindElement(By.Id("link-message"));

        public void SteamLinkButtonClick()
        {
            SteamLinkButton.Click();
        }

        public void FollowFirstGame()
        {
            FollowGamesButtons.FirstOrDefault();

            if (FollowGamesButtons.Count > 0)
            {
                FollowGamesButtons.FirstOrDefault().Click();
            }
        }

        public bool SteamAvatarImgVisible()
        {
            return SteamAvatarImg != null;
        }
        public void Logout()
        {
            IWebElement navbarLogoutButton = _webDriver.FindElement(By.Id("logout-button"));
            navbarLogoutButton.Click();
        }

        public bool GetLinkingMessage()
        {
            return LinkingMessage != null;
        }

        public bool ContainsGame(string gameName)
        {
            return _webDriver.FindElement(By.Id(gameName)) != null;
        }

        public bool FindHideButtonForGame(string gameName)
        {
            return _webDriver.FindElement(By.Id(gameName)).FindElement(By.ClassName("hide-btn")) != null;
        }

        [When(@"I click on the hide button for ""([^""]*)""")]
        public void WhenIClickOnTheHideButtonFor(string gameName)
        {
            _webDriver.FindElement(By.Id(gameName)).FindElement(By.ClassName("hide-btn")).Click();
        }

        [Then(@"I wont see ""([^""]*)""")]
        public void ThenIWontSee(string p0)
        {
            throw new PendingStepException();
        }

    }
}
