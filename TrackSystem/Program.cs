using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public delegate void DoTaskHandler (Developer developer, Tasks task);
    public delegate void TestTaskHandler (Tester test, Tasks task);

    class Program {
        static void Main (string[] args) {

            Developer developer;
            Tester tester;
            Tasks task;
            Filler.Fill (out developer, out tester, 5); // Randomly fill dev and testers
            Filler.Fill (out task, 20); // Randomly fill dev and testers

            Company company = Company.Instance; 
            string scrumTeamMemebers = company.Describe (developer, tester); // Display the team 

            Backlog backlog = new Backlog (task);
            
            Console.WriteLine (scrumTeamMemebers);
            Console.WriteLine ($"Total sprint score: {backlog.SprintTotalPoints} pts\n");
            
            DoTaskHandler devTask = developer.DoTask ;
            devTask (developer, task);

            TestTaskHandler testTask = tester.TestTask ;
            testTask (tester, task);
            
            Console.Read ();
        }
        

    }
}
