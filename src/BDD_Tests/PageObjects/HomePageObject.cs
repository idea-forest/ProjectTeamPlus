﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace BDD_Tests.PageObjects
{
    public class HomePageObject : PageObject
    {
        public HomePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Home";
        }

        public IWebElement RegisterButton => _webDriver.FindElement(By.ClassName("register-link"));
        public IWebElement NavBarHelloLink => _webDriver.FindElement(By.CssSelector("a[href=\"/Identity/Account/Manage\"]"));
        public IWebElement NavBarCompeteLink => _webDriver.FindElement(By.Id("navCompete"));
        public IWebElement NavBarProfileLink => _webDriver.FindElement(By.Id("navProfile"));

        public IWebElement NavBarDashboardLink => _webDriver.FindElement(By.Id("navDashboard"));

        public IWebElement NavBarLibraryLink => _webDriver.FindElement(By.Id("navLibrary"));



        public string NavbarWelcomeText()
        {
            return NavBarHelloLink.Text;
        }

        public void ClickNavBarCompeteLink()
        {
            NavBarCompeteLink.Click();
        }

        public void ClickNavBarDashboardLink()
        {
            NavBarDashboardLink.Click();
        }

        public void ClickNavBarLibraryLink()
        {
            NavBarLibraryLink.Click();
        }


        public void ClickNavBarProfileLink()
        {
            NavBarProfileLink.Click();
        }

        public void Logout()
        {
            IWebElement navbarLogoutButton = _webDriver.FindElement(By.Id("logout-button"));
            navbarLogoutButton.Click();
        }
    }
}
