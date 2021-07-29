Feature: reqres
	
Background: 
	Given I have connected to reqres

@mytag
Scenario: Get user
	
	When I get a user 3
	Then the first name is "Emma"

Scenario: Get user list
	When I am on page 12
	Then there are 6 items listed
