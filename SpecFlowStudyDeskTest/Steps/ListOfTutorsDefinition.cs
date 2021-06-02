using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using StudyDesck.API.Controllers;
using StudyDesck.API.Domain.Models;

namespace SpecFlowStudyDeskTest.Steps
{
    [Binding]
    public sealed class ListOfTutorsDefinition
    {

        private readonly ScenarioContext _scenarioContext;
        private readonly TutorSessionsController _tutorSessionsController;
        private readonly SessionStudentsController _sessionStudentsController;
        private readonly StudentSessionsController _studentSessionsController;
        public ListOfTutorsDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("a list of tutors")]
        public void GivenTheListOfTutor(int tutorId)
        {
            List<Tutor> tutors = new List<Tutor>;
        }

        [When(" one student filter by ratings of tutor with more of (.*) rewiers")]
        public void FiltrerByRatings()
        {

        }
    }
}
