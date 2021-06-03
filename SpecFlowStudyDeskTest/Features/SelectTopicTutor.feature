Feature: SelectTopicTutor

Scenario: Tutor selects topics
	Given exists a list of topics with 2 elemets
	When a tutor selects topics options
	Then the result is a list of 2 elements

Scenario: Tutor selects create topics
	Given the tutor cannot find the topic with name "api rest"
	When add new topic with the name "api rest"
	Then it is added the topic with the name "api rest" to the list of topics