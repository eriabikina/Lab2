using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    class Program {
        static void Main (string[] args) {

            Developer developer;
            Tester tester;
            Tasks task;
            Filler.Fill (out developer, out tester, 5); // Randomly fill dev and testers
            Filler.Fill (out task, 15); // Randomly fill dev and testers

            Company company = Company.Instance; 
            string scrumTeamMemebers = company.Describe (developer, tester); // Display the team 

            Backlog backlog = new Backlog (task);
            
            Console.WriteLine (scrumTeamMemebers);
            Console.WriteLine ($"Total sprint score: {backlog.SprintTotalPoints} pts");

            Console.WriteLine ("Development progress:\n");
            Console.WriteLine (developer.DoTask(developer,task) );

            Console.WriteLine ("Test progress:\n");
            Console.WriteLine (tester.TestTask (tester, task));

            Console.ReadLine ();
        }
        
    }
}
