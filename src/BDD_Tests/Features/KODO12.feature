﻿@Justin
Feature: Ability to see a Steam Library after signing in

As a user, I want the be able to go to the library page and see my Steam games present on the page.
If there is no Steam account linked, then I should be presented with a message telling me what I need
	to do in order to make the page load what's expected.

@LoggedIn
Scenario: Library page will show a message when a user doesn't have a linked Steam account
	Given I am a user with name "TestUser2"
	When I click on the "Library" link
	Then The page shows me a message that I dont have a Steam account linked
	
@LoggedIn
Scenario: Library page title contains Library
	Given I am signed in
	When I click on the "Library" link
	Then The page title contains "Library"

@LoggedIn
Scenario: Library page contains at least one game for user
	Given I am signed in
	When I click on the "Library" link
	Then I should see my owned game "Vampire Survivors"
