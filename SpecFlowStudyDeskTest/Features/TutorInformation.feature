Feature: TutorInformation

@mytag
Scenario: Get full information of tutor
	Given a tutor with name "Jose" description "Hola soy nuevo" and id 1
	When the student select a tutor with id 1
	Then he get a tutor with name "Jose" description "Hola soy nuevo" and id 1


@mytag
Scenario: Get incomplete information of tutor
	Given a tutor with name "Jose" without description and id 1
	When the student select a tutor with id 1
	Then he get a tutor with name "Jose" without description and id 1