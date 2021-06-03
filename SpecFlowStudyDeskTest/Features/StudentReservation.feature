Feature: Student Reservation Tutor

Background: 
	Given a career named "Civil" with a Careerid "1" 

Scenario: Student looks for a tutorship
	Given I am in the finding section (one)
	When I try to look for a tutorship (one)
	Then I should see a list of tutors

Scenario: Student looks fot a tutorship
	Given I am in the finding section (two)
	When I try to look for a tutorship (two)
	Then I should SEE "There are not tutors giving tutorships for this career"