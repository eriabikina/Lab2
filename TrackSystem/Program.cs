using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace TrackSystem {
    class Program {
        static void Main (string[] args) {
                      
            Developer developer;
            Tester tester;
            Tasks task;
            Filler.Fill (out developer, out tester, 5); // Randomly fill dev and testers
            Filler.Fill (out task, 20); // Randomly fill tasks

            Company company = Company.Instance;
            string scrumTeamMemebers = company.Describe (developer, tester); // Display scrum team members 
            Console.WriteLine (scrumTeamMemebers);

            company.SalaryPaid += OnSalaryPaid;   //subscribe for salary time          

            Backlog backlog = new Backlog (task);
            Console.WriteLine ($"Total sprint score: {backlog.SprintTotalPoints} pts\n");// How many points does the sprint worth 

            company.SalaryTime (developer.employee); // salary for developers

            DoTaskHandler devTask = developer.DoTask;
            devTask (developer, task);// display development task progress

            TestTaskHandler testTask = tester.TestTask;
            testTask (tester, task); // display test task progress                       
            
            company.SalaryTime (tester.employee);// salary for testers

            Console.Read ();
        }

        private static void OnSalaryPaid(object sender, SalaryPaidEventArgs args) {
            Console.WriteLine ($"My name is {args.Name} and I got paid: ${args.Salary}");
        }

    }
}
