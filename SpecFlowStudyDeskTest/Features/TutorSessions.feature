Feature: Tutor Add Sessions

Background: 
	Given a tutor named "Josias" with a TutorId "322"
	And a session named "Taller Progra"

Scenario: Tutor schedules a session 
	Given  I am in the tutorial session section (one)
	When I try to schedule a session 
	Then I should see "session successfully saved"

Scenario: tutor schedules a wrong session
	Given I am in the tutorial session section (two)
	When I try to schedule a session and I get confused in a data
	Then I should can deleting this session

Scenario: tutor schedules a session with a schedule crossing 
	Given I am in the tutorial session section (three)
	When I try to schedule a session that intersects at some time with another session that has already been scheduled
	Then I should see "They cannot exist crossings of schedules"





	


